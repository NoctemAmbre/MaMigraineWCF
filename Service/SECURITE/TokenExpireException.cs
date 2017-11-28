using System;
using System.Runtime.Serialization;

namespace MigraineCSMiddleware.Service.securite
{
    [Serializable]
    internal class TokenExpireException : Exception
    {
        public TokenExpireException()
        {
        }

        public TokenExpireException(string message) : base(message)
        {
        }

        public TokenExpireException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected TokenExpireException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}