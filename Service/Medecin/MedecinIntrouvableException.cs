using System;
using System.Runtime.Serialization;

namespace MigraineCSMiddleware.Service.medecin
{
    [Serializable]
    internal class MedecinIntrouvableException : Exception
    {
        public MedecinIntrouvableException()
        {
        }

        public MedecinIntrouvableException(string message) : base(message)
        {
        }

        public MedecinIntrouvableException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MedecinIntrouvableException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}