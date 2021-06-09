/* 
 * Title Direct Search Services
 *
 * Title Direct Search Services
 *
 * OpenAPI spec version: 4.0.1
 * 
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */
using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;


namespace Pims.Ltsa.Models
{
    /// <summary>
    /// DuplicateCertificate
    /// </summary>
    [DataContract]
    public partial class DuplicateCertificate
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DuplicateCertificate" /> class.
        /// </summary>
        /// <param name="issuedDate">The date that the certificate is deemed to have been issued. (required).</param>
        /// <param name="surrenderDate">The date that the certificate was surrended..</param>
        /// <param name="certificateIdentifier">certificateIdentifier (required).</param>
        /// <param name="certificateDelivery">certificateDelivery (required).</param>
        public DuplicateCertificate(DateTime? issuedDate = default(DateTime?), DateTime? surrenderDate = default(DateTime?), CertificateIdentifier certificateIdentifier = default(CertificateIdentifier), CertificateDelivery certificateDelivery = default(CertificateDelivery))
        {
            // to ensure "issuedDate" is required (not null)
            if (issuedDate == null)
            {
                throw new InvalidDataException("issuedDate is a required property for DuplicateCertificate and cannot be null");
            }
            else
            {
                this.IssuedDate = issuedDate;
            }
            // to ensure "certificateIdentifier" is required (not null)
            if (certificateIdentifier == null)
            {
                throw new InvalidDataException("certificateIdentifier is a required property for DuplicateCertificate and cannot be null");
            }
            else
            {
                this.CertificateIdentifier = certificateIdentifier;
            }
            // to ensure "certificateDelivery" is required (not null)
            if (certificateDelivery == null)
            {
                throw new InvalidDataException("certificateDelivery is a required property for DuplicateCertificate and cannot be null");
            }
            else
            {
                this.CertificateDelivery = certificateDelivery;
            }
            this.SurrenderDate = surrenderDate;
        }

        /// <summary>
        /// The date that the certificate is deemed to have been issued.
        /// </summary>
        /// <value>The date that the certificate is deemed to have been issued.</value>
        [DataMember(Name = "issuedDate", EmitDefaultValue = false)]
        public DateTime? IssuedDate { get; set; }

        /// <summary>
        /// The date that the certificate was surrended.
        /// </summary>
        /// <value>The date that the certificate was surrended.</value>
        [DataMember(Name = "surrenderDate", EmitDefaultValue = false)]
        public DateTime? SurrenderDate { get; set; }

        /// <summary>
        /// Gets or Sets CertificateIdentifier
        /// </summary>
        [DataMember(Name = "certificateIdentifier", EmitDefaultValue = false)]
        public CertificateIdentifier CertificateIdentifier { get; set; }

        /// <summary>
        /// Gets or Sets CertificateDelivery
        /// </summary>
        [DataMember(Name = "certificateDelivery", EmitDefaultValue = false)]
        public CertificateDelivery CertificateDelivery { get; set; }
    }
}