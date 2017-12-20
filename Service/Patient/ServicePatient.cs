using MigraineCSMiddleware.DAO;
using MigraineCSMiddleware.Metier;
using MigraineCSMiddleware.Modele;
using MigraineCSMiddleware.Service.medecin;
using MigraineCSMiddleware.Service.securite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace MigraineCSMiddleware.Service.patient
{
    public class ServicePatient
    {
        

        public Patient GetPatient(int ID)
        {
            return new PatientDAO().VoirPatient(ID);
        }

        public List<Patient> GetListPatient()
        {
            return new PatientDAO().ListPatient();
        }

        public List<Patient> GetListPatient(string Nom)
        {
            return new PatientDAO().GetListPatient(Nom);
        }

        public Patient AjoutPatient(Patient patient)
        {
            return new PatientDAO().AjoutPatient(patient);
        }

        public Patient Login(string Login, string Pass)
        {
            //X509Certificate Test = new X509Certificate();
            return new PatientDAO().Login(Login, Pass);
        }

        public Patient AttributionMedecin(int IDpatient, int IDMedecin)
        {
            PatientDAO patientDAO = new PatientDAO();
            MedecinDAO medecinDAO = new MedecinDAO();
            if (!patientDAO.IsPatient(IDpatient)) throw new PatientIncorrecteException("Ce compte n'est pas un compte Patient", medecinDAO.VoirMedecin(IDMedecin));
            if (!medecinDAO.IsMedecin(IDMedecin)) throw new MedecinIncorrecteException("Ce compte n'est pas un compte Medcin", medecinDAO.VoirMedecin(IDMedecin));

            Patient patient = patientDAO.VoirPatient(IDpatient);
            if (patient.MesMedecin != null)
            {

                var resultat = patient.MesMedecin.FirstOrDefault(elt => elt.IDMedecin == IDMedecin);
                if (resultat != null) throw new DejaMedecinAttribueException("Il y a déjà un médecin attribué à ce Patient", patient);
                
                //foreach (Medecin element in patient.MesMedecin)
                //{
                //    if (element.IDMedecin == IDMedecin)
                //    {
                //        patient.Erreur = "Le Médecin " + element.Nom + " " + element.Prenom + " fait déjà partit de vos médecin de référence";
                //        throw new DejaMedecinAttribueException("Il y a déjà un médecin attribué à ce Patient", element);
                //    }
                //}
                //return patient;
                
            }

            Medecin medecin = medecinDAO.VoirMedecinSimple(IDMedecin);
            return patientDAO.AttributionMedecin(patient, medecin);
        }

        public Patient AjoutMedicamentAPatient(int IdPatient, int IdMedicament)
        {
            PatientDAO patientDAO = new PatientDAO();
            if (!patientDAO.IsPatient(IdPatient)) throw new PatientIncorrecteException("Ce compte n'est pas un compte Patient", patientDAO.VoirPatient(IdPatient));
            Patient patient = new MedicamentDAO().AjoutMedicamentAuPatient(IdMedicament, IdPatient);
            return patient;
        }
        public Patient SupprMedicamentAPatient(int IdPatient, int IdMedicament)
        {
            PatientDAO patientDAO = new PatientDAO();
            if (!patientDAO.IsPatient(IdPatient)) throw new PatientIncorrecteException("Ce compte n'est pas un compte Patient", patientDAO.VoirPatient(IdPatient));
            Patient patient = new MedicamentDAO().SupprMedicamentDuPatient(IdMedicament, IdPatient);
            return patient;
        }
        


        private bool IsPatient(int idPatient)
        {
            using (DataClasses1DataContext entity = new DataClasses1DataContext())
            {
                T_PATIENT res = (from elt in entity.T_PATIENT where (elt.ID == idPatient) select elt).FirstOrDefault();
                if (res == null) return false;
                else return true;
            }
        }
        /// <summary>
        /// Changement mode passe du compte dépendant du patient
        /// </summary>
        /// <param name="IdPatient"></param>
        /// <param name="MotDePass"></param>
        /// <returns></returns>
        public Patient NouveauMotDePass(Patient patient)
        {
            return new PatientDAO().NouveauMotDePass(patient);
        }

        public Patient Modification(Patient patient)
        {
            return new PatientDAO().Modification(patient);
            //Patient patient = new PatientDAO().ModificationPatient(IdPatient, DateNaissance, IdMedecin); //on va chercher le patient
            //int retourIDcompte = new CompteDAO().ChangementInformation(patient.ID, Login, null, Nom, Prenom); //on transmet l'information de d'Id du compte et on transmet a CompteDAO l'ordre de changer le mot de passe. puis on récupère l'information que l'opération c'est bien passé
            //if (retourIDcompte != -1) return patient;

            
        }

        //public List<Patient> GetListMesPatient(int IdMedecin)
        //{
        //    return new PatientDAO().ListPatientDuMedecin(IdMedecin);
        //}

        public Patient SupprimerMedecin(int IDpatient, int IDMedecin)
        {
            PatientDAO patientDAO = new PatientDAO();
            MedecinDAO medecinDAO = new MedecinDAO();
            if (!patientDAO.IsPatient(IDpatient)) throw new PatientIncorrecteException("Ce compte n'est pas un compte Patient", patientDAO.VoirPatient(IDpatient));
            if (!medecinDAO.IsMedecin(IDMedecin)) throw new MedecinIncorrecteException("Ce compte n'est pas un compte Medecin", patientDAO.VoirPatient(IDpatient));
            Medecin medecin = medecinDAO.VoirMedecin(IDMedecin);
            Patient patient = patientDAO.VoirPatient(IDpatient);
            if (patient.MesMedecin.SingleOrDefault(elt => elt.IDMedecin == IDMedecin) == null) //S'il n'y a pas de médecin pour patient
            {
                medecin.Erreur = "Le patient " + patient.Nom + " " + patient.Prenom + " " + patient.Identifiant + " vous est déjà attribué";
                throw new PatientNonPresentException("Impossible de supprimer le patient du Médecin car attribution pas présente", medecin);
            }
            else patientDAO.SupprMedecinDuPatient(patient, medecin);
            return patientDAO.VoirPatient(patient.IDPatient);
        }

        public Medecin SupprimerPatient(int IDpatient, int IDMedecin)
        {
            PatientDAO patientDAO = new PatientDAO();
            MedecinDAO medecinDAO = new MedecinDAO();
            if (!patientDAO.IsPatient(IDpatient)) throw new PatientIncorrecteException("Ce compte n'est pas un compte Patient", patientDAO.VoirPatient(IDpatient));
            if (!medecinDAO.IsMedecin(IDMedecin)) throw new MedecinIncorrecteException("Ce compte n'est pas un compte Medcin", medecinDAO.VoirMedecin(IDMedecin));
            Medecin medecin = medecinDAO.VoirMedecin(IDMedecin);
            Patient patient = patientDAO.VoirPatient(IDpatient);
            if (patient.MesMedecin.SingleOrDefault(elt => elt.IDMedecin == IDMedecin) == null) //S'il n'y a pas de médecin pour patient
            {
                medecin.Erreur = "Le patient " + patient.Nom + " " + patient.Prenom + " " + patient.Identifiant + " vous est déjà attribué";
                throw new PatientNonPresentException("Impossible de supprimer le patient du Médecin car attribution non présente", medecin);
            }
            else patientDAO.SupprMedecinDuPatient(patient, medecin);

            return medecinDAO.VoirMedecin(medecin.IDMedecin);
        }

        public List<Migraine> ListMigraine(int IDPatient)
        {
            if (IsPatient(IDPatient))
            {
                return new MigraineDAO().ListeMigrainePatient(IDPatient);
            }
            else throw new PatientIncorrecteException("Ce compte n'est pas un compte Patient", new PatientDAO().VoirPatient(IDPatient));
        }

        public List<Medicament> ListeMedicaments(int IDPatient)
        {
            if (IsPatient(IDPatient))
            {
                return new MedicamentDAO().ListeMesMedicaments(IDPatient);
            }
            else throw new PatientIncorrecteException("Ce compte n'est pas un compte Patient", new PatientDAO().VoirPatient(IDPatient));
        }

        public List<Facteur> ListFacteurs(int IDPatient)
        {
            if (IsPatient(IDPatient))
            {
                return new FacteurDAO().ListeFacteurPatient(IDPatient);
            }
            else throw new PatientIncorrecteException("Ce compte n'est pas un compte Patient", new PatientDAO().VoirPatient(IDPatient));
        }

        public Patient AjouterFacteur(int IDPatient, int IDFacteur)
        {
            if (IsPatient(IDPatient))
            {
                new FacteurDAO().AjouterFacteurAPatient(IDFacteur, IDPatient);
                return new PatientDAO().VoirPatient(IDPatient);

            }
            else throw new PatientIncorrecteException("Ce compte n'est pas un compte Patient", new PatientDAO().VoirPatient(IDPatient));
        }


    }
}