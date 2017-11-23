using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MigraineCSMiddleware.Modele
{
    public class Medicament
    {
        public int ID { get; set; }
        public int CodeCIS { get; set; }
        public string DenominationMedicment { get; set; }
        public string FromePharmaceutique { get; set; }
        public string VoieAdministration { get; set; }
        public string StatutAdministratif { get; set; }
        public string TypeDeProcedureAutorisation { get; set; }
        public string EtatCommercialisatoin { get; set; }
        public DateTime DateAmm { get; set; }
        public string StatutDbm { get; set; }
        public string NumeroAutorisation { get; set; }
        public string Titulaire { get; set; }
        public string SurveillanceRenforcee { get; set; }
        public int Quantite { get; set; }
    }
}