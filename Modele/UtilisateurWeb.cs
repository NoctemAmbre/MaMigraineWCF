using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MigraineCSMiddleware.Modele
{
    public class UtilisateurWeb : Compte
    {
        private int _IDWeb;
        private bool _Type;
        private bool _Sexe;
        private string _DateNaissance;
        private List<UtilisateurWeb> _MesMedecin;
        private string _TelephonePortable;
        private string _InfoComplementaire;
        private Horaire[] _HoraireOuverture;
        private List<UtilisateurWeb> _MesPatients;
        private List<Migraine> _MesMigraines;
        private List<Medicament> _MesMedicaments;
        private List<Facteur> _MesFacteurs;

        public int IDWeb { get => _IDWeb; set => _IDWeb = value; }
        public bool Type { get => _Type; set => _Type = value; }
        public bool Sexe { get => _Sexe; set => _Sexe = value; }
        public string DateNaissance { get => _DateNaissance; set => _DateNaissance = value; }
        public List<UtilisateurWeb> MesMedecin { get => _MesMedecin; set => _MesMedecin = value; }
        public string TelephonePortable { get => _TelephonePortable; set => _TelephonePortable = value; }
        public string InfoComplementaire { get => _InfoComplementaire; set => _InfoComplementaire = value; }
        public Horaire[] HoraireOuverture { get => _HoraireOuverture; set => _HoraireOuverture = value; }
        public List<UtilisateurWeb> MesPatients { get => _MesPatients; set => _MesPatients = value; }
        public List<Migraine> MesMigraines { get => _MesMigraines; set => _MesMigraines = value; }
        public List<Medicament> MesMedicaments { get => _MesMedicaments; set => _MesMedicaments = value; }
        public List<Facteur> MesFacteurs { get => _MesFacteurs; set => _MesFacteurs = value; }
    }

    // a détruire au plus vite
    public class UtilisateurTest :Compte
    {
        private Horaire[] _HoraireOuverture;
        private int _IDWeb;
        private bool _Type;
        private bool _Sexe;
        private string _DateNaissance;
        private Medecin _Medecin;
        private string _TelephonePortable;
        private string _InfoComplementaire;

        public int IDWeb { get => _IDWeb; set => _IDWeb = value; }
        public bool Type { get => _Type; set => _Type = value; }
        public bool Sexe { get => _Sexe; set => _Sexe = value; }
        public string DateNaissance { get => _DateNaissance; set => _DateNaissance = value; }
        public Medecin Medecin { get => _Medecin; set => _Medecin = value; }
        public string TelephonePortable { get => _TelephonePortable; set => _TelephonePortable = value; }
        public string InfoComplementaire { get => _InfoComplementaire; set => _InfoComplementaire = value; }
        public Horaire[] HoraireOuverture { get => _HoraireOuverture; set => _HoraireOuverture = value; }
    }
}