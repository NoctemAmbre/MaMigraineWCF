using MigraineCSMiddleware.Modele;
using System;
using System.Runtime.Serialization;

namespace MigraineCSMiddleware.Service.medecin
{
    [Serializable]
    internal class MedecinNonAttribueAuPatientException : Exception
    {
        private Medecin _medecin;
        public MedecinNonAttribueAuPatientException()
        {
        }

        public MedecinNonAttribueAuPatientException(string message, Medecin medecin) : base(message)
        {
            this.Medecin = medecin;
        }

        public MedecinNonAttribueAuPatientException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MedecinNonAttribueAuPatientException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public Medecin Medecin { get => _medecin; set => _medecin = value; }
    }
}