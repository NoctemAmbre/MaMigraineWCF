using MigraineCSMiddleware.Modele;
using System;
using System.Runtime.Serialization;

namespace MigraineCSMiddleware.Service.patient
{
    [Serializable]
    internal class PatientNonPresentException : Exception
    {
        private Medecin _medecin;
        public PatientNonPresentException()
        {
        }

        public PatientNonPresentException(string message, Medecin medecin) : base(message)
        {
            this.Medecin = medecin;
        }

        public PatientNonPresentException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected PatientNonPresentException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public Medecin Medecin { get => _medecin; set => _medecin = value; }
    }
}