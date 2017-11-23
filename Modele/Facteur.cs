using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MigraineCSMiddleware.Modele
{
    public class Facteur
    {
        public FACTEUR TypeFacteur { get; set; }
        public string Nom { get; set; }
        public string Question { get; set; }
        public int Quantite { get; set; }

        public enum FACTEUR
        {
            AGRAVANT, AMELIORANT
        };
    }
    
}