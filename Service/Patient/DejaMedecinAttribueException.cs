using MigraineCSMiddleware.Modele;
using System;
using System.Runtime.Serialization;

namespace MigraineCSMiddleware.Service
{
    [Serializable]
    internal class DejaMedecinAttribueException : Exception
    {
        private Patient _patient;

        public DejaMedecinAttribueException()
        {
        }

        public DejaMedecinAttribueException(string message) : base(message)
        {
        }

        public DejaMedecinAttribueException(string message, Patient patient) : base(message)
        {
            this.Patient = patient;
        }

        public DejaMedecinAttribueException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DejaMedecinAttribueException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public Patient Patient { get => _patient; set => _patient = value; }
    }
}