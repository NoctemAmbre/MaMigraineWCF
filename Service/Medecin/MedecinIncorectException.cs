using MigraineCSMiddleware.Modele;
using System;
using System.Runtime.Serialization;

namespace MigraineCSMiddleware.Service.medecin
{
    [Serializable]
    internal class MedecinIncorrecteException : Exception
    {
        private Patient _patient;
        private Medecin _medecin;
        public MedecinIncorrecteException()
        {
        }

        public MedecinIncorrecteException(string message) : base(message)
        {
        }

        public MedecinIncorrecteException(string message, Patient patient) : base(message)
        {
            this.Patient = patient;
        }

        public MedecinIncorrecteException(string message, Medecin medecin) : base(message)
        {
            this.Medecin = medecin;
        }

        public MedecinIncorrecteException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MedecinIncorrecteException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public Patient Patient { get => _patient; set => _patient = value; }
        public Medecin Medecin { get => _medecin; set => _medecin = value; }
    }
}