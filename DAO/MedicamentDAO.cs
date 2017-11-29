using MigraineCSMiddleware.Modele;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace MigraineCSMiddleware.DAO
{
    public class MedicamentDAO
    {
        private List<Medicament> _ListMedicament = new List<Medicament>();
        public List<Medicament> ListMedicament { get => _ListMedicament; set => _ListMedicament = value; }

        public MedicamentDAO()
        {
            //Rafraichir();
        }

        /// <summary>
        /// méthode pour lecture de la database et remplissage de la List de Patient
        /// </summary>
        private void Rafraichir()
        {
            _ListMedicament = new List<Medicament>();
            using (DataClasses1DataContext entity = new DataClasses1DataContext())
            {
                var ret = entity.T_MEDICAMENT.ToList();
                ListMedicament.Clear();
                foreach (var element in ret)
                {
                    //ListMedicament.Add(new Medicament() { ID = element.ID, CodeCIS = (int)element.CodeCIS, DenominationMedicament = element.Denominationmedicament, FormePharmaceutique = element.Formepharmaceutique, VoiesAdministration = element.Voiesadministration, StatutAdministratif = element.Statutadministratif, TypeDeProcedureAutorisation = element.Typedeprocedureautorisation, EtatCommercialisatoin = element.Etatcommercialisation, DateAmm = (DateTime)element.DateAMM, Statutbdm = element.StatutBdm, NumeroAutorisation = element.Numeroautorisationeuropeenne, Titulaire = element.Titulaire, SurveillanceRenforcee = element.Surveillancerenforcee });
                    ListMedicament.Add(new Medicament() { ID = element.ID, DenominationMedicament = element.Denominationmedicament, FormePharmaceutique = element.Formepharmaceutique, VoiesAdministration = element.Voiesadministration });
                }
            }
        }

        public List<Medicament> ListeMesMedicaments(int IDPatient)
        {
            using (DataClasses1DataContext entity = new DataClasses1DataContext())
            {
                var ListMedicaments = entity.T_MEDICAMENT.Join(entity.T_ORDONNANCE,
                    M => M.ID,
                    O => O.idMedicament,
                    (M, O) => new { IDPatient = O.idPatient, Quantite = O.Quantité, Denominationmedicament = M.Denominationmedicament, Formepharmaceutique = M.Formepharmaceutique, Voiesadministration = M.Voiesadministration }).ToList();
                var MedicamentDuPatient = ListMedicaments.Where(elt => elt.IDPatient == IDPatient).ToList();
                List<Medicament> ListMedicament = new List<Medicament>();
                foreach (var Element in MedicamentDuPatient)
                {
                    //ListMedicament.Add(new Medicament() { DenominationMedicament = Element.Denominationmedicament, FormePharmaceutique = Element.Formepharmaceutique, VoiesAdministration = Element.Voiesadministration, Quantite = (int)Element.Quantite });
                    ListMedicament.Add(new Medicament() { DenominationMedicament = Element.Denominationmedicament, FormePharmaceutique = Element.Formepharmaceutique, VoiesAdministration = Element.Voiesadministration });
                }
                return ListMedicament;
            }
        }

        public List<Medicament> ListeMedicamentDeLaMigraine(int IDMigraine)
        {
            using (DataClasses1DataContext entity = new DataClasses1DataContext())
            {
                var ListMedicaments = entity.T_MEDICAMENT.Join(entity.T_MEDICAMENTS_MIGRAINE,
                    M => M.ID,
                    MM => MM.IDMedicament,
                    (M, MM) => new { IDMigraine = MM.IDMigraine, Quantite = MM.Quantité, Denominationmedicament = M.Denominationmedicament, Formepharmaceutique = M.Formepharmaceutique, Voiesadministration = M.Voiesadministration }).ToList();
                var MedicamentPourMigraine = ListMedicaments.Where(elt => elt.IDMigraine == IDMigraine).ToList();
                List<Medicament> ListMedicament = new List<Medicament>();
                foreach(var Element in MedicamentPourMigraine)
                {
                    //ListMedicament.Add(new Medicament() { DenominationMedicament = Element.Denominationmedicament, FormePharmaceutique = Element.Formepharmaceutique, VoiesAdministration = Element.Voiesadministration, Quantite = (int)Element.Quantite });
                    ListMedicament.Add(new Medicament() { DenominationMedicament = Element.Denominationmedicament, FormePharmaceutique = Element.Formepharmaceutique, VoiesAdministration = Element.Voiesadministration });
                }
                return ListMedicament;
            }
        }

        /// <summary>
        /// Liste des médicaments sont le mot recherché est contenu dans l'intitulé
        /// </summary>
        /// <param name="Nom"></param>
        /// <returns></returns>
        public List<Medicament> ChercheMedicament(string Nom)
        {
            using (DataClasses1DataContext entity = new DataClasses1DataContext())
            {
                var ListMedicaments = entity.T_MEDICAMENT.Where(elt =>  elt.Denominationmedicament.Contains(Nom)).ToList();
                List<Medicament> ListRetour= new List<Medicament>();
                foreach(var Element in ListMedicaments)
                {
                    ListRetour.Add(new Medicament() { DenominationMedicament = Element.Denominationmedicament, FormePharmaceutique = Element.Formepharmaceutique, VoiesAdministration = Element.Voiesadministration, ID = Element.ID, EtatCommercialisatoin = Element.Etatcommercialisation, StatutAdministratif = Element.Statutadministratif, TypeDeProcedureAutorisation = Element.Typedeprocedureautorisation, CodeCIS = (int)Element.CodeCIS, NumeroAutorisation = Element.Numeroautorisationeuropeenne, Statutbdm = Element.StatutBdm, SurveillanceRenforcee = Element.Surveillancerenforcee, Titulaire = Element.Titulaire });
                }
                return ListRetour;
            }
        }

        /// <summary>
        /// List de tout les médicaments
        /// </summary>
        /// <returns></returns>
        public List<Medicament> ListeMedicements()
        {
            using (DataClasses1DataContext entity = new DataClasses1DataContext())
            {
                var ListMedicaments = entity.T_MEDICAMENT.ToList();
                List<Medicament> ListRetour = new List<Medicament>();
                foreach (var Element in ListMedicaments)
                {
                    ListRetour.Add(new Medicament() { DenominationMedicament = Element.Denominationmedicament, FormePharmaceutique = Element.Formepharmaceutique, VoiesAdministration = Element.Voiesadministration });
                }
                return ListRetour;
            }
        }

        //var res = entity.T_MEDICAMENT.ToList();


        //    foreach (T_MEDICAMENT element in entity.T_MEDICAMENT)
        //    {
        //        string Type;
        //        List<T_MEDICAMENT> test = (from elt in entity.T_MEDICAMENT select elt).ToList();
        //       T_TYPE res = (from elt in entity.T_TYPE where (elt.ID == element.idType) select elt).First();
        //        if (res != null) Type = res.Type; else Type = "";

        //        Medicament medicament = new Medicament() { Id = element.ID, Nom = element.nom, Type = Type };
        //        ListMedicament.Add(medicament);
        //    }
        //}


        public Medicament VoirMedicament(int IdMedicament)
        {
            return _ListMedicament.SingleOrDefault(elt => elt.ID == IdMedicament);
        }

        public Patient AjoutMedicamentAuPatient(int IDMedicament, int IDPatient, int Quantite)
        {
            using (DataClasses1DataContext entity = new DataClasses1DataContext())
            {
                int retour = entity.AjoutMedicamentAPatient(IDMedicament, IDPatient, Quantite);

                return new PatientDAO().VoirPatient(IDPatient);
            }
        }
        public Patient SupprMedicamentDuPatient(int IDMedicament, int IDPatient)
        {
            using (DataClasses1DataContext entity = new DataClasses1DataContext())
            {
                int retour = entity.SupprMedicamentDuPatient(IDMedicament, IDPatient);
                return new PatientDAO().VoirPatient(IDPatient);
            }
        }
    }
}