using System;
using System.Runtime.Serialization;

namespace MigraineCSMiddleware.Service.compte
{
    [Serializable]
    internal class LoginTropLongException : Exception
    {
        private int _IdCompte;
        public LoginTropLongException()
        {
        }

        public LoginTropLongException(string message, int IdCompte) : base(message)
        {
            this.IdCompte = IdCompte;
        }
        public int IdCompte { get => _IdCompte; set => _IdCompte = value; }
    }
}