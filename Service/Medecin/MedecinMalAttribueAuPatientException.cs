using System;
using System.Runtime.Serialization;

namespace MigraineCSMiddleware.Service.medecin
{
    [Serializable]
    internal class MedecinMalAttribueAuPatientException : Exception
    {
        public MedecinMalAttribueAuPatientException()
        {
        }

        public MedecinMalAttribueAuPatientException(string message) : base(message)
        {
        }

        public MedecinMalAttribueAuPatientException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MedecinMalAttribueAuPatientException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}