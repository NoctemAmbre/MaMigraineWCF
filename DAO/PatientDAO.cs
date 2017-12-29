using MigraineCSMiddleware.Metier;
using MigraineCSMiddleware.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;
using MigraineCSMiddleware.Modele;
using MigraineCSMiddleware.Service.date;
using MigraineCSMiddleware.Service.securite;
using MigraineCSMiddleware.Service.utilisateurweb;
using MigraineCSMiddleware.Service.compte;
using MigraineCSMiddleware.Service.patient;
using MigraineCSMiddleware.Service.medecin;

namespace MigraineCSMiddleware.DAO
{
    public class PatientDAO
    {
        //private List<Patient> _ListPatient = new List<Patient>();
        //public List<Patient> ListPatient { get => _ListPatient; set => _ListPatient = value; }

        public PatientDAO()
        {
            //Rafraichir();
        }

        /// <summary>
        /// méthode pour lecture de la database et remplissage de la List de Patient
        /// </summary>
        //private void Rafraichir()
        //{
        //    _ListPatient = new List<Patient>();
        //    AdresseDAO adresseDao = new AdresseDAO();
        //    using (DataClasses1DataContext entity = new DataClasses1DataContext())
        //    {
        //        var ret = entity.T_PATIENT.Join(entity.T_COMPTE, 
        //            P => P.IdCompte,
        //            C => C.ID,
        //            (P, C) => new { IDPatient = P.ID, ID = C.ID, Identifiant = C.Identifiant, MotDePass = "", Nom = C.Nom, Prenom = C.Prenom, AdresseMail = C.AdressMail, Telephone = P.TelephoneFixe, TelephonePortable = P.TelephonePortable, DateNaissance = P.DateNaissance, MedecinAttitre = 0, Token = C.Token });
        //        _ListPatient.Clear();
        //        foreach (var element in ret)
        //        {
        //            Patient patient = new Patient() { IDPatient = element.IDPatient, ID = (int)element.ID, Identifiant = element.Identifiant, MotDePass = element.MotDePass, Nom = element.Nom, Prenom = element.Prenom, AdresseMail = element.AdresseMail, Telephone = element.Telephone, TelephonePortable = element.TelephonePortable, DateNaissance = ConvertionDate.ConvertionStringVersDateTime(element.DateNaissance), MesMedecin = new MedecinDAO().ListMedecinDuPatient((int)element.IDPatient), MesMedicaments = new MedicamentDAO().ListeMesMedicaments((int)element.IDPatient), Adresse = adresseDao.LectureAdresse(element.ID), Token = element.Token };
        //            ListPatient.Add(patient);
        //        }
        //    }
        //}

        /// <summary>
        /// return l'objet Patient en fonction du l'id du compte transmis
        /// </summary>
        /// <param name="IdPatient"></param>
        /// <returns></returns>
        public Patient VoirPatient(int IdPatient)
        {
            using (DataClasses1DataContext entity = new DataClasses1DataContext())
            {
                var ret = entity.T_PATIENT.Join(entity.T_COMPTE,
                    P => P.IdCompte,
                    C => C.ID,
                (P, C) => new { IDPatient = P.ID, ID = C.ID, Identifiant = C.Identifiant, Nom = C.Nom, Prenom = C.Prenom, AdresseMail = C.AdressMail, Telephone = P.TelephoneFixe, TelephonePortable = P.TelephonePortable, DateNaissance = P.DateNaissance, Token = C.Token }).Where(elt => elt.IDPatient == IdPatient).FirstOrDefault();

                List<Medecin> MesMedecins = new MedecinDAO().ListMedecinDuPatient((int)ret.IDPatient);
                List<Migraine> MesMigraines = new MigraineDAO().ListeMigrainePatient((int)ret.IDPatient);
                List<Medicament> MesMedicaments = new MedicamentDAO().ListeMesMedicaments((int)ret.IDPatient);
                List<Facteur> MesFacteurs = new FacteurDAO().ListeFacteurPatient((int)ret.IDPatient);
                return new Patient() { IDPatient = ret.IDPatient, ID = (int)ret.ID, Identifiant = ret.Identifiant, MotDePass = "", Nom = ret.Nom, Prenom = ret.Prenom, AdresseMail = ret.AdresseMail, Telephone = ret.Telephone, TelephonePortable = ret.TelephonePortable, DateNaissance = ConvertionDate.ConvertionStringVersDateTime(ret.DateNaissance), MesMedecin = MesMedecins, MesMigraines = MesMigraines, MesMedicaments = MesMedicaments, MesFacteurs = MesFacteurs, Adresse = new AdresseDAO().LectureAdresse(ret.ID), Token = ret.Token };
            }
        }

