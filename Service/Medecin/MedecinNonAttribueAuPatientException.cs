using System;
using System.Runtime.Serialization;

namespace MigraineCSMiddleware.Service.medecin
{
    [Serializable]
    internal class MedecinNonAttribueAuPatientException : Exception
    {
        public MedecinNonAttribueAuPatientException()
        {
        }

        public MedecinNonAttribueAuPatientException(string message) : base(message)
        {
        }

        public MedecinNonAttribueAuPatientException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MedecinNonAttribueAuPatientException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}