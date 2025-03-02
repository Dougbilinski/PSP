using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using MoreLinq;
using Pims.Dal.Constants;
using Pims.Dal.Entities;
using Pims.Dal.Exceptions;
using Pims.Dal.Helpers;
using Pims.Dal.Repositories;

namespace Pims.Api.Services
{
    public class LeaseService : ILeaseService
    {
        private readonly ILogger _logger;
        private readonly ILeaseRepository _leaseRepository;
        private readonly ICoordinateTransformService _coordinateService;
        private readonly IPropertyRepository _propertyRepository;
        private readonly IPropertyLeaseRepository _propertyLeaseRepository;

        public LeaseService(
            ILogger<LeaseService> logger,
            ILeaseRepository leaseRepository,
            ICoordinateTransformService coordinateTransformService,
            IPropertyRepository propertyRepository,
            IPropertyLeaseRepository propertyLeaseRepository)
        {
            _logger = logger;
            _leaseRepository = leaseRepository;
            _coordinateService = coordinateTransformService;
            _propertyRepository = propertyRepository;
            _propertyLeaseRepository = propertyLeaseRepository;
        }

        public bool IsRowVersionEqual(long leaseId, long rowVersion)
        {
            long currentRowVersion = _leaseRepository.GetRowVersion(leaseId);
            return currentRowVersion == rowVersion;
        }

        public PimsLease GetById(long leaseId)
        {
            var lease = _leaseRepository.Get(leaseId);
            foreach (PimsPropertyLease propertyLease in lease.PimsPropertyLeases)
            {
                var property = propertyLease.Property;
                if (property?.Location != null)
                {
                    var newCoords = _coordinateService.TransformCoordinates(SpatialReference.BC_ALBERS, SpatialReference.WGS_84, property.Location.Coordinate);
                    property.Location = GeometryHelper.CreatePoint(newCoords, SpatialReference.WGS_84);
                }
            }
            return lease;
        }

        public PimsLease Add(PimsLease lease, bool userOverride = false)
        {
            var leasesWithProperties = AssociatePropertyLeases(lease, userOverride);
            return _leaseRepository.Add(leasesWithProperties, userOverride);
        }

        public PimsLease Update(PimsLease lease, bool userOverride = false)
        {
            _leaseRepository.Update(lease, false);
            var leaseWithProperties = AssociatePropertyLeases(lease, userOverride);
            var updatedLease = _leaseRepository.UpdatePropertyLeases(lease.Id, lease.ConcurrencyControlNumber, leaseWithProperties.PimsPropertyLeases, userOverride);
            _leaseRepository.CommitTransaction();
            return updatedLease;
        }

        /// <summary>
        /// Attempt to associate property leases with real properties in the system using the pid/pin identifiers.
        /// Do not attempt to update any preexisiting properties, simply refer to them by id.
        ///
        /// By default, do not allow a property with existing leases to be associated unless the userOverride flag is true.
        /// </summary>
        /// <param name="lease"></param>
        /// <param name="userOverride"></param>
        /// <returns></returns>
        private PimsLease AssociatePropertyLeases(PimsLease lease, bool userOverride = false)
        {
            MatchProperties(lease);

            lease.PimsPropertyLeases.ForEach(propertyLease =>
            {
                PimsProperty property = propertyLease.Property;
                var existingPropertyLeases = _propertyLeaseRepository.GetAllByPropertyId(property.PropertyId);
                var isPropertyOnOtherLease = existingPropertyLeases.Any(p => p.LeaseId != lease.Id);
                var isPropertyOnThisLease = existingPropertyLeases.Any(p => p.LeaseId == lease.Id);
                if (isPropertyOnOtherLease && !isPropertyOnThisLease && !userOverride)
                {
                    var genericOverrideErrorMsg = $"is attached to L-File # {existingPropertyLeases.FirstOrDefault().Lease.LFileNo}";
                    if (propertyLease?.Property?.Pin != null)
                    {
                        throw new UserOverrideException($"PIN {propertyLease?.Property?.Pin.ToString() ?? string.Empty} {genericOverrideErrorMsg}");
                    }
                    if (propertyLease?.Property?.Pid != null)
                    {
                        throw new UserOverrideException($"PID {propertyLease?.Property?.Pid.ToString() ?? string.Empty} {genericOverrideErrorMsg}");
                    }
                    throw new UserOverrideException($"Lng/Lat {propertyLease?.Property?.Location.Coordinate.X.ToString() ?? string.Empty}, " +
                        $"{propertyLease?.Property?.Location.Coordinate.Y.ToString() ?? string.Empty} {genericOverrideErrorMsg}");
                }

                // If the property exist dont update it, just refer to it by id.
                if (property.Id != 0)
                {
                    propertyLease.PropertyId = property.PropertyId;
                    propertyLease.Property = null;
                }
            });

            return lease;
        }

        private void MatchProperties(PimsLease lease)
        {
            foreach (var leaseProperty in lease.PimsPropertyLeases)
            {
                if (leaseProperty.Property.Pid.HasValue)
                {
                    var pid = leaseProperty.Property.Pid.Value;
                    try
                    {
                        var foundProperty = _propertyRepository.GetByPid(pid);
                        leaseProperty.PropertyId = foundProperty.Id;
                        leaseProperty.Property = foundProperty;
                    }
                    catch (KeyNotFoundException)
                    {
                        _logger.LogDebug("Adding new property with pid:{pid}", pid);
                        PopulateNewProperty(leaseProperty.Property);
                    }
                }
                else if (leaseProperty.Property.Pin.HasValue)
                {
                    var pin = leaseProperty.Property.Pin.Value;
                    try
                    {
                        var foundProperty = _propertyRepository.GetByPin(pin);
                        leaseProperty.PropertyId = foundProperty.Id;
                        leaseProperty.Property = foundProperty;
                    }
                    catch (KeyNotFoundException)
                    {
                        _logger.LogDebug("Adding new property with pin:{pin}", pin);
                        PopulateNewProperty(leaseProperty.Property);
                    }
                }
                else
                {
                    _logger.LogDebug("Adding new property without a pid or pin");
                    PopulateNewProperty(leaseProperty.Property);
                }
            }
        }

        private void PopulateNewProperty(PimsProperty property)
        {
            property.PropertyClassificationTypeCode = "UNKNOWN";
            property.PropertyDataSourceEffectiveDate = System.DateTime.Now;
            property.PropertyDataSourceTypeCode = "PMBC";

            property.PropertyTypeCode = "UNKNOWN";

            property.PropertyStatusTypeCode = "UNKNOWN";
            property.SurplusDeclarationTypeCode = "UNKNOWN";

            property.IsPropertyOfInterest = false;

            // convert spatial location from lat/long (4326) to BC Albers (3005) for database storage
            var geom = property.Location;
            if (geom.SRID != SpatialReference.BC_ALBERS)
            {
                var newCoords = _coordinateService.TransformCoordinates(geom.SRID, SpatialReference.BC_ALBERS, geom.Coordinate);
                property.Location = GeometryHelper.CreatePoint(newCoords, SpatialReference.BC_ALBERS);
            }
        }
    }
}
