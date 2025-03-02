﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Pims.Dal.Entities
{
    [Keyless]
    [Table("BCA_DATA_ADVICE_SCHOOL_VALUE")]
    [Index(nameof(DataAdviceId), Name = "BCDSCV_DATA_ADVICE_ID_IDX")]
    public partial class BcaDataAdviceSchoolValue
    {
        [Column("DATA_ADVICE_ID")]
        public long? DataAdviceId { get; set; }
        [Column("PROPERTY_CLASS_CODE")]
        [StringLength(16)]
        public string PropertyClassCode { get; set; }
        [Column("PROPERTY_CLASS_DESCRIPTION")]
        [StringLength(255)]
        public string PropertyClassDescription { get; set; }
        [Column("PROPERTY_SUBCLASS_CODE")]
        [StringLength(16)]
        public string PropertySubclassCode { get; set; }
        [Column("PROPERTY_SUBCLASS_DESCRIPTION")]
        [StringLength(255)]
        public string PropertySubclassDescription { get; set; }
        [Column("GROSS_LAND_VALUE", TypeName = "money")]
        public decimal? GrossLandValue { get; set; }
        [Column("GROSS_IMPROVEMENT_VALUE", TypeName = "money")]
        public decimal? GrossImprovementValue { get; set; }
        [Column("TAX_EXEMPT_LAND_VALUE", TypeName = "money")]
        public decimal? TaxExemptLandValue { get; set; }
        [Column("TAX_EXEMPT_IMPROVEMENT_VALUE", TypeName = "money")]
        public decimal? TaxExemptImprovementValue { get; set; }
        [Column("NET_LAND_VALUE", TypeName = "money")]
        public decimal? NetLandValue { get; set; }
        [Column("NET_IMPROVEMENT_VALUE", TypeName = "money")]
        public decimal? NetImprovementValue { get; set; }
        [Column("DB_CREATE_TIMESTAMP", TypeName = "datetime")]
        public DateTime DbCreateTimestamp { get; set; }
        [Required]
        [Column("DB_CREATE_USERID")]
        [StringLength(30)]
        public string DbCreateUserid { get; set; }
        [Column("DB_LAST_UPDATE_TIMESTAMP", TypeName = "datetime")]
        public DateTime DbLastUpdateTimestamp { get; set; }
        [Required]
        [Column("DB_LAST_UPDATE_USERID")]
        [StringLength(30)]
        public string DbLastUpdateUserid { get; set; }

        [ForeignKey(nameof(DataAdviceId))]
        public virtual BcaDataAdvice DataAdvice { get; set; }
    }
}
