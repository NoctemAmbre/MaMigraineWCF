using System;
using System.Runtime.Serialization;

namespace MigraineCSMiddleware.Service.Securite
{
    [Serializable]
    internal class SecuriteLoginIncorecteException : Exception
    {
        public SecuriteLoginIncorecteException()
        {
        }

        public SecuriteLoginIncorecteException(string message) : base(message)
        {
        }

        public SecuriteLoginIncorecteException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected SecuriteLoginIncorecteException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}