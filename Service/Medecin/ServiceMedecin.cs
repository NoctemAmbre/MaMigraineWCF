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
            return new MedecinDAO().ListMedecin;
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
            if (!patientDAO.IsPatient(IDpatient)) throw new PatientIncorectException("Ce compte c'est pas un compte Patient");
            if (!medecinDAO.IsMedecin(IDMedecin)) throw new MedecinIncorectException("Ce compte n'est pas un compte Medcin");
            Medecin medecin;
            Patient patient = patientDAO.VoirPatient(IDpatient);
            if (patient.MesMedecin.SingleOrDefault(elt => elt.IDMedecin == IDMedecin) != null)
            {
                medecin = medecinDAO.VoirMedecin(IDMedecin);
                medecin.Erreur = "Le patient " + patient.Nom + " " + patient.Prenom + " " + patient.Identifiant + " vous est déjà attribué";
                return medecin;
                //throw new DejaMedecinAttribueException("Il y a déjà un médecin attribué à ce Patient");
            }

            patientDAO.AttributionMedecin(patient, medecinDAO.VoirMedecinSimple(IDMedecin));
            return medecinDAO.VoirMedecin(IDMedecin);
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