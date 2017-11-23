using System;
using System.Runtime.Serialization;

namespace MigraineCSMiddleware.Service.Utilisateur
{
    [Serializable]
    internal class CompteException : Exception
    {
        public CompteException()
        {
        }

        public CompteException(string message) : base(message)
        {
        }

        public CompteException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CompteException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}