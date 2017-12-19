using MigraineCSMiddleware.DAO;
using MigraineCSMiddleware.Modele;
using MigraineCSMiddleware.Service.compte;
using MigraineCSMiddleware.Service.date;
using MigraineCSMiddleware.Service.utilisateurweb;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace MigraineCSMiddleware.Service.securite
{
    public class ServiceSecurite
    {
        private const string _Algo= "HmacSHA256";
        private const string _CleeBasic = "j6tYtmgst2XIOIeRsPHR";
        private const string _CleeLongue = "rz74X97GhpMnRT9sYT644XxeMq67qrkr8We7KhNWS6u6778fJes6873VcCZBq9YH"; //clée de 64 caractère pour le token
        private const int _expirationMinutes = 10;

        public static string GenererToken(string Login, string MotDePass, long ticks)
        {
            string hash = string.Join(":", new string[] { Login, ticks.ToString() });
            string hashGauche = "";
            string hashDroit = "";
            using (HMAC hmac = HMACSHA256.Create(_Algo))
            {
                hmac.Key = Encoding.UTF8.GetBytes(MotDePass);
                hmac.ComputeHash(Encoding.UTF8.GetBytes(hash));
                hashGauche = Convert.ToBase64String(hmac.Hash);
                hashDroit = string.Join(":", new string[] { Login, ticks.ToString() });
            }
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Join(":", hashGauche, hashDroit)));
        }

        public static string HashMotDePass(string MotDePass)
        {
            string key = string.Join(":", new string[] { MotDePass, _CleeLongue });
            using (HMAC hmac = HMACSHA256.Create(_Algo))
            {
                hmac.Key = Encoding.UTF8.GetBytes(MotDePass);
                hmac.ComputeHash(Encoding.UTF8.GetBytes(key));
                return Convert.ToBase64String(hmac.Hash);
            }
        }


        //private string HashPassword(string pass)
        //{
        //    byte[] bytes = Encoding.Unicode.GetBytes(pass + "AjouteFinale");
        //    SHA256Managed hashstring = new SHA256Managed();
        //    byte[] hash = hashstring.ComputeHash(bytes);
        //    string hashString = string.Empty;
        //    foreach (byte x in hash)
        //    {
        //        hashString += String.Format("{0:x2}", x);
        //    }
        //    return hashString;
        //}




        /// <summary>
        /// transforme des donné Json en classe UtilisateurWeb
        /// </summary>
        /// <param name="Token"></param>
        /// <returns></returns>
        public static UtilisateurWeb UtilisateurWebDepuisValeur(string Token)
        {
            string key = Encoding.UTF8.GetString(Convert.FromBase64String(Token));
            using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(key)))
            {
                DataContractJsonSerializer deserializer = new DataContractJsonSerializer(typeof(UtilisateurWeb));
                UtilisateurWeb compteWeb = (UtilisateurWeb)deserializer.ReadObject(ms);
                return compteWeb;
            }
        }
        /// <summary>
        /// récupère le token et le vérifie avec le basic Token 
        /// </summary>
        /// <param name="Token"></param>
        /// <returns></returns>
        public static void TokenBasicValide(UtilisateurWeb Utilisateur)
        {
            if (Encoding.UTF8.GetString(Convert.FromBase64String(Utilisateur.Token)) != _CleeBasic) throw new TokenInvalidException(Utilisateur, "Le tocken est invalide");
        }

        //public static TokenValide(string tokenlongue)
        //{
        //    IsTokenValid(tokenlongue);
        //}

        /// <summary>
        /// Transforme l'UtilisateurWeb en un Patient ou un Medecin. Retourne un compte
        /// </summary>
        /// <param name="compteWeb"></param>
        /// <returns></returns>
        public static Compte UtilisateurWebVersCompte(UtilisateurWeb compteWeb)
        {
            
            if (compteWeb.IDWeb == 0) //S'il s'agit d'un nouveau compte.
            {
                if (compteWeb.Type)
                {
                    Medecin CompteMedecin = new Medecin();
                    CompteMedecin.IDMedecin = compteWeb.IDWeb;
                    CompteMedecin.InfoComplementaire = compteWeb.InfoComplementaire;
                    CompteMedecin.Adresse = compteWeb.Adresse;
                    CompteMedecin.CreePar = compteWeb.CreePar;
                    CompteMedecin.DateCreation = ConvertionDate.ConvertionDateTimeVersString(DateTime.Now);
                    CompteMedecin.DernierModif = ConvertionDate.ConvertionDateTimeVersString(DateTime.Now);
                    CompteMedecin.Identifiant = compteWeb.Identifiant;
                    CompteMedecin.MotDePass = compteWeb.MotDePass;
                    CompteMedecin.Nom = compteWeb.Nom;
                    CompteMedecin.Prenom = compteWeb.Prenom;
                    CompteMedecin.Telephone = compteWeb.Telephone;
                    CompteMedecin.AdresseMail = compteWeb.AdresseMail;
                    CompteMedecin.HoraireOuverture = compteWeb.HoraireOuverture;
                    CompteMedecin.Token = compteWeb.Token;
                    return CompteMedecin;
                }
                else
                {
                    Patient retourPatient = new Patient();
                    retourPatient.IDPatient = compteWeb.IDWeb;
                    retourPatient.Adresse = compteWeb.Adresse;
                    retourPatient.CreePar = compteWeb.CreePar;
                    retourPatient.DateCreation = ConvertionDate.ConvertionDateTimeVersString(DateTime.Now);
                    retourPatient.DateNaissance = ConvertionDate.ConvertionStringVersDateTime(compteWeb.DateNaissance);
                    retourPatient.DernierModif = ConvertionDate.ConvertionDateTimeVersString(DateTime.Now);
                    retourPatient.Identifiant = compteWeb.Identifiant;
                    retourPatient.MesMedecin = null;
                    retourPatient.MotDePass = compteWeb.MotDePass;
                    retourPatient.Nom = compteWeb.Nom;
                    retourPatient.Prenom = compteWeb.Prenom;
                    retourPatient.Sexe = compteWeb.Sexe;
                    retourPatient.Telephone = compteWeb.Telephone;
                    retourPatient.TelephonePortable = compteWeb.TelephonePortable;
                    retourPatient.AdresseMail = compteWeb.AdresseMail;
                    retourPatient.Token = compteWeb.Token;
                    return retourPatient;
                }
            }
            if (new MedecinDAO().IsMedecin(compteWeb.IDWeb)) // si le compte en création est un Medecin
            {
                Medecin CompteMedecin = new Medecin();
                CompteMedecin.IDMedecin = compteWeb.IDWeb;
                CompteMedecin.InfoComplementaire = compteWeb.InfoComplementaire;
                CompteMedecin.Adresse = compteWeb.Adresse;
                CompteMedecin.CreePar = compteWeb.CreePar;
                CompteMedecin.DateCreation = ConvertionDate.ConvertionDateTimeVersString(DateTime.Now);
                CompteMedecin.DernierModif = ConvertionDate.ConvertionDateTimeVersString(DateTime.Now);
                CompteMedecin.Identifiant = compteWeb.Identifiant;
                CompteMedecin.MotDePass = compteWeb.MotDePass;
                CompteMedecin.Nom = compteWeb.Nom;
                CompteMedecin.Prenom = compteWeb.Prenom;
                CompteMedecin.Telephone = compteWeb.Telephone;
                CompteMedecin.AdresseMail = compteWeb.AdresseMail;
                CompteMedecin.HoraireOuverture = compteWeb.HoraireOuverture;
                CompteMedecin.Token = compteWeb.Token;
                return CompteMedecin;
            }
            if (new PatientDAO().IsPatient(compteWeb.IDWeb)) // si le compte en création est un Patient
            {
                Patient retourPatient = new Patient();
                retourPatient.IDPatient = compteWeb.IDWeb;
                retourPatient.Adresse = compteWeb.Adresse;
                retourPatient.CreePar = compteWeb.CreePar;
                retourPatient.DateCreation = ConvertionDate.ConvertionDateTimeVersString(DateTime.Now);
                retourPatient.DateNaissance = ConvertionDate.ConvertionStringVersDateTime(compteWeb.DateNaissance);
                retourPatient.DernierModif = ConvertionDate.ConvertionDateTimeVersString(DateTime.Now);
                retourPatient.Identifiant = compteWeb.Identifiant;
                retourPatient.MesMedecin = null;
                retourPatient.MotDePass = compteWeb.MotDePass;
                retourPatient.Nom = compteWeb.Nom;
                retourPatient.Prenom = compteWeb.Prenom;
                retourPatient.Sexe = compteWeb.Sexe;
                retourPatient.Telephone = compteWeb.Telephone;
                retourPatient.TelephonePortable = compteWeb.TelephonePortable;
                retourPatient.AdresseMail = compteWeb.AdresseMail;
                retourPatient.Token = compteWeb.Token;

                return retourPatient;
            }
            else throw new UtilisateurWebInexistantException("Ce compte n'existe pas", compteWeb);
        }

        /// <summary>
        /// transcription du Token en objet Compte avec cryptage du mot de passe
        /// </summary>
        /// <param name="Token"></param>
        /// <returns></returns>
        public static Compte GetCompteSecurite(string Token)
        {
            string key = Encoding.UTF8.GetString(Convert.FromBase64String(Token));

            using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(key)))
            {
                DataContractJsonSerializer deserializer = new DataContractJsonSerializer(typeof(Compte));
                Compte compte = (Compte)deserializer.ReadObject(ms);
                UtilisateurWeb utilisateur = new UtilisateurWeb() { Identifiant = compte.Identifiant, Token = compte.Token };


                if (key == null) throw new AutentificationIncorrecteException("Les information de login / mot de passe sont incorrecte");
                if (String.IsNullOrEmpty(compte.Identifiant)) throw new AutentificationIncorrecteException("Le login est nul ou vide");
                if (String.IsNullOrEmpty(compte.MotDePass)) throw new AutentificationIncorrecteException("Le mot de passe est nul ou vide");
                if (String.IsNullOrEmpty(compte.Token)) throw new TokenInvalidException(utilisateur, "Le tocken est nul ou vide");
                if (compte.Token != _CleeBasic) throw new TokenInvalidException(utilisateur, "Le tocken est invalide");
                compte.MotDePass = HashMotDePass(compte.MotDePass);

                string retourMotDePass = new CompteDAO().GetMotDePass(compte.Identifiant);
                if (retourMotDePass == "" | compte.MotDePass != retourMotDePass) throw new AutentificationIncorrecteException(compte.Identifiant, "Identifiant ou mot de passe incorrecte");

                return compte;
            }
        }

        //public static bool IsTokenValid(UtilisateurWeb utilisateurweb)
        //{
        //    return IsTokenValid(utilisateurweb.Token);
        //}

        public static void IsTokenValid(UtilisateurWeb utilisateur, String Token)
        {
            if (string.IsNullOrEmpty(Token) | (Token == "undefined")) throw new TokenInvalidException(utilisateur, "Le token est invalide");
         
            string key = Encoding.UTF8.GetString(Convert.FromBase64String(Token));

            string[] parts = key.Split(new char[] { ':' });
            if (parts.Length > 3 | parts.Length < 3) throw new TokenInvalidException(utilisateur, "Le token est invalide");
  
            string hash = parts[0];
            string username = parts[1];
            long ticks = long.Parse(parts[2]);
            DateTime timeStamp = new DateTime(ticks);
            // Ensure the timestamp is valid.
            bool expired = Math.Abs((DateTime.UtcNow - timeStamp).TotalMinutes) > _expirationMinutes;
            if (expired) throw new TokenExpireException(utilisateur, "Le token est expiré");
               
            CompteDAO comptedao = new CompteDAO();

            string[] Keybdd = Encoding.UTF8.GetString(Convert.FromBase64String(comptedao.GetToken(username))).Split(new char[] { ':' });
            if (Keybdd[0] != hash) throw new TokenInvalidException(utilisateur, "Le token est invalide");

            //string computedToken = GenererToken(username, comptedao.GetMotDePass(username), ticks);

        }

        //private string HashPassword(string pass)
        //{
        //    byte[] bytes = Encoding.Unicode.GetBytes(pass + "AjouteFinale");
        //    SHA256Managed hashstring = new SHA256Managed();
        //    byte[] hash = hashstring.ComputeHash(bytes);
        //    string hashString = string.Empty;
        //    foreach (byte x in hash)
        //    {
        //        hashString += String.Format("{0:x2}", x);
        //    }
        //    return hashString;
        //}
    }
}