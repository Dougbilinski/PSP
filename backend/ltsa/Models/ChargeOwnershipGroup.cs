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
    /// ChargeOwnershipGroup
    /// </summary>
    [DataContract]
    public partial class ChargeOwnershipGroup
    {
        /// <summary>
        /// Indicates the state with respect to the Transfer of Charge Ownership lifecycle.
        /// </summary>
        /// <value>Indicates the state with respect to the Transfer of Charge Ownership lifecycle.</value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum CreatingApplicationStatusEnum
        {
            /// <summary>
            /// Enum Draft for value: Draft
            /// </summary>
            [EnumMember(Value = "Draft")]
            Draft = 1,
            /// <summary>
            /// Enum Registered for value: Registered
            /// </summary>
            [EnumMember(Value = "Registered")]
            Registered = 2
        }
        /// <summary>
        /// Indicates the state with respect to the Transfer of Charge Ownership lifecycle.
        /// </summary>
        /// <value>Indicates the state with respect to the Transfer of Charge Ownership lifecycle.</value>
        [DataMember(Name = "creatingApplicationStatus", EmitDefaultValue = false)]
        public CreatingApplicationStatusEnum? CreatingApplicationStatus { get; set; }
        /// <summary>
        /// Indicates the state with respect to the Transfer of Charge Ownership lifecycle.
        /// </summary>
        /// <value>Indicates the state with respect to the Transfer of Charge Ownership lifecycle.</value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum CancellingApplicationStatusEnum
        {
            /// <summary>
            /// Enum Draft for value: Draft
            /// </summary>
            [EnumMember(Value = "Draft")]
            Draft = 1,
            /// <summary>
            /// Enum Registered for value: Registered
            /// </summary>
            [EnumMember(Value = "Registered")]
            Registered = 2
        }
        /// <summary>
        /// Indicates the state with respect to the Transfer of Charge Ownership lifecycle.
        /// </summary>
        /// <value>Indicates the state with respect to the Transfer of Charge Ownership lifecycle.</value>
        [DataMember(Name = "cancellingApplicationStatus", EmitDefaultValue = false)]
        public CancellingApplicationStatusEnum? CancellingApplicationStatus { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="ChargeOwnershipGroup" /> class.
        /// </summary>
        /// <param name="jointTenancyIndication">Indicator for joint tenancy (required).</param>
        /// <param name="creatingApplicationNumber">The Transfer of Charge Ownership application that initiated the creation of this charge ownership group..</param>
        /// <param name="creatingApplicationStatus">Indicates the state with respect to the Transfer of Charge Ownership lifecycle..</param>
        /// <param name="cancellingApplicationNumber">The Transfer of Charge Ownership application that cancelled this charge ownership group..</param>
        /// <param name="cancellingApplicationStatus">Indicates the state with respect to the Transfer of Charge Ownership lifecycle..</param>
        /// <param name="interestFractionNumerator">Interest Fraction Numerator. It is used if not 1 (1/1), assumed to be equal division amongst ownership groups unless specified otherwise..</param>
        /// <param name="interestFractionDenominator">Interest Fraction Denominator. It is used if not 1 (1/1), assumed to be equal division amongst ownership groups unless specified otherwise..</param>
        /// <param name="ownershipRemarks">Charge Ownership Remarks.</param>
        /// <param name="chargeOwners">chargeOwners (required).</param>
        public ChargeOwnershipGroup(bool? jointTenancyIndication = default(bool?), string creatingApplicationNumber = default(string), CreatingApplicationStatusEnum? creatingApplicationStatus = default(CreatingApplicationStatusEnum?), string cancellingApplicationNumber = default(string), CancellingApplicationStatusEnum? cancellingApplicationStatus = default(CancellingApplicationStatusEnum?), string interestFractionNumerator = default(string), string interestFractionDenominator = default(string), string ownershipRemarks = default(string), List<ChargeOwnershipGroupChargeOwner> chargeOwners = default(List<ChargeOwnershipGroupChargeOwner>))
        {
            // to ensure "jointTenancyIndication" is required (not null)
            if (jointTenancyIndication == null)
            {
                throw new InvalidDataException("jointTenancyIndication is a required property for ChargeOwnershipGroup and cannot be null");
            }
            else
            {
                this.JointTenancyIndication = jointTenancyIndication;
            }
            // to ensure "chargeOwners" is required (not null)
            if (chargeOwners == null)
            {
                throw new InvalidDataException("chargeOwners is a required property for ChargeOwnershipGroup and cannot be null");
            }
            else
            {
                this.ChargeOwners = chargeOwners;
            }
            this.CreatingApplicationNumber = creatingApplicationNumber;
            this.CreatingApplicationStatus = creatingApplicationStatus;
            this.CancellingApplicationNumber = cancellingApplicationNumber;
            this.CancellingApplicationStatus = cancellingApplicationStatus;
            this.InterestFractionNumerator = interestFractionNumerator;
            this.InterestFractionDenominator = interestFractionDenominator;
            this.OwnershipRemarks = ownershipRemarks;
        }

        /// <summary>
        /// Indicator for joint tenancy
        /// </summary>
        /// <value>Indicator for joint tenancy</value>
        [DataMember(Name = "jointTenancyIndication", EmitDefaultValue = false)]
        public bool? JointTenancyIndication { get; set; }

        /// <summary>
        /// The Transfer of Charge Ownership application that initiated the creation of this charge ownership group.
        /// </summary>
        /// <value>The Transfer of Charge Ownership application that initiated the creation of this charge ownership group.</value>
        [DataMember(Name = "creatingApplicationNumber", EmitDefaultValue = false)]
        public string CreatingApplicationNumber { get; set; }


        /// <summary>
        /// The Transfer of Charge Ownership application that cancelled this charge ownership group.
        /// </summary>
        /// <value>The Transfer of Charge Ownership application that cancelled this charge ownership group.</value>
        [DataMember(Name = "cancellingApplicationNumber", EmitDefaultValue = false)]
        public string CancellingApplicationNumber { get; set; }


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
        /// Charge Ownership Remarks
        /// </summary>
        /// <value>Charge Ownership Remarks</value>
        [DataMember(Name = "ownershipRemarks", EmitDefaultValue = false)]
        public string OwnershipRemarks { get; set; }

        /// <summary>
        /// Gets or Sets ChargeOwners
        /// </summary>
        [DataMember(Name = "chargeOwners", EmitDefaultValue = false)]
        public List<ChargeOwnershipGroupChargeOwner> ChargeOwners { get; set; }
    }
}