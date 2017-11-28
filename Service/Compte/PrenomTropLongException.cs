using System;
using System.Runtime.Serialization;

namespace MigraineCSMiddleware.Service.compte
{
    [Serializable]
    internal class PrenomTropLongException : Exception
    {
        private int _IdCompte;
        public PrenomTropLongException()
        {
        }

        public PrenomTropLongException(string message, int IdCompte) : base(message)
        {
            this.IdCompte = IdCompte;
        }

        public int IdCompte { get => _IdCompte; set => _IdCompte = value; }
    }
}