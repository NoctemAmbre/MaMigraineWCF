using System;
using System.Runtime.Serialization;

namespace MigraineCSMiddleware.Service.securite
{
    [Serializable]
    internal class SecuritePasswordIncorecteException : Exception
    {
        public SecuritePasswordIncorecteException()
        {
        }

        public SecuritePasswordIncorecteException(string message) : base(message)
        {
        }

        public SecuritePasswordIncorecteException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected SecuritePasswordIncorecteException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}