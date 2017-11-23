using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MigraineCSMiddleware.Modele
{
    public class Medecin : Compte
    {
        private int _IDMedecin;
        private string _InfoComplementaire;
        private Horaire[] _HoraireOuverture;
        private List<Patient> _MesPatient;

        public int IDMedecin { get => _IDMedecin; set => _IDMedecin = value; }
        public string InfoComplementaire { get => _InfoComplementaire; set => _InfoComplementaire = value; }
        public Horaire[] HoraireOuverture { get => _HoraireOuverture; set => _HoraireOuverture = value; }
        public List<Patient> MesPatient { get => _MesPatient; set => _MesPatient = value; }
    }
}