
using Pims.Api.Models;

namespace Pims.Api.Areas.Persons.Models.Person
{
    /// <summary>
    /// Provides a contact-oriented contact method model.
    /// </summary>
    public class ContactMethodCreateModel
    {
        #region Properties
        /// <summary>
        /// get/set - The primary key to identify the contact method.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// get/set - The concurrency row version.
        /// </summary>
        /// <value></value>
        public long RowVersion { get; set; }

        /// <summary>
        /// get/set - Foreign key to the contact method type.
        /// </summary>
        public TypeModel<string> ContactMethodTypeCode { get; set; }

        /// <summary>
        /// get/set - The contact method value.
        /// </summary>
        public string Value { get; set; }
        #endregion
    }
}