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
    /// Canadian Province
    /// </summary>
    /// <value>Canadian Province</value>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum CanadianProvince
    {
        /// <summary>
        /// Enum ABALBERTA for value: AB - ALBERTA
        /// </summary>
        [EnumMember(Value = "AB - ALBERTA")]
        ABALBERTA = 1,
        /// <summary>
        /// Enum BCBRITISHCOLUMBIA for value: BC - BRITISH COLUMBIA
        /// </summary>
        [EnumMember(Value = "BC - BRITISH COLUMBIA")]
        BCBRITISHCOLUMBIA = 2,
        /// <summary>
        /// Enum MBMANITOBA for value: MB - MANITOBA
        /// </summary>
        [EnumMember(Value = "MB - MANITOBA")]
        MBMANITOBA = 3,
        /// <summary>
        /// Enum NBNEWBRUNSWICK for value: NB - NEW BRUNSWICK
        /// </summary>
        [EnumMember(Value = "NB - NEW BRUNSWICK")]
        NBNEWBRUNSWICK = 4,
        /// <summary>
        /// Enum NFNEWFOUNDLAND for value: NF - NEWFOUNDLAND
        /// </summary>
        [EnumMember(Value = "NF - NEWFOUNDLAND")]
        NFNEWFOUNDLAND = 5,
        /// <summary>
        /// Enum NSNOVASCOTIA for value: NS - NOVA SCOTIA
        /// </summary>
        [EnumMember(Value = "NS - NOVA SCOTIA")]
        NSNOVASCOTIA = 6,
        /// <summary>
        /// Enum NTNORTHWESTTERRITORIES for value: NT - NORTHWEST TERRITORIES
        /// </summary>
        [EnumMember(Value = "NT - NORTHWEST TERRITORIES")]
        NTNORTHWESTTERRITORIES = 7,
        /// <summary>
        /// Enum ONONTARIO for value: ON - ONTARIO
        /// </summary>
        [EnumMember(Value = "ON - ONTARIO")]
        ONONTARIO = 8,
        /// <summary>
        /// Enum PEPRINCEEDWARDISLAND for value: PE - PRINCE EDWARD ISLAND
        /// </summary>
        [EnumMember(Value = "PE - PRINCE EDWARD ISLAND")]
        PEPRINCEEDWARDISLAND = 9,
        /// <summary>
        /// Enum PQQUEBEC for value: PQ - QUEBEC
        /// </summary>
        [EnumMember(Value = "PQ - QUEBEC")]
        PQQUEBEC = 10,
        /// <summary>
        /// Enum SKSASKATCHEWAN for value: SK - SASKATCHEWAN
        /// </summary>
        [EnumMember(Value = "SK - SASKATCHEWAN")]
        SKSASKATCHEWAN = 11,
        /// <summary>
        /// Enum YTYUKON for value: YT - YUKON
        /// </summary>
        [EnumMember(Value = "YT - YUKON")]
        YTYUKON = 12
    }
}