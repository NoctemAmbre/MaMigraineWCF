using MigraineCSMiddleware.DAO;
using MigraineCSMiddleware.Metier;
using MigraineCSMiddleware.Modele;
using MigraineCSMiddleware.Service.patient;
using System.Collections.Generic;
using System.Linq;

namespace MigraineCSMiddleware.Service.medecin
{
    public class ServiceMedecin
    {

        public Medecin GetMedecin(int ID)
        {
            return new MedecinDAO().VoirMedecin(ID);
        }

        public List<Medecin> GetListMedecin()
        {
            return new MedecinDAO().GetListMedecin();
        }
        public List<Medecin> GetListMedecin(int ID)
        {
            return new MedecinDAO().ListMedecinDuPatient(ID);
        }

        public List<Medecin> GetListMedecin(string Nom)
        {
            return new MedecinDAO().GetListMedecin(Nom);
        }

        public Medecin AjoutMedecin(Medecin medecin)
        {
            return new MedecinDAO().AjoutMedecin(medecin);
        }
        public Medecin Login(string Login, string Pass)
        {
            return new MedecinDAO().Login(Login, Pass);
        }

        public Medecin Modification(Medecin medecin)
        {
            return new MedecinDAO().Modification(medecin);
        }

        public Medecin AttributionPatient(int IDpatient, int IDMedecin)
        {
            PatientDAO patientDAO = new PatientDAO();
            MedecinDAO medecinDAO = new MedecinDAO();
            if (!patientDAO.IsPatient(IDpatient)) throw new PatientIncorrecteException("Ce compte n'est pas un compte Patient", medecinDAO.VoirMedecin(IDMedecin));
            if (!medecinDAO.IsMedecin(IDMedecin)) throw new MedecinIncorrecteException("Ce compte n'est pas un compte Medecin", medecinDAO.VoirMedecin(IDMedecin));
            Medecin medecin = medecinDAO.VoirMedecin(IDMedecin);
            Patient patient = patientDAO.VoirPatient(IDpatient);
            if (patient.MesMedecin.SingleOrDefault(elt => elt.IDMedecin == IDMedecin) != null)
            {
                medecin.Erreur = "Le patient " + patient.Nom + " " + patient.Prenom + " " + patient.Identifiant + " vous est déjà attribué";
                throw new DejaMedecinAttribueException("Il y a déjà un médecin attribué à ce Patient", medecin);               
            }

            patientDAO.AttributionMedecin(patient, medecinDAO.VoirMedecinSimple(IDMedecin));
            return medecinDAO.VoirMedecin(IDMedecin);
        }
        public Medecin SupprimerPatient(int IDpatient, int IDMedecin)
        {
            PatientDAO patientDAO = new PatientDAO();
            MedecinDAO medecinDAO = new MedecinDAO();
            if (!patientDAO.IsPatient(IDpatient)) throw new PatientIncorrecteException("Ce compte n'est pas un compte Patient", medecinDAO.VoirMedecin(IDMedecin));
            if (!medecinDAO.IsMedecin(IDMedecin)) throw new MedecinIncorrecteException("Ce compte n'est pas un compte Medcin", medecinDAO.VoirMedecin(IDMedecin));
            Medecin medecin = medecinDAO.VoirMedecin(IDMedecin);
            Patient patient = patientDAO.VoirPatient(IDpatient);
            if (patient.MesMedecin.SingleOrDefault(elt => elt.IDMedecin == IDMedecin) == null) //S'il n'y a pas de médecin pour patient
            {
                medecin.Erreur = "Le patient " + patient.Nom + " " + patient.Prenom + " " + patient.Identifiant + " vous est déjà attribué";
                throw new PatientNonPresentException("Impossible de supprimer le patient du Médecin car attribution pas présente", medecin);
            }
            else patientDAO.SupprMedecinDuPatient(patient, medecin);
            
            return medecinDAO.VoirMedecin(medecin.IDMedecin);
        }
        
        private bool IsMedecin(int idMedecin)
        {
            using (DataClasses1DataContext entity = new DataClasses1DataContext())
            {
                T_MEDECIN res = (from elt in entity.T_MEDECIN where (elt.ID == idMedecin) select elt).First();
                if (res == null) return false;
                else return true;
            }
        }

        /// <summary>
        /// Changement mot de passe du compte dépendant du médedin
        /// </summary>
        /// <param name="IdMedecin"></param>
        /// <param name="MotDePass"></param>
        /// <returns></returns>

        public Medecin NouveauMotDePass(Medecin medecin)
        {
            return new MedecinDAO().NouveauMotDePass(medecin);
        }


    }
}