using System;
using System.Runtime.Serialization;

namespace MigraineCSMiddleware.Service.Securite
{
    [Serializable]
    internal class SecuriteNullException : Exception
    {
        public SecuriteNullException()
        {
        }

        public SecuriteNullException(string message) : base(message)
        {
        }

        public SecuriteNullException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected SecuriteNullException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}