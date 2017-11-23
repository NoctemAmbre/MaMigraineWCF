using System;
using System.Runtime.Serialization;

namespace MigraineCSMiddleware.DAO
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