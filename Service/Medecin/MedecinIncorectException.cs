using MigraineCSMiddleware.Modele;
using System;
using System.Runtime.Serialization;

namespace MigraineCSMiddleware.Service
{
    [Serializable]
    internal class MedecinIncorectException : Exception
    {
        public MedecinIncorectException()
        {
        }

        public MedecinIncorectException(string message) : base(message)
        {
        }

        public MedecinIncorectException(Medecin medecin, string message) : base(message)
        {
        }

        public MedecinIncorectException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MedecinIncorectException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}