        public Patient VoirPatientDuCompte(int IdCompte)
        {
            using (DataClasses1DataContext entity = new DataClasses1DataContext())
            {
                var ret = entity.T_PATIENT.Join(entity.T_COMPTE,
                    P => P.IdCompte,
                    C => C.ID,
                (P, C) => new { IDPatient = P.ID, ID = C.ID, Identifiant = C.Identifiant, Nom = C.Nom, Prenom = C.Prenom, AdresseMail = C.AdressMail, Telephone = P.TelephoneFixe, TelephonePortable = P.TelephonePortable, DateNaissance = P.DateNaissance, Token = C.Token }).Where(elt => elt.ID == IdCompte).FirstOrDefault();

                List<Medecin> MesMedecins = new MedecinDAO().ListMedecinDuPatient((int)ret.IDPatient);
                List<Migraine> MesMigraines = new MigraineDAO().ListeMigrainePatient((int)ret.IDPatient);
                List<Medicament> MesMedicaments = new MedicamentDAO().ListeMesMedicaments((int)ret.IDPatient);
                List<Facteur> MesFacteurs = new FacteurDAO().ListeFacteurPatient((int)ret.IDPatient);
                return new Patient() { IDPatient = ret.IDPatient, ID = (int)ret.ID, Identifiant = ret.Identifiant, MotDePass = "", Nom = ret.Nom, Prenom = ret.Prenom, AdresseMail = ret.AdresseMail, Telephone = ret.Telephone, TelephonePortable = ret.TelephonePortable, DateNaissance = ConvertionDate.ConvertionStringVersDateTime(ret.DateNaissance), MesMedecin = MesMedecins, MesMigraines = MesMigraines, MesMedicaments = MesMedicaments, MesFacteurs = MesFacteurs, Adresse = new AdresseDAO().LectureAdresse(ret.ID), Token = ret.Token };
            }
        }


        public List<Patient> ListPatient()
        {
            using (DataClasses1DataContext entity = new DataClasses1DataContext())
            {
                var ret = entity.T_PATIENT.Join(entity.T_COMPTE,
                    P => P.IdCompte,
                    C => C.ID,
                (P, C) => new { IDPatient = P.ID, ID = C.ID, Identifiant = C.Identifiant, Nom = C.Nom, Prenom = C.Prenom, AdresseMail = C.AdressMail, Telephone = P.TelephoneFixe, TelephonePortable = P.TelephonePortable, DateNaissance = P.DateNaissance, Token = C.Token });

                List<Patient> Retour = new List<Patient>();

                foreach (var Element in ret)
                {
                    List<Medecin> MesMedecins = new MedecinDAO().ListMedecinDuPatient((int)Element.IDPatient);
                    List<Migraine> MesMigraines = new MigraineDAO().ListeMigrainePatient((int)Element.IDPatient);
                    List<Medicament> MesMedicaments = new MedicamentDAO().ListeMesMedicaments((int)Element.IDPatient);
                    List<Facteur> MesFacteurs = new FacteurDAO().ListeFacteurPatient((int)Element.IDPatient);
                    Retour.Add(new Patient() { IDPatient = Element.IDPatient, ID = (int)Element.ID, Identifiant = Element.Identifiant, MotDePass = "", Nom = Element.Nom, Prenom = Element.Prenom, AdresseMail = Element.AdresseMail, Telephone = Element.Telephone, TelephonePortable = Element.TelephonePortable, DateNaissance = ConvertionDate.ConvertionStringVersDateTime(Element.DateNaissance), MesMedecin = MesMedecins, MesMigraines = MesMigraines, MesMedicaments = MesMedicaments, MesFacteurs = MesFacteurs, Adresse = new AdresseDAO().LectureAdresse(Element.ID)});
                }
                return Retour;
            }
        }
        //public Patient VoirPatient(int IdPatient)
        //{
        //    new AdresseDAO();
        //    using (DataClasses1DataContext entity = new DataClasses1DataContext())
        //    {
        //        var ret = entity.T_PATIENT.Join(entity.T_COMPTE,
        //        P => P.IdCompte,
        //        C => C.ID,
        //        (P, C) => new { IDPatient = P.ID, ID = C.ID, Identifiant = C.Identifiant, MotDePass = "", Nom = C.Nom, Prenom = C.Prenom, AdresseMail = C.AdressMail, Telephone = P.TelephoneFixe, TelephonePortable = P.TelephonePortable, DateNaissance = P.DateNaissance, MedecinAttitre = 0, Token = C.Token }).FirstOrDefault(elt => elt.IDPatient == IdPatient);

