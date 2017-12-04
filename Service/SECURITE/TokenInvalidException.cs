using MigraineCSMiddleware.Modele;
using System;
using System.Runtime.Serialization;

namespace MigraineCSMiddleware.Service.securite
{
    [Serializable]
    internal class TokenInvalidException : Exception
    {
        private UtilisateurWeb _Utilisateurweb;

        public UtilisateurWeb Utilisateurweb { get => _Utilisateurweb; set => _Utilisateurweb = value; }

        public TokenInvalidException()
        {
        }

        public TokenInvalidException(string message) : base(message)
        {
        }
        
        public TokenInvalidException(UtilisateurWeb utilisateurweb, string message) : base(message)
        {
            utilisateurweb.Erreur = message;
            this._Utilisateurweb = utilisateurweb;
        }

        public TokenInvalidException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected TokenInvalidException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        
    }
}