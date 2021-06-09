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
    /// LegalNotationOnTitle
    /// </summary>
    [DataContract]
    public partial class LegalNotationOnTitle
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LegalNotationOnTitle" /> class.
        /// </summary>
        /// <param name="legalNotationNumber">Legal Notation Number (required).</param>
        /// <param name="status">State of the Legal Notation on Title. A Legal Notation is cancelled by a subsequent \&quot;\&quot;CAN\&quot;\&quot; Legal Notation. A Legal Notation may be modified by a subsequent \&quot;\&quot;MOD\&quot;\&quot; Legal Notation. This relationship is captured by the Legal Notation Entity. Modifications and Cancellations are Title specific.  (required).</param>
        /// <param name="cancellationDate">Legal Notation Cancellation Date - only appears on a historic view of the title..</param>
        /// <param name="legalNotation">legalNotation (required).</param>
        public LegalNotationOnTitle(string legalNotationNumber = default(string), string status = default(string), string cancellationDate = default(string), LegalNotation legalNotation = default(LegalNotation))
        {
            // to ensure "legalNotationNumber" is required (not null)
            if (legalNotationNumber == null)
            {
                throw new InvalidDataException("legalNotationNumber is a required property for LegalNotationOnTitle and cannot be null");
            }
            else
            {
                this.LegalNotationNumber = legalNotationNumber;
            }
            // to ensure "status" is required (not null)
            if (status == null)
            {
                throw new InvalidDataException("status is a required property for LegalNotationOnTitle and cannot be null");
            }
            else
            {
                this.Status = status;
            }
            // to ensure "legalNotation" is required (not null)
            if (legalNotation == null)
            {
                throw new InvalidDataException("legalNotation is a required property for LegalNotationOnTitle and cannot be null");
            }
            else
            {
                this.LegalNotation = legalNotation;
            }
            this.CancellationDate = cancellationDate;
        }

        /// <summary>
        /// Legal Notation Number
        /// </summary>
        /// <value>Legal Notation Number</value>
        [DataMember(Name = "legalNotationNumber", EmitDefaultValue = false)]
        public string LegalNotationNumber { get; set; }

        /// <summary>
        /// State of the Legal Notation on Title. A Legal Notation is cancelled by a subsequent \&quot;\&quot;CAN\&quot;\&quot; Legal Notation. A Legal Notation may be modified by a subsequent \&quot;\&quot;MOD\&quot;\&quot; Legal Notation. This relationship is captured by the Legal Notation Entity. Modifications and Cancellations are Title specific. 
        /// </summary>
        /// <value>State of the Legal Notation on Title. A Legal Notation is cancelled by a subsequent \&quot;\&quot;CAN\&quot;\&quot; Legal Notation. A Legal Notation may be modified by a subsequent \&quot;\&quot;MOD\&quot;\&quot; Legal Notation. This relationship is captured by the Legal Notation Entity. Modifications and Cancellations are Title specific. </value>
        [DataMember(Name = "status", EmitDefaultValue = false)]
        public string Status { get; set; }

        /// <summary>
        /// Legal Notation Cancellation Date - only appears on a historic view of the title.
        /// </summary>
        /// <value>Legal Notation Cancellation Date - only appears on a historic view of the title.</value>
        [DataMember(Name = "cancellationDate", EmitDefaultValue = false)]
        public string CancellationDate { get; set; }

        /// <summary>
        /// Gets or Sets LegalNotation
        /// </summary>
        [DataMember(Name = "legalNotation", EmitDefaultValue = false)]
        public LegalNotation LegalNotation { get; set; }
    }
}