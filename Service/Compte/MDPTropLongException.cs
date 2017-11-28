using System;
using System.Runtime.Serialization;

namespace MigraineCSMiddleware.Service.compte
{
    [Serializable]
    internal class MDPTropLongException : Exception
    {
        private int _IdCompte;
        public MDPTropLongException()
        {
        }

        public MDPTropLongException(string message, int IdCompte) : base(message)
        {
            this.IdCompte = IdCompte;
        }
        public int IdCompte { get => _IdCompte; set => _IdCompte = value; }
    }
}