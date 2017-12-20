using MigraineCSMiddleware.DAO;
using MigraineCSMiddleware.Modele;
using MigraineCSMiddleware.Service.compte;
using MigraineCSMiddleware.Service.date;
using MigraineCSMiddleware.Service.facteur;
using MigraineCSMiddleware.Service.medecin;
using MigraineCSMiddleware.Service.medicament;
using MigraineCSMiddleware.Service.patient;
using MigraineCSMiddleware.Service.securite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MigraineCSMiddleware.Service.utilisateurweb
{
    public class ServiceUtilisateursWeb
    {

        public ServiceUtilisateursWeb()
        {

        }
        public UtilisateurWeb Login(string Token)
        {
            
            Compte compte = ServiceSecurite.GetCompteSecurite(Token); //teste du compte et du token basic. Si incorrecte passage en catch
           
            PatientDAO _PatientDAO = new PatientDAO();
            MedecinDAO _MedecinDAO = new MedecinDAO();
            if (_PatientDAO.IsPatient(compte.Identifiant))
            {
                return Conversion(_PatientDAO.Login(compte.Identifiant, compte.MotDePass));
            }
            else if (_MedecinDAO.IsMedecin(compte.Identifiant))
            {
                return Conversion(_MedecinDAO.Login(compte.Identifiant, compte.MotDePass));
            }
            else return null;
        }

        public UtilisateurWeb CreationCompte(string ValueJSON)
        {
            UtilisateurWeb UtilWeb = ServiceSecurite.UtilisateurWebDepuisValeur(ValueJSON); //convertion             
            ServiceSecurite.TokenBasicValide(UtilWeb); //teste du token. Si incorect passage en catch

            Compte retour = ServiceSecurite.UtilisateurWebVersCompte(UtilWeb);
            if (retour is Medecin)
            {
                return Conversion((new ServiceMedecin()).AjoutMedecin(retour as Medecin));
                
            }
            else if (retour is Patient)
            {
                return Conversion((new ServicePatient()).AjoutPatient(retour as Patient));
            }
            else return null;
        }

        public UtilisateurWeb ModificationCompte(string ValueJSON, String Token)
        {
            UtilisateurWeb UtilWeb = ServiceSecurite.UtilisateurWebDepuisValeur(ValueJSON);//convertion
            ServiceSecurite.IsTokenValid(Token); //teste du token long

            Compte retour = ServiceSecurite.UtilisateurWebVersCompte(UtilWeb);
            if (retour is Medecin)
            {
                return Conversion((new ServiceMedecin()).Modification(retour as Medecin));
            }
            else if (retour is Patient)
            {
                return Conversion((new ServicePatient()).Modification(retour as Patient));
            }
            else return null;



            //MedecinDAO medecinDAO = new MedecinDAO();
            //int retourIDCompte = medecinDAO.ModificationMedecin(IdMedecin, Adress, Telephone, InfoComplementaire); //on va chercher le patient tout en le modifiant
            //if (retourIDCompte != -1)
            //{
            //    //retourIDCompte = new CompteDAO().ChangementInformation(retourIDCompte, Login, null, Nom, Prenom); //on transmet l'information de d'Id du compte et on transmet a CompteDAO l'ordre de changer le mot de passe. puis on récupère l'information que l'opération c'est bien passé
            //    //if (retourIDCompte != -1) return medecinDAO.VoirMedecinSimple(IdMedecin);
            //}
            //return null;
        }

        public UtilisateurWeb ChangeMotDePass(string ValueJSON, string Token)
        {
            UtilisateurWeb UtilWeb = ServiceSecurite.UtilisateurWebDepuisValeur(ValueJSON);//convertion
            ServiceSecurite.IsTokenValid(Token); //teste du token long
            Compte retour = ServiceSecurite.UtilisateurWebVersCompte(UtilWeb);
            if (retour is Medecin)
            {
                return Conversion((new ServiceMedecin()).NouveauMotDePass(retour as Medecin));
            }
            else if (retour is Patient)
            {
                return Conversion((new ServicePatient()).NouveauMotDePass(retour as Patient));
            }
            else return null;
        }


        public UtilisateurWeb GetMedecin(string ValueJSON, string Token)
        {
            UtilisateurWeb UtilWeb = ServiceSecurite.UtilisateurWebDepuisValeur(ValueJSON);//convertion
            ServiceSecurite.IsTokenValid(Token); //teste du token long
            return Conversion(new ServiceMedecin().GetMedecin(UtilWeb.MesMedecin[0].IDWeb));
        }

        public List<UtilisateurWeb> GetListMedecin()
        {
            List<Medecin> Listmedecin = new ServiceMedecin().GetListMedecin();
            List<UtilisateurWeb> retour = new List<UtilisateurWeb>();
            if (Listmedecin.Count == 0) throw new MedecinIntrouvableException("il n'y a pas de médecin existant");
            foreach(Medecin Element in Listmedecin)
            {
                retour.Add(new UtilisateurWeb() { IDWeb = Element.IDMedecin, Identifiant = Element.Identifiant, Nom = Element.Nom, Prenom = Element.Prenom });
            }
            return retour;
        }

        public List<UtilisateurWeb> GetListMedecin(string ValueJSON,string Token)
        {
            UtilisateurWeb UtilWeb = ServiceSecurite.UtilisateurWebDepuisValeur(ValueJSON);//convertion
            ServiceSecurite.IsTokenValid(Token); //teste du token long
            return RetourListMedecin(new ServiceMedecin().GetListMedecin(UtilWeb.Nom));
        }

        public UtilisateurWeb GetPatient(string ValueJSON, string Token)
        {
            UtilisateurWeb UtilWeb = ServiceSecurite.UtilisateurWebDepuisValeur(ValueJSON);//convertion
            ServiceSecurite.IsTokenValid(Token); //teste du token long
            return Conversion(new ServicePatient().GetPatient(UtilWeb.MesPatients[0].IDWeb));
        }

        public List<UtilisateurWeb> GetListPatient(string ValueJSON, string Token)
        {
            UtilisateurWeb UtilWeb = ServiceSecurite.UtilisateurWebDepuisValeur(ValueJSON);//convertion
            ServiceSecurite.IsTokenValid(Token); //teste du token long
            return RetourListPatient(new ServicePatient().GetListPatient(UtilWeb.Nom));
        }

        public UtilisateurWeb AttributionMedecin(string ValueJSON, string Token)
        {
            UtilisateurWeb UtilWeb = ServiceSecurite.UtilisateurWebDepuisValeur(ValueJSON);//convertion
            ServiceSecurite.IsTokenValid(Token); //teste du token long
            Compte retour = ServiceSecurite.UtilisateurWebVersCompte(UtilWeb);
            //if (retour is Medecin)
            //{
            //    return Conversion((new ServiceMedecin()).AttributionPatient(UtilWeb.IDWeb, UtilWeb.MesMedecin.IDWeb));
            //}
            if (retour is Patient)
            {
                return Conversion((new ServicePatient()).AttributionMedecin(UtilWeb.IDWeb, UtilWeb.MesMedecin[0].IDWeb));
            }
            else return null;
        }

        
        public List<Modele.Medicament> GetListTotalMedicaments(string Value, string Token)
        {
            UtilisateurWeb UtilWeb = ServiceSecurite.UtilisateurWebDepuisValeur(Value);//convertion
            ServiceSecurite.IsTokenValid(Token); //teste du token long
            return new ServiceMedicament().ListeTotalMedicaments();
        }

        //public List<Modele.Medicament> GetListMedicaments(string Value)
        public List<Medicament> GetListMedicaments(string Value, string Token)
        {
            UtilisateurWeb UtilWeb = ServiceSecurite.UtilisateurWebDepuisValeur(Value);//convertion
            ServiceSecurite.IsTokenValid(Token); //teste du token long
            //UtilisateurWeb test = new UtilisateurWeb();
            //List<Medicament> toto = new List<Medicament>();
            //Medicament tata = new Medicament() { ID = 0, DenominationMedicament = "Medoc" };
            //toto.Add(tata);
            //test.IDWeb = 10;
            return new ServiceMedicament().ListeMedicaments(UtilWeb.MesMedicaments[0].DenominationMedicament);
             
            //Docteur
            //return new ServiceMedicament().ListeMedicaments(UtilWeb.MesMedicaments[0].DenominationMedicament);
        }

        public List<Medicament> GetListMedicamentsDuPatient(string Value, string Token)
        {
            UtilisateurWeb UtilWeb = ServiceSecurite.UtilisateurWebDepuisValeur(Value);//convertion
            ServiceSecurite.IsTokenValid(Token); //teste du token long
            return new ServicePatient().ListeMedicaments(UtilWeb.IDWeb);
        }

        public UtilisateurWeb AttributionPatient(string ValueJSON, string Token)
        {
            UtilisateurWeb UtilWeb = ServiceSecurite.UtilisateurWebDepuisValeur(ValueJSON);//convertion
            ServiceSecurite.IsTokenValid(Token); //teste du token long
            Compte retour = ServiceSecurite.UtilisateurWebVersCompte(UtilWeb);
            if (retour is Medecin)
            {
                return Conversion((new ServiceMedecin()).AttributionPatient(UtilWeb.MesPatients[0].IDWeb, UtilWeb.IDWeb));
            }
            else return null;
        }

        public UtilisateurWeb DesAttributionPatient(string ValueJSON, string Token)
        {
            UtilisateurWeb UtilWeb = ServiceSecurite.UtilisateurWebDepuisValeur(ValueJSON);//convertion
            ServiceSecurite.IsTokenValid(Token); //teste du token long
            Compte retour = ServiceSecurite.UtilisateurWebVersCompte(UtilWeb);
            if (retour is Medecin)
            {
                return Conversion((new ServiceMedecin()).SupprimerPatient(UtilWeb.MesPatients[0].IDWeb, UtilWeb.IDWeb));
            }
            else if (retour is Patient)
            {
                return Conversion((new ServicePatient()).SupprimerMedecin(UtilWeb.IDWeb, UtilWeb.MesMedecin[0].IDWeb));
            }
            else return null;
        }

        public UtilisateurWeb PatientAjoutMedicament(string ValueJSON, string Token)
        {
            UtilisateurWeb UtilWeb = ServiceSecurite.UtilisateurWebDepuisValeur(ValueJSON);//convertion
            ServiceSecurite.IsTokenValid(Token); //teste du token long
            return Conversion((new ServicePatient()).AjoutMedicamentAPatient(UtilWeb.MesPatients[0].IDWeb, UtilWeb.MesPatients[0].MesMedicaments[0].ID));
        }
        public UtilisateurWeb PatientAjoutFacteur(string ValueJSON, string Token)
        {
            UtilisateurWeb UtilWeb = ServiceSecurite.UtilisateurWebDepuisValeur(ValueJSON);//convertion
            ServiceSecurite.IsTokenValid(Token); //teste du token long
            return Conversion((new ServicePatient()).AjouterFacteur(UtilWeb.IDWeb, UtilWeb.MesFacteurs[0].ID));
        }
        

        public UtilisateurWeb PatientSupprMedicament(string ValueJSON, string Token)
        {
            UtilisateurWeb UtilWeb = ServiceSecurite.UtilisateurWebDepuisValeur(ValueJSON);//convertion
            ServiceSecurite.IsTokenValid(Token); //teste du token long
            return Conversion((new ServicePatient()).SupprMedicamentAPatient(UtilWeb.MesPatients[0].IDWeb, UtilWeb.MesPatients[0].MesMedicaments[0].ID));
        }

        public List<Migraine> PatientListeMigraine(string ValueJSON, string Token)
        {
            UtilisateurWeb UtilWeb = ServiceSecurite.UtilisateurWebDepuisValeur(ValueJSON);//convertion
            ServiceSecurite.IsTokenValid(Token); //teste du token long
            return (new ServicePatient()).ListMigraine(UtilWeb.IDWeb);
        }

        public List<Facteur> PatientListeFacteurs(string ValueJSON, string Token)
        {
            UtilisateurWeb UtilWeb = ServiceSecurite.UtilisateurWebDepuisValeur(ValueJSON);//convertion
            ServiceSecurite.IsTokenValid(Token); //teste du token long
            return (new ServicePatient()).ListFacteurs(UtilWeb.IDWeb);
        }

    public UtilisateurWeb Conversion(Patient patient)
        {
            if (patient == null) throw new CompteException("Une erreur est survenu sur le patient");

            return new UtilisateurWeb()
            {
                IDWeb = patient.IDPatient,
                Adresse = patient.Adresse,
                DateNaissance = ConvertionDate.ConvertionDateTimeVersString(patient.DateNaissance),
                CreePar = patient.CreePar,
                DateCreation = patient.DateCreation,
                DernierModif = patient.DernierModif,
                Identifiant = patient.Identifiant,
                MotDePass = "",
                Nom = patient.Nom,
                Prenom = patient.Prenom,
                MesMedecin = retourMedecinAttritre(patient.MesMedecin),
                MesMigraines = patient.MesMigraines,
                MesMedicaments = patient.MesMedicaments,
                MesFacteurs = patient.MesFacteurs,
                Telephone = patient.Telephone,
                TelephonePortable = patient.TelephonePortable,
                AdresseMail = patient.AdresseMail,
                Erreur = patient.Erreur,
                Token = patient.Token,
                Type = false
            };
        }
        private List<UtilisateurWeb> retourMedecinAttritre(List<Medecin> Listmedecin)
        {
            List<UtilisateurWeb> retour = new List<UtilisateurWeb>();
            //UtilisateurWeb retour = new UtilisateurWeb() { Nom = "", Prenom = "" };

            if (Listmedecin.Count == 0)
            {
                retour.Add(new UtilisateurWeb() { Nom = "", Prenom = "" });
            }
            else
            {
                foreach (Medecin Element in Listmedecin)
                {
                    retour.Add(new UtilisateurWeb()
                    {
                        IDWeb = Element.IDMedecin,
                        Identifiant = Element.Identifiant,
                        Nom = Element.Nom,
                        Prenom = Element.Prenom
                    });
                }
            }


            //if (medecin == null) return retour;
            //if (medecin.Identifiant == null) retour.Identifiant = "";
            //if (medecin.Nom == null) retour.Nom = "";
            //if (medecin.Prenom == null) retour.Prenom = "";
            return retour;
        }

        private List<UtilisateurWeb> RetourListPatient(List<Patient> ListPatient)
        {
            List<UtilisateurWeb> retour = new List<UtilisateurWeb>();
            foreach(Patient Element in ListPatient)
            {
                retour.Add(new UtilisateurWeb() { IDWeb = Element.IDPatient, Identifiant = Element.Identifiant, Nom = Element.Nom, Prenom = Element.Prenom });
            }
            return retour;
        }

        private List<UtilisateurWeb> RetourListMedecin(List<Medecin> ListMedecin)
        {
            List<UtilisateurWeb> retour = new List<UtilisateurWeb>();
            foreach (Medecin Element in ListMedecin)
            {
                retour.Add(new UtilisateurWeb() { IDWeb = Element.IDMedecin, Identifiant = Element.Identifiant, Nom = Element.Nom, Prenom = Element.Prenom });
            }
            return retour;
        }


        public UtilisateurWeb Conversion(Medecin medecin)
        {
            if (medecin == null) throw new CompteException("Une erreur est survenu sur le medecin");

            return new UtilisateurWeb()
            {
                IDWeb = medecin.IDMedecin,
                Adresse = medecin.Adresse,
                CreePar = medecin.CreePar,
                DateCreation = medecin.DateCreation,
                DernierModif = medecin.DernierModif,
                Identifiant = medecin.Identifiant,
                MotDePass = "",
                Nom = medecin.Nom,
                Prenom = medecin.Prenom,
                AdresseMail = medecin.AdresseMail,
                InfoComplementaire = medecin.InfoComplementaire,
                HoraireOuverture = medecin.HoraireOuverture,
                Telephone = medecin.Telephone,
                Erreur = medecin.Erreur,
                MesPatients = RetourListPatient(medecin.MesPatient),
                Token = medecin.Token,
                Type = true
            };
    
        }
        
        //private string NomMedecinAttitre(Medecin medecin)
        //{
        //    if (medecin == null) return "";
        //    else return medecin.Prenom + " " + medecin.Nom;
        //}
        //private int IdMedecinAttitre(Medecin medecin)
        //{
        //    if (medecin == null) return 0;
        //    else return medecin.IDMedecin;
        //}
    }
}
