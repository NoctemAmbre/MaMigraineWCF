using MigraineCSMiddleware.Modele;
using System;
using System.Runtime.Serialization;

namespace MigraineCSMiddleware.Service.compte
{
    [Serializable]
    internal class CompteModificationException : Exception
    {
        public Compte Compte;
        public CompteModificationException()
        {
        }

        public CompteModificationException(Medecin medecin, string message) : base(message)
        {
            this.Compte = medecin;
        }
        public CompteModificationException(Patient patient, string message) : base(message)
        {
            this.Compte = patient;
        }

        public CompteModificationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CompteModificationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}