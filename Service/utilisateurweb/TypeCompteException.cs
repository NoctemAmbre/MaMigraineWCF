using System;
using System.Runtime.Serialization;
using MigraineCSMiddleware.Modele;

namespace MigraineCSMiddleware.Service.utilisateurweb
{
    [Serializable]
    internal class TypeCompteException : Exception
    {
        private UtilisateurWeb _Utilisateurweb;
        public UtilisateurWeb Utilisateurweb { get => _Utilisateurweb; set => _Utilisateurweb = value; }

        public TypeCompteException()
        {
        }

        public TypeCompteException(string message) : base(message)
        {
        }

        public TypeCompteException(UtilisateurWeb utilisateurWeb, string message)
        {
            Utilisateurweb.Erreur = message;
            this.Utilisateurweb = Utilisateurweb;
        }

        public TypeCompteException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected TypeCompteException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}