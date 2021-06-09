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
    /// TitleOwnershipGroup
    /// </summary>
    [DataContract]
    public partial class TitleOwnershipGroup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TitleOwnershipGroup" /> class.
        /// </summary>
        /// <param name="jointTenancyIndication">Indicator for joint tenancy.</param>
        /// <param name="interestFractionNumerator">Interest Fraction Numerator. It is used if not 1 (1/1), assumed to be equal division amongst ownership groups unless specified otherwise..</param>
        /// <param name="interestFractionDenominator">Interest Fraction Denominator. It is used if not 1 (1/1), assumed to be equal division amongst ownership groups unless specified otherwise..</param>
        /// <param name="ownershipRemarks">Remarks on title ownership.  It may contain an address from ALTOS1. Can also be used for other purposes, e.g., to indicate Corporate Sole, Trust.  (required).</param>
        /// <param name="titleOwners">titleOwners (required).</param>
        public TitleOwnershipGroup(string jointTenancyIndication = default(string), string interestFractionNumerator = default(string), string interestFractionDenominator = default(string), string ownershipRemarks = default(string), List<TitleOwner> titleOwners = default(List<TitleOwner>))
        {
            // to ensure "ownershipRemarks" is required (not null)
            if (ownershipRemarks == null)
            {
                throw new InvalidDataException("ownershipRemarks is a required property for TitleOwnershipGroup and cannot be null");
            }
            else
            {
                this.OwnershipRemarks = ownershipRemarks;
            }
            // to ensure "titleOwners" is required (not null)
            if (titleOwners == null)
            {
                throw new InvalidDataException("titleOwners is a required property for TitleOwnershipGroup and cannot be null");
            }
            else
            {
                this.TitleOwners = titleOwners;
            }
            this.JointTenancyIndication = jointTenancyIndication;
            this.InterestFractionNumerator = interestFractionNumerator;
            this.InterestFractionDenominator = interestFractionDenominator;
        }

        /// <summary>
        /// Indicator for joint tenancy
        /// </summary>
        /// <value>Indicator for joint tenancy</value>
        [DataMember(Name = "jointTenancyIndication", EmitDefaultValue = false)]
        public string JointTenancyIndication { get; set; }

        /// <summary>
        /// Interest Fraction Numerator. It is used if not 1 (1/1), assumed to be equal division amongst ownership groups unless specified otherwise.
        /// </summary>
        /// <value>Interest Fraction Numerator. It is used if not 1 (1/1), assumed to be equal division amongst ownership groups unless specified otherwise.</value>
        [DataMember(Name = "interestFractionNumerator", EmitDefaultValue = false)]
        public string InterestFractionNumerator { get; set; }

        /// <summary>
        /// Interest Fraction Denominator. It is used if not 1 (1/1), assumed to be equal division amongst ownership groups unless specified otherwise.
        /// </summary>
        /// <value>Interest Fraction Denominator. It is used if not 1 (1/1), assumed to be equal division amongst ownership groups unless specified otherwise.</value>
        [DataMember(Name = "interestFractionDenominator", EmitDefaultValue = false)]
        public string InterestFractionDenominator { get; set; }

        /// <summary>
        /// Remarks on title ownership.  It may contain an address from ALTOS1. Can also be used for other purposes, e.g., to indicate Corporate Sole, Trust. 
        /// </summary>
        /// <value>Remarks on title ownership.  It may contain an address from ALTOS1. Can also be used for other purposes, e.g., to indicate Corporate Sole, Trust. </value>
        [DataMember(Name = "ownershipRemarks", EmitDefaultValue = false)]
        public string OwnershipRemarks { get; set; }

        /// <summary>
        /// Gets or Sets TitleOwners
        /// </summary>
        [DataMember(Name = "titleOwners", EmitDefaultValue = false)]
        public List<TitleOwner> TitleOwners { get; set; }
    }
}