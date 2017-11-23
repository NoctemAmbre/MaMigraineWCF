using MigraineCSMiddleware.DAO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MigraineCSMiddleware.Modele
{
    public class Patient : Compte
    {
        private int _IDPatient;
        private DateTime _DateNaissance;
        private List<Medecin> _MesMedecin;
        private string _TelephonePortable;
        private bool _Sexe;
        public List<Facteur> MesFacteurs { get; set; }
        public List<Migraine> MesMigraines { get; set; }
        public List<Medicament> MesMedicaments { get; set; }

        public Patient()
        {
            //base.Token = Compte.GenerateToken();
        }

        public int IDPatient { get => _IDPatient; set => _IDPatient = value; }
        public DateTime DateNaissance { get => _DateNaissance; set => _DateNaissance = value; }
        public List<Medecin> MesMedecin { get => _MesMedecin; set => _MesMedecin = value;}
        //public Medecin MesMedecin { get { if (_MedecinAttitre == null) _MedecinAttitre = new Medecin() { ID = -1 }; return _MedecinAttitre; } set { _MedecinAttitre = value; } }
        public string TelephonePortable { get => _TelephonePortable; set => _TelephonePortable = value; }
        public bool Sexe { get => _Sexe; set => _Sexe = value; }
    }
}