        //        Patient patient = new Patient() { IDPatient = ret.IDPatient, ID = (int)ret.ID, Identifiant = ret.Identifiant, MotDePass = ret.MotDePass, Nom = ret.Nom, Prenom = ret.Prenom, AdresseMail = ret.AdresseMail, Telephone = ret.Telephone, TelephonePortable = ret.TelephonePortable, DateNaissance = ConvertionDate.ConvertionStringVersDateTime(ret.DateNaissance), MesMedecin = new MedecinDAO().ListMedecinDuPatient((int)ret.IDPatient), MesMedicaments = new MedicamentDAO().ListeMesMedicaments((int)ret.IDPatient), Adresse = new AdresseDAO().LectureAdresse(ret.ID), Token = ret.Token };
        //        return patient;
        //    }
        //}

        public List<Patient> GetListPatient(string Nom)
        {
            using (DataClasses1DataContext entity = new DataClasses1DataContext())
            {
                var ret = entity.T_PATIENT.Join(entity.T_COMPTE,
                    P => P.IdCompte,
                    C => C.ID,
                    (P, C) => new { IDPatient = P.ID, Identifiant = C.Identifiant, Nom = C.Nom, Prenom = C.Prenom}).Where(elt => elt.Nom.StartsWith(Nom));
                List<Patient> retour = new List<Patient>();
                foreach (var Element in ret)
                {
                    retour.Add(new Patient() { IDPatient = Element.IDPatient, Identifiant = Element.Identifiant, Nom = Element.Nom, Prenom = Element.Prenom });
                }
                return retour;
            }
        }

        

        /// <summary>
        /// Ajoute un nouveau Patient
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        public Patient AjoutPatient(Patient patient)
        {
            using (DataClasses1DataContext entity = new DataClasses1DataContext())
            {
                //DateTime Maintenant = DateTime.Now;
                int retour = entity.AjoutPatient(patient.Identifiant, ServiceSecurite.HashMotDePass(patient.MotDePass), patient.Nom, patient.Prenom, patient.DateCreation, patient.DernierModif, 0, patient.AdresseMail, "", ConvertionDate.ConvertionDateTimeVersString(patient.DateNaissance), patient.TelephonePortable, patient.Telephone, patient.Sexe);
                if (retour == -1) throw new CompteException("Le compte exite déjà");
                //Rafraichir();

                //Patient RetourPatient = (from elt in _ListPatient where (elt.IDPatient == retour) select elt).SingleOrDefault();
                //Patient RetourPatient = _ListPatient.SingleOrDefault(p => p.IDPatient == retour);
                Patient RetourPatient = this.VoirPatient(patient.IDPatient);
                RetourPatient.Adresse = new AdresseDAO().AjoutAdresse(RetourPatient.ID, patient);
                return RetourPatient;
            }
        }

        int GetIdMedecinAttritre(Medecin medecin)
        {
            if (medecin == null) return 0;
            return medecin.IDMedecin;
        }

