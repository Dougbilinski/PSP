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
    /// CertificateDelivery
    /// </summary>
    [DataContract]
    public partial class CertificateDelivery
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CertificateDelivery" /> class.
        /// </summary>
        /// <param name="certificateText">Free text used to store recipient and/or delivery information for converted ALTOS1 certificates..</param>
        /// <param name="intendedRecipientLastName">The Last Name of the person who is going to hold (be responsible for) the certificate. (required).</param>
        /// <param name="intendedRecipientGivenName">The Given Name of the person who is going to hold (be responsible for) the certificate..</param>
        /// <param name="address">address.</param>
        public CertificateDelivery(string certificateText = default(string), string intendedRecipientLastName = default(string), string intendedRecipientGivenName = default(string), List<OwnerAddress> address = default(List<OwnerAddress>))
        {
            // to ensure "intendedRecipientLastName" is required (not null)
            if (intendedRecipientLastName == null)
            {
                throw new InvalidDataException("intendedRecipientLastName is a required property for CertificateDelivery and cannot be null");
            }
            else
            {
                this.IntendedRecipientLastName = intendedRecipientLastName;
            }
            this.CertificateText = certificateText;
            this.IntendedRecipientGivenName = intendedRecipientGivenName;
            this.Address = address;
        }

        /// <summary>
        /// Free text used to store recipient and/or delivery information for converted ALTOS1 certificates.
        /// </summary>
        /// <value>Free text used to store recipient and/or delivery information for converted ALTOS1 certificates.</value>
        [DataMember(Name = "certificateText", EmitDefaultValue = false)]
        public string CertificateText { get; set; }

        /// <summary>
        /// The Last Name of the person who is going to hold (be responsible for) the certificate.
        /// </summary>
        /// <value>The Last Name of the person who is going to hold (be responsible for) the certificate.</value>
        [DataMember(Name = "intendedRecipientLastName", EmitDefaultValue = false)]
        public string IntendedRecipientLastName { get; set; }

        /// <summary>
        /// The Given Name of the person who is going to hold (be responsible for) the certificate.
        /// </summary>
        /// <value>The Given Name of the person who is going to hold (be responsible for) the certificate.</value>
        [DataMember(Name = "intendedRecipientGivenName", EmitDefaultValue = false)]
        public string IntendedRecipientGivenName { get; set; }

        /// <summary>
        /// Gets or Sets Address
        /// </summary>
        [DataMember(Name = "address", EmitDefaultValue = false)]
        public List<OwnerAddress> Address { get; set; }
    }
}