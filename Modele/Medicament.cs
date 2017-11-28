using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MigraineCSMiddleware.Modele
{
    public class Medicament
    {
        private int _ID;
        //private int _CodeCIS;
        private string _DenominationMedicament;
        private string _FormePharmaceutique;
        private string _VoiesAdministration;
        //private string _StatutAdministratif;
        //private string _TypeDeProcedureAutorisation;
        //private string _EtatCommercialisatoin;
        //private DateTime _DateAmm;
        //private string _Statutbdm;
        //private string _NumeroAutorisation;
        //private string _Titulaire;
        //private string _SurveillanceRenforcee;
        //private int _Quantite;

        public int ID { get => _ID; set => _ID = value; }
        public string DenominationMedicament { get => _DenominationMedicament; set => _DenominationMedicament = value; }
        public string FormePharmaceutique { get => _FormePharmaceutique; set => _FormePharmaceutique = value; }
        public string VoiesAdministration { get => _VoiesAdministration; set => _VoiesAdministration = value; }
        //public int CodeCIS { get => _CodeCIS; set => _CodeCIS = value; }


        //public string StatutAdministratif { get => _StatutAdministratif; set => _StatutAdministratif = value; }
        //public string TypeDeProcedureAutorisation { get => _TypeDeProcedureAutorisation; set => _TypeDeProcedureAutorisation = value; }
        //public string EtatCommercialisatoin { get => _EtatCommercialisatoin; set => _EtatCommercialisatoin = value; }
        //public DateTime DateAmm { get => _DateAmm; set => _DateAmm = value; }
        //public string Statutbdm { get => _Statutbdm; set => _Statutbdm = value; }
        //public string NumeroAutorisation { get => _NumeroAutorisation; set => _NumeroAutorisation = value; }
        //public string Titulaire { get => _Titulaire; set => _Titulaire = value; }
        //public string SurveillanceRenforcee { get => _SurveillanceRenforcee; set => _SurveillanceRenforcee = value; }
        //public int Quantite { get => _Quantite; set => _Quantite = value; }
    }
}