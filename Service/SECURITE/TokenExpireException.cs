using MigraineCSMiddleware.Modele;
using System;
using System.Runtime.Serialization;

namespace MigraineCSMiddleware.Service.securite
{
    [Serializable]
    internal class TokenExpireException : Exception
    {
        private UtilisateurWeb _Utilisateurweb;
        public UtilisateurWeb Utilisateurweb { get => _Utilisateurweb; set => _Utilisateurweb = value; }

        public TokenExpireException()
        {
        }

        public TokenExpireException(string message) : base(message)
        {
        }

        public TokenExpireException(UtilisateurWeb Utilisateurweb, string message)
        {
            Utilisateurweb.Erreur = "Votre Token a expiré";
            this.Utilisateurweb = Utilisateurweb;
        }

        public TokenExpireException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected TokenExpireException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        
    }
}