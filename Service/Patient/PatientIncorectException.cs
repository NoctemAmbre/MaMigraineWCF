using MigraineCSMiddleware.Modele;
using System;
using System.Runtime.Serialization;

namespace MigraineCSMiddleware.Service.patient
{
    [Serializable]
    internal class PatientIncorrecteException : Exception
    {
        private Medecin _medecin;
        private Patient _patient;
        public PatientIncorrecteException()
        {
        }

        public PatientIncorrecteException(string message, Medecin medecin) : base(message)
        {
            this.Medecin = medecin;
        }
        public PatientIncorrecteException(string message, Patient patient) : base(message)
        {
            this.Patient = patient;
        }

        public PatientIncorrecteException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected PatientIncorrecteException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public Medecin Medecin { get => _medecin; set => _medecin = value; }
        public Patient Patient { get => _patient; set => _patient = value; }
    }
}