using System;
using System.Runtime.Serialization;

namespace Pims.Dal.Exceptions
{
    /// <summary>
    /// NotAuthorizedException class, provides a way to throw an exception when a user is not authorized to perform an action.
    /// </summary>
    [Serializable]
    public class NotAuthorizedException : Exception
    {
        #region Constructors

        /// <summary>
        /// Creates a new instance of a NotAuthorizedException class.
        /// </summary>
        /// <returns></returns>
        public NotAuthorizedException()
            : base()
        {
        }

        /// <summary>
        /// Creates a new instance of a NotAuthorizedException class, and initializes it with the specified arguments.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public NotAuthorizedException(string message)
            : base(message ?? "User is not authorized to perform this action.")
        {
        }

        /// <summary>
        /// Creates a new instance of a NotAuthorizedException class, and initializes it with the specified arguments.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        /// <returns></returns>
        public NotAuthorizedException(string message, Exception innerException)
            : base(message ?? "User is not authorized to perform this action.", innerException)
        {
        }

        /// <summary>
        /// Creates a new instance of a NotAuthorizedException class, and initializes it with the specified arguments.
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        protected NotAuthorizedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
        #endregion
    }
}
