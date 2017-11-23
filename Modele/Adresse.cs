using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MigraineCSMiddleware.Modele
{
    public class Adresse
    {
        private int _IdCompte;
        private int _Numero;
        private string _NomRue;
        private int _CodePostal;
        private string _Ville;

        public int IdCompte { get => _IdCompte; set => _IdCompte = value; }
        public int Numero { get => _Numero; set => _Numero = value; }
        public string NomRue { get => _NomRue; set => _NomRue = value; }
        public int CodePostal { get => _CodePostal; set => _CodePostal = value; }
        public string Ville { get => _Ville; set => _Ville = value; }
    }
}