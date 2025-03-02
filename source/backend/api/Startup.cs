using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using HealthChecks.UI.Client;
using Mapster;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Pims.Api.Handlers;
using Pims.Api.Helpers;
using Pims.Api.Helpers.Authorization;
using Pims.Api.Helpers.Exceptions;
using Pims.Api.Helpers.Logging;
using Pims.Api.Helpers.Mapping;
using Pims.Api.Helpers.Middleware;
using Pims.Api.Helpers.Routes.Constraints;
using Pims.Api.Repositories.Cdogs;
using Pims.Api.Repositories.Mayan;
using Pims.Api.Services;
using Pims.Av;
using Pims.Core.Converters;
using Pims.Core.Http;
using Pims.Dal;
using Pims.Dal.Keycloak;
using Pims.Geocoder;
using Pims.Ltsa;
using Prometheus;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Pims.Api
{
    /// <summary>
    /// Startup class, provides a way to startup the .netcore RESTful API and configure it.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        #region Properties

        /// <summary>
        /// get - The application configuration settings.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// get/set - The environment settings for the application.
        /// </summary>
        public IWebHostEnvironment Environment { get; }
        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instances of a Startup class.
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="env"></param>
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            this.Configuration = configuration;
            this.Environment = env;
        }
        #endregion

        #region Methods

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSerilogging(this.Configuration);
            var jsonSerializerOptions = this.Configuration.GenerateJsonSerializerOptions();
            var pimsOptions = this.Configuration.GeneratePimsOptions();
            services.AddMapster(jsonSerializerOptions, pimsOptions, options =>
            {
                options.Default.IgnoreNonMapped(true);
                options.Default.IgnoreNullValues(true);
                options.AllowImplicitDestinationInheritance = true;
                options.AllowImplicitSourceInheritance = true;
                options.Default.UseDestinationValue(member =>
                    member.SetterModifier == AccessModifier.None &&
                    member.Type.IsGenericType &&
                    member.Type.GetGenericTypeDefinition() == typeof(ICollection<>));
            });
            services.Configure<JsonSerializerOptions>(options =>
            {
                options.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                options.DefaultIgnoreCondition = jsonSerializerOptions.DefaultIgnoreCondition;
                options.PropertyNameCaseInsensitive = jsonSerializerOptions.PropertyNameCaseInsensitive;
                options.PropertyNamingPolicy = jsonSerializerOptions.PropertyNamingPolicy;
                options.WriteIndented = jsonSerializerOptions.WriteIndented;
                options.Converters.Add(new JsonStringEnumMemberConverter());
                options.Converters.Add(new Int32ToStringJsonConverter());
            });
            services.Configure<Core.Http.Configuration.AuthClientOptions>(this.Configuration.GetSection("Keycloak"));
            services.Configure<Core.Http.Configuration.OpenIdConnectOptions>(this.Configuration.GetSection("Keycloak:OpenIdConnect"));
            services.Configure<Keycloak.Configuration.KeycloakOptions>(this.Configuration.GetSection("Keycloak"));
            services.Configure<Pims.Dal.PimsOptions>(this.Configuration.GetSection("Pims"));
            services.AddOptions();

            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                    options.JsonSerializerOptions.DefaultIgnoreCondition = jsonSerializerOptions.DefaultIgnoreCondition;
                    options.JsonSerializerOptions.PropertyNameCaseInsensitive = jsonSerializerOptions.PropertyNameCaseInsensitive;
                    options.JsonSerializerOptions.PropertyNamingPolicy = jsonSerializerOptions.PropertyNamingPolicy;
                    options.JsonSerializerOptions.WriteIndented = jsonSerializerOptions.WriteIndented;
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                    options.JsonSerializerOptions.Converters.Add(new Int32ToStringJsonConverter());
                    options.JsonSerializerOptions.Converters.Add(new GeometryJsonConverter());
                });

            services.AddMvcCore()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.DefaultIgnoreCondition = jsonSerializerOptions.DefaultIgnoreCondition;
                    options.JsonSerializerOptions.PropertyNameCaseInsensitive = jsonSerializerOptions.PropertyNameCaseInsensitive;
                    options.JsonSerializerOptions.PropertyNamingPolicy = jsonSerializerOptions.PropertyNamingPolicy;
                    options.JsonSerializerOptions.WriteIndented = jsonSerializerOptions.WriteIndented;
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                    options.JsonSerializerOptions.Converters.Add(new Int32ToStringJsonConverter());
                    options.JsonSerializerOptions.Converters.Add(new GeometryJsonConverter());
                });

            services.AddRouting(options =>
            {
                options.ConstraintMap.Add("pid", typeof(PidConstraint));
            });

            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    var key = Encoding.ASCII.GetBytes(Configuration["Keycloak:Secret"]);
                    options.RequireHttpsMetadata = false;
                    options.Authority = Configuration["OpenIdConnect:Authority"];
                    options.Audience = Configuration["Keycloak:Audience"];
                    options.SaveToken = true;
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidAlgorithms = new List<string>() { "RS256" },
                    };
                    if (key.Length > 0)
                    {
                        options.TokenValidationParameters.IssuerSigningKey = new SymmetricSecurityKey(key);
                    }

                    options.Events = new JwtBearerEvents()
                    {
                        OnTokenValidated = context =>
                        {
                            return Task.CompletedTask;
                        },
                        OnAuthenticationFailed = context =>
                        {
                            context.NoResult();
                            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                            throw new AuthenticationException("Failed to authenticate", context.Exception);
                        },
                        OnForbidden = context =>
                        {
                            return Task.CompletedTask;
                        },
                    };
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Administrator", policy => policy.Requirements.Add(new RealmAccessRoleRequirement("administrator")));
            });

            // Generate the database connection string.
            var csBuilder = new SqlConnectionStringBuilder(this.Configuration.GetConnectionString("PIMS"));
            var pwd = this.Configuration["DB_PASSWORD"];
            if (!string.IsNullOrEmpty(pwd))
            {
                csBuilder.Password = pwd;
            }

            services.AddHttpClient();
            services.AddTransient<LoggingHandler>();
            services.AddHttpClient("Pims.Api.Logging").AddHttpMessageHandler<LoggingHandler>();
            services.AddPimsContext(this.Environment, csBuilder.ConnectionString);
            services.AddPimsDalRepositories();
            AddPimsApiRepositories(services);
            AddPimsApiServices(services);
            services.AddPimsKeycloakService();
            services.AddGeocoderService(this.Configuration.GetSection("Geocoder")); // TODO: PSP-4415 Determine if a default value could be used instead.
            services.AddLtsaService(this.Configuration.GetSection("Ltsa"));
            services.AddClamAvService(this.Configuration.GetSection("Av"));
            services.AddSingleton<IAuthorizationHandler, RealmAccessRoleHandler>();
            services.AddTransient<IClaimsTransformation, KeycloakClaimTransformer>();
            services.AddHttpContextAccessor();
            services.AddTransient<ClaimsPrincipal>(s => s.GetService<IHttpContextAccessor>().HttpContext.User);
            services.AddScoped<IProxyRequestClient, ProxyRequestClient>();
            services.AddScoped<IOpenIdConnectRequestClient, OpenIdConnectRequestClient>();
            int maxFileSize = int.Parse(this.Configuration.GetSection("Av")?["MaxFileSize"]);
            services.Configure<FormOptions>(x =>
            {
                x.ValueLengthLimit = maxFileSize;
                x.MultipartBodyLengthLimit = maxFileSize; // In case of multipart
            });

            services.AddHealthChecks()
                .AddCheck("liveliness", () => HealthCheckResult.Healthy())
                .AddSqlServer(csBuilder.ConnectionString, tags: new[] { "services" });

            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ApiVersionReader = new HeaderApiVersionReader("api-version");
            });
            services.AddVersionedApiExplorer(options =>
            {
                // add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
                // note: the specified format code will format the version as "'v'major[.minor][-status]"
                options.GroupNameFormat = "'v'VVV";

                // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
                // can also be used to control the format of the API version in route templates
                options.SubstituteApiVersionInUrl = true;
            });
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, Helpers.Swagger.ConfigureSwaggerOptions>();

            services.AddSwaggerGen(options =>
            {
                options.EnableAnnotations(false, true);
                options.CustomSchemaIds(o => o.FullName);
                options.OperationFilter<Helpers.Swagger.SwaggerDefaultValues>();
                options.DocumentFilter<Helpers.Swagger.SwaggerDocumentFilter>();
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Description = "Please enter into field the word 'Bearer' following by space and JWT",
                    Type = SecuritySchemeType.ApiKey,
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer",
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    },
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });

            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders = ForwardedHeaders.All;
                options.AllowedHosts = this.Configuration.GetValue<string>("AllowedHosts")?.Split(';').ToList<string>();
            });
            services.AddDatabaseDeveloperPageExceptionFilter();
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="provider"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            app.UseMetricServer();
            app.UseHttpMetrics();

            if (!env.IsProduction())
            {
                app.UseDeveloperExceptionPage();
            }

            var baseUrl = this.Configuration.GetValue<string>("BaseUrl");
            app.UsePathBase(baseUrl);
            app.UseForwardedHeaders();

            app.UseSwagger(options =>
            {
                options.RouteTemplate = this.Configuration.GetValue<string>("Swagger:RouteTemplate");
            });
            app.UseSwaggerUI(options =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint(string.Format(this.Configuration.GetValue<string>("Swagger:EndpointPath"), description.GroupName), description.GroupName);
                }
                options.RoutePrefix = this.Configuration.GetValue<string>("Swagger:RoutePrefix");
            });

            app.UseMiddleware<ResponseTimeMiddleware>();
            app.UseMiddleware<LogRequestMiddleware>();
            app.UseMiddleware<LogResponseMiddleware>();
            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.UseRouting();
            app.UseCors();

            app.UseAuthentication();
            app.UseAuthorization();

            var healthPort = this.Configuration.GetValue<int>("HealthChecks:Port");
            app.UseHealthChecks(this.Configuration.GetValue<string>("HealthChecks:LivePath"), healthPort, new HealthCheckOptions
            {
                Predicate = r => r.Name.Contains("liveliness"),
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
            });
            app.UseHealthChecks(this.Configuration.GetValue<string>("HealthChecks:ReadyPath"), healthPort, new HealthCheckOptions
            {
                Predicate = r => r.Tags.Contains("services"),
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
            });

            app.UseEndpoints(config =>
            {
                config.MapControllers();
            });
        }

        private static void AddPimsApiRepositories(IServiceCollection services)
        {
            services.AddSingleton<IEdmsAuthRepository, MayanAuthRepository>();
            services.AddScoped<IEdmsDocumentRepository, MayanDocumentRepository>();
            services.AddScoped<IEdmsMetadataRepository, MayanMetadataRepository>();
            services.AddScoped<IDocumentGenerationRepository, CdogsRepository>();
            services.AddSingleton<IDocumentGenerationAuthRepository, CdogsAuthRepository>();
        }

        /// <summary>
        /// Add PimsService objects to the dependency injection service collection.
        /// </summary>
        /// <param name="services"></param>
        private static void AddPimsApiServices(IServiceCollection services)
        {
            services.AddScoped<IDocumentService, DocumentService>();
            services.AddScoped<IDocumentActivityService, DocumentActivityService>();
            services.AddScoped<IDocumentLeaseService, DocumentLeaseService>();
            services.AddScoped<IDocumentSyncService, DocumentSyncService>();
            services.AddScoped<INoteService, NoteService>();
            services.AddScoped<IActivityService, ActivityService>();
            services.AddScoped<IAcquisitionFileService, AcquisitionFileService>();
            services.AddScoped<ILeaseService, LeaseService>();
            services.AddScoped<ILeaseReportsService, LeaseReportsService>();
            services.AddScoped<ILeaseTermService, LeaseTermService>();
            services.AddScoped<ILeasePaymentService, LeasePaymentService>();
            services.AddScoped<ISecurityDepositService, SecurityDepositService>();
            services.AddScoped<ISecurityDepositReturnService, SecurityDepositReturnService>();
            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<IOrganizationService, OrganizationService>();
            services.AddScoped<IResearchFileService, ResearchFileService>();
            services.AddScoped<IPropertyService, PropertyService>();
            services.AddScoped<ICoordinateTransformService, CoordinateTransformService>();
            services.AddScoped<IDocumentGenerationService, DocumentGenerationService>();
        }
        #endregion
    }
}
