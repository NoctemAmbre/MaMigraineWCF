using MigraineCSMiddleware.Modele;
using System;
using System.Runtime.Serialization;

namespace MigraineCSMiddleware.Service
{
    [Serializable]
    internal class DejaMedecinAttribueException : Exception
    {
        public DejaMedecinAttribueException()
        {
        }

        public DejaMedecinAttribueException(string message) : base(message)
        {
        }

        public DejaMedecinAttribueException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DejaMedecinAttribueException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}