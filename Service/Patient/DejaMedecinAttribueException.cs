using MigraineCSMiddleware.Modele;
using System;
using System.Runtime.Serialization;

namespace MigraineCSMiddleware.Service.patient
{
    [Serializable]
    internal class DejaMedecinAttribueException : Exception
    {
        private Medecin _medecin;

        public DejaMedecinAttribueException()
        {
        }

        public DejaMedecinAttribueException(string message) : base(message)
        {
        }

        public DejaMedecinAttribueException(string message, Medecin medecin) : base(message)
        {
            this.Medecin = medecin;
        }

        public DejaMedecinAttribueException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DejaMedecinAttribueException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public Medecin Medecin { get => _medecin; set => _medecin = value; }
    }
}