        public List<Patient> ListPatientDuMedecin(int IDMedecin)
        {
            List<Patient> retour = new List<Patient>();
            using (DataClasses1DataContext entity = new DataClasses1DataContext())
            {
                var retour1 = entity.T_MEDECINPATIENT.Where(elt => elt.IDMedecin == IDMedecin);
                var retour2 = retour1.Join(entity.T_PATIENT,
                    R1 => R1.IDPatient,
                    P => P.ID,
                    (R1, P) => new { IDPatient = P.ID, IDCompte = P.IdCompte }).Join(entity.T_COMPTE,
                    P => P.IDCompte,
                    C => C.ID,
                    (P, C) => new { IDPatient = P.IDPatient, ID = C.ID, Identifiant = C.Identifiant, Nom = C.Nom, Prenom = C.Prenom }).ToList();

                foreach (var Element in retour2)
                {
                    retour.Add(new Patient() { IDPatient = Element.IDPatient, Identifiant = Element.Identifiant, Nom = Element.Nom, Prenom = Element.Prenom });
                }
                return retour;
            }
        }


        /// <summary>
        /// retour une information vrais ou faux en fonction du login et du mot de passe envoyé. Nous ne stoquons pas , ainsi le mot de passe en mémoire
        /// </summary>
        /// <param name="Login"></param>
        /// <param name="Pass"></param>
        /// <returns></returns>
        public Patient Login(string Identifiant, string Pass)
        {
            int IdCompte = new CompteDAO().Autentification(Identifiant, Pass); //teste de l'identifiant et du mot de passe pour rejet si non présent dans la base
            //Rafraichir();
            Patient retourPatient = VoirPatientDuCompte(IdCompte); //on récupère le patient à partir de l'id du compte
            try { retourPatient.Adresse = new AdresseDAO().LectureAdresse(retourPatient.ID); }//lecture de l'adresse a partir de l'IdCompte
            catch (Exception) { retourPatient.Adresse = null; }

            return retourPatient;
        }

        /// <summary>
        /// retourne une valeur vrais ou faux pour déterminé si l'id est bien celui d'un Patient
        /// </summary>
        /// <param name="IdPatient"></param>
        /// <returns></returns>
        public bool IsPatient(int IdPatient)
        {
            using (DataClasses1DataContext entity = new DataClasses1DataContext())
            {
                var Reponse = entity.T_PATIENT.FirstOrDefault(elt => elt.ID == IdPatient);
                if (Reponse == null) return false;
                else return true;
            }
            //foreach (Patient Element in _ListPatient)
            //{
            //    if (Element.IDPatient == IdPatient) return true;
            //}
            //return false;
        }

        public bool IsPatient(string Identifiant)
        {
            //Patient retourPatient = _ListPatient.Where(info => info.Identifiant == Identifiant).First();
            //if (retourPatient != null) return true;
            //else return false;
            using (DataClasses1DataContext entity = new DataClasses1DataContext())
            {
                var Reponse = entity.T_PATIENT.Join(entity.T_COMPTE,
                    P => P.IdCompte,
                    C => C.ID,
                    (P, C) => new {IdCompte = C.ID, IdPatient = P.ID, Identifiant = C.Identifiant }).FirstOrDefault(elt => elt.Identifiant == Identifiant);
                if (Reponse == null) return false;
                else return true;
            }
            //foreach (Patient Element in _ListPatient)
            //{
            //    if (Element.Identifiant.ToLower() == Identifiant.ToLower()) return true;
            //}
            //return false;
        }

        /// <summary>
        /// Attribution d'un Medecin à un patient. Retourne un Patient
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="medecin"></param>
        /// <returns></returns>
        public Patient AttributionMedecin(Patient patient, Medecin medecin)
        {
            using (DataClasses1DataContext entity = new DataClasses1DataContext())
            {
                int retour = entity.AjoutMedecinPatient(medecin.IDMedecin,patient.IDPatient);

                if (retour != -1)
                {
                    return VoirPatient(patient.IDPatient);
                    //Rafraichir(); //rafraichissement de la list
                    //return VoirPatient(patient.IDPatient); //retourne le Patient en fonction de son ID
                }
            }
            return null;
        }

