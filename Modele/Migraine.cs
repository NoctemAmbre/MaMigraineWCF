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
        public DateTime Debut { get; set; }
        public DateTime Fin { get; set; }
        public List<Medicament> MedicamentsPris { get; set; }
        public List<Facteur> Facteurs { get; set; }
    }
}