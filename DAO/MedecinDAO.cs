using MigraineCSMiddleware.Metier;
using MigraineCSMiddleware.Modele;
using MigraineCSMiddleware.Service;
using MigraineCSMiddleware.Service.Date;
using MigraineCSMiddleware.Service.Securite;
using MigraineCSMiddleware.Service.Utilisateur;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace MigraineCSMiddleware.DAO
{
    public class MedecinDAO
    {
        private List<Medecin> _ListMedecin = new List<Medecin>();
        public List<Medecin> ListMedecin { get => _ListMedecin; set => _ListMedecin = value; }

        public MedecinDAO()
        {
            Rafraichir();
        }

        /// <summary>
        /// méthode pour lecture de la database et remplissage de la List de Patient
        /// </summary>
        private void Rafraichir()
        {
            _ListMedecin = new List<Medecin>();
            AdresseDAO adresseDAO = new AdresseDAO();
            HoraireDAO horaireDAO = new HoraireDAO();
            using (DataClasses1DataContext entity = new DataClasses1DataContext())
            {
                var ret = entity.T_MEDECIN.Join(entity.T_COMPTE,
                    P => P.IdCompte,
                    C => C.ID,
                    (P, C) => new { IDMedecin = P.ID, ID = C.ID, Identifiant = C.Identifiant, MotDePass = "", Nom = C.Nom, Prenom = C.Prenom, Telephone = P.TelephoneCabinet, AdresseMail = C.AdressMail, InfoComplementaire = P.InfoComplementaire, DateCreation = C.DateCreation, DernierModif = C.DerniereModif, CreePar = 0, Token = C.Token });
                _ListMedecin.Clear();
                foreach (var element in ret)
                {
                    Medecin medecin = new Medecin() { IDMedecin = (int)element.IDMedecin, ID = (int)element.ID, Identifiant = element.Identifiant, MotDePass = element.MotDePass, Nom = element.Nom, Prenom = element.Prenom, Telephone = element.Telephone, AdresseMail = element.AdresseMail, InfoComplementaire = element.InfoComplementaire, DateCreation = element.DateCreation, DernierModif = element.DernierModif, CreePar = 0, Adresse = adresseDAO.LectureAdresse(element.ID), Token = element.Token };
                    //patient.Adresse = adresseDao.LectureAdresse(patient.ID);
                    medecin.HoraireOuverture = horaireDAO.LectureHoraire(medecin.IDMedecin);
                    _ListMedecin.Add(medecin);
                }



                //foreach (T_MEDECIN element in entity.T_MEDECIN)
                //{
                //    T_COMPTE res = (from elt in entity.T_COMPTE where (elt.ID == element.IdCompte) select elt).First();
                //    if (res != null)
                //    {
                //        //string[] valeurCreation = Regex.Split(res.DateCreation, "-");
                //        //string[] valeurModif = Regex.Split(res.DerniereModif, "-");
                //        //DateTime dateCreation = new DateTime(int.Parse(valeurCreation[0]), int.Parse(valeurCreation[1]), int.Parse(valeurCreation[2]));
                //        //DateTime dernierModif = new DateTime(int.Parse(valeurModif[0]), int.Parse(valeurModif[1]), int.Parse(valeurModif[2]));
                //        Medecin medecin = new Medecin() { IDMedecin = (int)element.ID, ID = (int)res.ID, Identifiant = res.Identifiant, MotDePass = null, Nom = res.Nom, Prenom = res.Prenom, Telephone = element.TelephoneCabinet, AdresseMail = res.AdressMail, InfoComplementaire = element.InfoComplementaire, DateCreation = res.DateCreation, DernierModif = res.DerniereModif,  CreePar = 0, Token = res.Token};
                //        medecin.Adresse = adresseDAO.LectureAdresse(medecin.ID);
                //        medecin.HoraireOuverture = horaireDAO.LectureHoraire(medecin.IDMedecin);
                //        _ListMedecin.Add(medecin);
                //    }
                //}
            }
        }

        public List<Medecin> GetListMedecin(string Nom)
        {
            using (DataClasses1DataContext entity = new DataClasses1DataContext())
            {
                var ret = entity.T_MEDECIN.Join(entity.T_COMPTE,
                    M => M.IdCompte,
                    C => C.ID,
                    (P, C) => new { IDMedecin = P.ID, Identifiant = C.Identifiant, Nom = C.Nom, Prenom = C.Prenom }).Where(elt => elt.Nom.StartsWith(Nom));
                List<Medecin> retour = new List<Medecin>();
                foreach (var Element in ret)
                {
                    retour.Add(new Medecin() { IDMedecin = Element.IDMedecin, Identifiant = Element.Identifiant, Nom = Element.Nom, Prenom = Element.Prenom });
                }
                return retour;
            }
        }


        /// <summary>
        /// return l'objet Medecin en fonction du l'id du compte transmis
        /// </summary>
        /// <param name="IdMedecin"></param>
        /// <returns></returns>
        public Medecin VoirMedecinSimple(int IDMedecin)
        {
            using (DataClasses1DataContext entity = new DataClasses1DataContext())
            {
                var ret = entity.T_MEDECIN.Join(entity.T_COMPTE,
                    M => M.IdCompte,
                    C => C.ID,
                    (M, C) => new { IDMedecin = M.ID, ID = C.ID, Identifiant = C.Identifiant, Nom = C.Nom, Prenom = C.Prenom}).FirstOrDefault(elm => elm.IDMedecin == IDMedecin);
                
                    return new Medecin() { IDMedecin = ret.IDMedecin, ID = ret.ID, Identifiant = ret.Identifiant, Nom = ret.Nom, Prenom = ret.Prenom };
            }
            
            return null;
            //return (from elt in _ListMedecin where (elt.IDMedecin == IdMedecin) select elt).First();
        }

        public Medecin VoirMedecin(string Identifiant)
        {
            AdresseDAO adresseDAO = new AdresseDAO();
            HoraireDAO horaireDAO = new HoraireDAO();
            using (DataClasses1DataContext entity = new DataClasses1DataContext())
            {
                var ret = entity.T_MEDECIN.Join(entity.T_COMPTE,
                    M => M.IdCompte,
                    C => C.ID,
                    (M, C) => new { IDMedecin = M.ID, ID = C.ID, Identifiant = C.Identifiant, MotDePass = "", Nom = C.Nom, Prenom = C.Prenom, Telephone = M.TelephoneCabinet, AdresseMail = C.AdressMail, InfoComplementaire = M.InfoComplementaire, DateCreation = C.DateCreation, DernierModif = C.DerniereModif, CreePar = 0, Token = C.Token }).FirstOrDefault(elm => elm.Identifiant == Identifiant);

                Medecin medecin = new Medecin() { IDMedecin = (int)ret.IDMedecin, ID = (int)ret.ID, Identifiant = ret.Identifiant, MotDePass = ret.MotDePass, Nom = ret.Nom, Prenom = ret.Prenom, Telephone = ret.Telephone, AdresseMail = ret.AdresseMail, InfoComplementaire = ret.InfoComplementaire, DateCreation = ret.DateCreation, DernierModif = ret.DernierModif, CreePar = 0, Token = ret.Token };
                medecin.Adresse = adresseDAO.LectureAdresse(medecin.ID);
                medecin.HoraireOuverture = horaireDAO.LectureHoraire(medecin.IDMedecin);
                medecin.MesPatient = new PatientDAO().ListPatientDuMedecin(medecin.IDMedecin);

                return medecin;
            }
        }

        public Medecin VoirMedecin(int IDMedecin)
        {
            AdresseDAO adresseDAO = new AdresseDAO();
            HoraireDAO horaireDAO = new HoraireDAO();
            using (DataClasses1DataContext entity = new DataClasses1DataContext())
            {
                var ret = entity.T_MEDECIN.Join(entity.T_COMPTE,
                    M => M.IdCompte,
                    C => C.ID,
                    (M, C) => new { IDMedecin = M.ID, ID = C.ID, Identifiant = C.Identifiant, MotDePass = "", Nom = C.Nom, Prenom = C.Prenom, Telephone = M.TelephoneCabinet, AdresseMail = C.AdressMail, InfoComplementaire = M.InfoComplementaire, DateCreation = C.DateCreation, DernierModif = C.DerniereModif, CreePar = 0, Token = C.Token }).FirstOrDefault(elm => elm.IDMedecin == IDMedecin);

                Medecin medecin = new Medecin() { IDMedecin = (int)ret.IDMedecin, ID = (int)ret.ID, Identifiant = ret.Identifiant, MotDePass = ret.MotDePass, Nom = ret.Nom, Prenom = ret.Prenom, Telephone = ret.Telephone, AdresseMail = ret.AdresseMail, InfoComplementaire = ret.InfoComplementaire, DateCreation = ret.DateCreation, DernierModif = ret.DernierModif, CreePar = 0, Token = ret.Token };
                medecin.Adresse             = adresseDAO.LectureAdresse(medecin.ID); 
                medecin.HoraireOuverture    = horaireDAO.LectureHoraire(medecin.IDMedecin);
                medecin.MesPatient          = new PatientDAO().ListPatientDuMedecin(medecin.IDMedecin);

                return medecin;
            }
        }

        public List<Medecin> ListMedecinDuPatient(int IDPatient)
        {
            List<Medecin> retour = new List<Medecin>();
            using (DataClasses1DataContext entity = new DataClasses1DataContext())
            {
                var retour1 = entity.T_MEDECINPATIENT.Where(elt => elt.IDPatient == IDPatient).ToList();
                var retour2 = retour1.Join(entity.T_MEDECIN,
                    R1 => R1.IDMedecin,
                    P => P.ID,
                    (R1, P) => new { IDMedecin = P.ID, IDCompte = P.IdCompte }).Join(entity.T_COMPTE,
                    P => P.IDCompte,
                    C => C.ID,
                    (P, C) => new { IDMedecin = P.IDMedecin, ID = C.ID, Identifiant = C.Identifiant, Nom = C.Nom, Prenom = C.Prenom }).ToList();

                foreach (var Element in retour2)
                {
                    retour.Add(new Medecin() { IDMedecin = Element.IDMedecin, Identifiant = Element.Identifiant, Nom = Element.Nom, Prenom = Element.Prenom });
                }
                return retour;
            }
        }

        /// <summary>
        /// Ajout, dans la based d'un nouveau Medecin
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        public Medecin AjoutMedecin(Medecin medecin)
        {
            using (DataClasses1DataContext entity = new DataClasses1DataContext())
            {
                int retour = entity.AjoutMedecin(medecin.Identifiant, ServiceSecurite.HashMotDePass(medecin.MotDePass), medecin.Nom, medecin.Prenom, ConvertionDate.ConvertionDateTimeVersString(DateTime.Now), ConvertionDate.ConvertionDateTimeVersString(DateTime.Now), 0, medecin.AdresseMail, "", medecin.Telephone, medecin.InfoComplementaire);
                if (retour == -1) throw new CompteException("Le compte exite déjà");

                Rafraichir();
                Medecin RetourMedecin = _ListMedecin.Where(Id => Id.IDMedecin == retour).SingleOrDefault();
                                
                RetourMedecin.HoraireOuverture = new HoraireDAO().AjoutHoraire(retour, medecin.HoraireOuverture);
                RetourMedecin.Adresse = new AdresseDAO().AjoutAdresse(RetourMedecin.ID, medecin);

                
                return RetourMedecin;
            }
        }

        /// <summary>
        /// retour une information vrais ou faux en fonction du login et du mot de passe envoyé
        /// </summary>
        /// <param name="Login"></param>
        /// <param name="Pass"></param>
        /// <returns></returns>
        public Medecin Login(string Identifiant, string Pass)
        {
            int IdCompte = new CompteDAO().Autentification(Identifiant, Pass); //teste de l'identifiant et du mot de passe pour rejet si non présent dans la base
                                                                               //Rafraichir();

            Medecin retourMedecin = VoirMedecin(Identifiant);

            //Medecin retourMedecin = _ListMedecin.SingleOrDefault(medecin => medecin.ID == IdCompte); //on récupère le médecin a partir de l'id du compte
            //try { retourMedecin.Adresse = new AdresseDAO().LectureAdresse(retourMedecin.ID); }//lecture de l'adresse a partir de l'IdCompte
            //catch (Exception) { retourMedecin.Adresse = null; }

            //try { retourMedecin.HoraireOuverture = new HoraireDAO().LectureHoraire(retourMedecin.IDMedecin); } //on récupère la table des horaires en fonctiond de l'id du médecin
            //catch (Exception) { retourMedecin.HoraireOuverture = null; }


            //foreach (Medecin Element in _ListMedecin)
            //{
            //    if (Element.Identifiant == Identifiant & Element.MotDePass == Pass)
            //    {
            //        Element.Adresse = new AdresseDAO().LectureAdresse(Element);
            //        return Element;
            //    }
            //}
            return retourMedecin;
        }




        /// <summary>
        /// retourne une valeur vrais ou faux pour déterminé si l'id est bien celui d'un Medecin
        /// </summary>
        /// <param name="idMedecin"></param>
        /// <returns></returns>
        public bool IsMedecin(int idMedecin)
        {
            foreach (Medecin Element in _ListMedecin)
            {
                if (Element.IDMedecin == idMedecin) return true;
            }
            return false;
        }
        public bool IsMedecin(string Identifiant)
        {
            foreach (Medecin Element in _ListMedecin)
            {
                if (Element.Identifiant.ToLower() == Identifiant.ToLower()) return true;
            }
            return false;
        }

        /// <summary>
        /// Modification des informations du Medecin
        /// </summary>
        /// <param name="IdMedecin"></param>
        /// <param name="Age"></param>
        /// <returns></returns>
        public Medecin Modification(Medecin medecin)
        {
            using (DataClasses1DataContext entity = new DataClasses1DataContext())
            {
                Medecin medecinbdd = _ListMedecin.Where(elt => elt.Identifiant == medecin.Identifiant).SingleOrDefault();
                int retour = entity.ModifMedecin(medecinbdd.ID, medecin.Nom, medecin.Prenom, ConvertionDate.ConvertionDateTimeVersString(DateTime.UtcNow), medecin.AdresseMail, ServiceSecurite.GenererToken(medecin.Identifiant, new CompteDAO().GetMotDePass(medecin.Identifiant), DateTime.UtcNow.Ticks), (int)medecin.IDMedecin, medecin.Telephone, medecin.InfoComplementaire);
                if (retour == -1) throw new CompteModificationException(medecin, "La modification du medecin n'a pus avoir lieu");
                
                Rafraichir();
                
                Medecin RetourMedecin = _ListMedecin.Where(Id => Id.IDMedecin == retour).SingleOrDefault();
                RetourMedecin.HoraireOuverture = new HoraireDAO().AjoutHoraire(retour, medecin.HoraireOuverture);
                RetourMedecin.Adresse = new AdresseDAO().AjoutAdresse(RetourMedecin.ID, medecin);
                Rafraichir();

                return _ListMedecin.Where(Id => Id.IDMedecin == retour).First();
            }
            return null;
        }

        public Medecin NouveauMotDePass(Medecin medecin)
        {
            Medecin medecinbdd = _ListMedecin.Where(elt => elt.Identifiant == medecin.Identifiant).First();
            string MotDePasse = ServiceSecurite.HashMotDePass(medecin.MotDePass);
            int retour = new CompteDAO().ChangementInformation(medecin.ID, null, MotDePasse, null, null, null, null, 0, null, ServiceSecurite.GenererToken(medecin.Identifiant, MotDePasse, DateTime.UtcNow.Ticks)); //on transmet l'information de d'Id du compte et on transmet a CompteDAO l'ordre de changer le mot de passe. puis on récupère l'information que l'opération c'est bien passé
            if (retour != -1) throw new CompteModificationException(medecin, "Le changement de mot de passe n'a pus être effectué");

            Rafraichir();
            Medecin RetourMedecin = _ListMedecin.Where(elt => elt.IDMedecin == medecin.IDMedecin).SingleOrDefault();
 
            return RetourMedecin;
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