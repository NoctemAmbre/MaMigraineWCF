using System;
using System.Runtime.Serialization;
using MigraineCSMiddleware.Modele;

namespace MigraineCSMiddleware.Service.utilisateurweb
{
    [Serializable]
    internal class UtilisateurWebInexistantException : Exception
    {
        private string v;
        private UtilisateurWeb _UtilisateurWeb;

        public UtilisateurWebInexistantException()
        {
        }

        public UtilisateurWebInexistantException(string message, UtilisateurWeb UtilisateurWeb) : base(message)
        {
            this.UtilisateurWeb = UtilisateurWeb;
            this.UtilisateurWeb.Erreur = message;
        }

        public UtilisateurWebInexistantException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UtilisateurWebInexistantException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public UtilisateurWeb UtilisateurWeb { get => _UtilisateurWeb; set => _UtilisateurWeb = value; }
    }
}