        /// <summary>
        /// Modification des informations du Patient
        /// </summary>
        /// <param name="IdPatient"></param>
        /// <param name="Age"></param>
        /// <param name="IdMedecin"></param>
        /// <returns></returns>
        public Patient Modification(Patient patient)
        {
            using (DataClasses1DataContext entity = new DataClasses1DataContext())
            {
                //Patient patienbdd = _ListPatient.Where(elt => elt.Identifiant == patient.Identifiant).First();

                int retour = entity.ModifPatient(entity.T_PATIENT.FirstOrDefault(elt => elt.ID == patient.IDPatient).IdCompte, patient.Nom, patient.Prenom, ConvertionDate.ConvertionDateTimeVersString(DateTime.UtcNow), patient.AdresseMail, ServiceSecurite.GenererToken(patient.Identifiant, new CompteDAO().GetMotDePass(patient.Identifiant), DateTime.UtcNow.Ticks), patient.IDPatient, ConvertionDate.ConvertionDateTimeVersString(patient.DateNaissance), patient.TelephonePortable, patient.Telephone, patient.Sexe);
                if (retour == -1) throw new CompteModificationException(patient, "La modification du patient n'a pus avoir lieu");

                //Rafraichir();
                Patient RetourPatient = VoirPatient(patient.IDPatient);
                return RetourPatient;
              }    
        }

        public Patient NouveauMotDePass(Patient patient)
        {
            using (DataClasses1DataContext entity = new DataClasses1DataContext())
            {

                //Patient patienbdd = _ListPatient.Where(elt => elt.Identifiant == patient.Identifiant).First();
                string MotDePasse = ServiceSecurite.HashMotDePass(patient.MotDePass);
                int retour = new CompteDAO().ChangementInformation(entity.T_PATIENT.FirstOrDefault(elt => elt.ID == patient.ID).IdCompte, patient.Identifiant, MotDePasse, null, null, null, null, 0, null, ServiceSecurite.GenererToken(patient.Identifiant, MotDePasse, DateTime.UtcNow.Ticks)); //on transmet l'information de d'Id du compte et on transmet a CompteDAO l'ordre de changer le mot de passe. puis on récupère l'information que l'opération c'est bien passé
                if (retour == -1) throw new CompteModificationException(patient, "Le changement de mot de passe n'a pus être effectué");

                //Rafraichir();
                Patient RetourPatient = VoirPatient(patient.IDPatient);

                return RetourPatient;
            }
        }

        //public List<Patient> GetListMesPatient(int IdMedecin)
        //{
        //    List<Patient> ListPatientMedecinNonNull = (from elt in _ListPatient where (elt.MesMedecin != null) select elt).ToList();
        //    List<Patient> ListRetour = (from elt in ListPatientMedecinNonNull where (elt.MesMedecin.IDMedecin == IdMedecin) select elt).ToList();

        //    return ListRetour;
        //}

        private int VoirIDMedecinAttitre(Medecin medecin)
        {
            if (medecin == null) return -1;
            else return medecin.IDMedecin;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdPatient"></param>
        /// <param name="idMedecin"></param>
        /// <returns></returns>
        public int SupprMedecinDuPatient(Patient patient, Medecin medecin)
        {
            using (DataClasses1DataContext entity = new DataClasses1DataContext())
            {
                if (patient == null) throw new PatientIncorrecteException("Le Patient est incorrecte", medecin);
                if (medecin == null) throw new MedecinNonAttribueAuPatientException("Aucun médecin n'est rattaché à ce patient", medecin);
                //if (medecin.IDMedecin != idMedecin) throw new MedecinMalAttribueAuPatientException("Le medecin d'Id " + idMedecin.ToString() + " n'est pas rattaché à ce patient. L'ID médecin pour ce patient est : " + medecin.IDMedecin.ToString());
                //int retour = entity.ModifPatient(patient.ID, patient.Nom, patient.Prenom, ConvertionDate.ConvertionDateTimeVersString(DateTime.UtcNow), patient.AdresseMail, patient.Token, patient.IDPatient, ConvertionDate.ConvertionDateTimeVersString(patient.DateNaissance), patient.TelephonePortable, patient.Telephone, patient.Sexe);
                return entity.SupprMedecinPatient(medecin.IDMedecin, patient.IDPatient);
            }
        }
    }
}