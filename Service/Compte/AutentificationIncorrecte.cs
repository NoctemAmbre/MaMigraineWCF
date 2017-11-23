using System;
using System.Runtime.Serialization;

namespace MigraineCSMiddleware.DAO
{
    [Serializable]
    internal class AutentificationIncorrecteException : Exception
    {
        private string _Identifiant;

        public string Identifiant { get => _Identifiant; set => _Identifiant = value; }
 

        public AutentificationIncorrecteException()
        {
            
        }

        public AutentificationIncorrecteException(string message) : base(message)
        {
            this.Identifiant = "";
        }

        public AutentificationIncorrecteException(string Identifiant, string message) : base(message)
        {
            this.Identifiant = Identifiant;
        }

        public AutentificationIncorrecteException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AutentificationIncorrecteException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}