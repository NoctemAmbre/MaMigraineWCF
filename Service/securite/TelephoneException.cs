using System;
using System.Runtime.Serialization;
using MigraineCSMiddleware.Modele;

namespace MigraineCSMiddleware.Service.securite
{
    [Serializable]
    internal class TelephoneException : Exception
    {
        private UtilisateurWeb _Utilisateurweb;
        public UtilisateurWeb Utilisateurweb { get => _Utilisateurweb; set => _Utilisateurweb = value; }

        public TelephoneException()
        {
        }

        public TelephoneException(string message) : base(message)
        {
        }

        public TelephoneException(UtilisateurWeb utilisateur, string message)
        {
            Utilisateurweb.Erreur = message;
            this.Utilisateurweb = Utilisateurweb;
        }

        public TelephoneException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected TelephoneException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}