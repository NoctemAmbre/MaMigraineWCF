using System;
using System.Runtime.Serialization;
using System.Security.Cryptography;

namespace MigraineCSMiddleware.Modele
{
    public class Compte
    {
        private int _ID;
        private string _Identifiant;
        private string _MotDePass;
        private string _Nom;
        private string _Prenom;
        private Adresse _Adresse;
        private string _Telephone;
        private string _Erreur;
        private string _DateCreation;
        private string _DernierModif;
        private string _AdresseMail;
        private int _CreePar;
        private string _Token;

        public int ID { get => _ID; set => _ID = value; }
        public string Identifiant { get => _Identifiant; set => _Identifiant = value; }
        public string MotDePass { get => _MotDePass; set => _MotDePass = value; }
        public string Nom { get => _Nom; set => _Nom = value; }
        public string Prenom { get => _Prenom; set => _Prenom = value; }
        public Adresse Adresse { get => _Adresse; set => _Adresse = value; }
        public string Telephone { get => _Telephone; set => _Telephone = value; }
        public string Erreur { get => _Erreur; set => _Erreur = value; }
        public string DateCreation { get => _DateCreation; set => _DateCreation = value; }
        public string DernierModif { get => _DernierModif; set => _DernierModif = value; }
        public int CreePar { get => _CreePar; set => _CreePar = value; }
        public string AdresseMail { get => _AdresseMail; set => _AdresseMail = value; }
        public string Token { get => _Token; set => _Token = value; }

        //public static string GenerateToken()
        //{
        //    var buffer = new byte[128];
        //    using (var rngCryptoServiceProvider = new RNGCryptoServiceProvider())
        //    {
        //        rngCryptoServiceProvider.GetNonZeroBytes(buffer);
        //    }
        //    return Convert.ToBase64String(buffer);
        //}
    }
}