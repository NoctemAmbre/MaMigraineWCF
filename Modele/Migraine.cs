using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MigraineCSMiddleware.Modele
{
    public class Migraine
    {
        public int ID { get; set; }
        public int Intensite { get; set; }
        public string Debut { get; set; }
        public string Fin { get; set; }
        public List<Medicament> MedicamentsPris { get; set; }
        public List<Facteur> Facteurs { get; set; }
    }
}