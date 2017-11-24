using MigraineCSMiddleware.Modele;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MigraineCSMiddleware.DAO
{
    public class MedicamentDAO
    {
        private List<Medicament> _ListMedicament = new List<Medicament>();
        public List<Medicament> ListMedicament { get => _ListMedicament; set => _ListMedicament = value; }

        public MedicamentDAO()
        {
            Rafraichir();
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
                    ListMedicament.Add(new Medicament() { ID = element.ID, CodeCIS = (int)element.CodeCIS, DenominationMedicment = element.Denominationmedicament, FromePharmaceutique = element.Formepharmaceutique, VoieAdministration = element.Voiesadministration, StatutAdministratif = element.Statutadministratif, TypeDeProcedureAutorisation = element.Typedeprocedureautorisation, EtatCommercialisatoin = element.Etatcommercialisation, DateAmm = (DateTime)element.DateAMM, StatutDbm = element.StatutBdm, NumeroAutorisation = element.Numeroautorisationeuropeenne, Titulaire = element.Titulaire, SurveillanceRenforcee = element.Surveillancerenforcee });
                }
            }
        }

        public List<Medicament> ListMedicamentDeLaMigraine(int IDMigraine)
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
                    ListMedicament.Add(new Medicament() { DenominationMedicment = Element.Denominationmedicament, FromePharmaceutique = Element.Formepharmaceutique, VoieAdministration = Element.Voiesadministration, Quantite = (int)Element.Quantite });
                }
                return ListMedicament;
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

        //public Medicament AjoutMedicament(string nom, string idType)
        //{
        //    using (DataClasses1DataContext entity = new DataClasses1DataContext())
        //    {
        //        int retour = entity.AjoutMedicament(nom, int.Parse(idType));
        //        Rafraichir();

        //        return (from elt in _ListMedicament where (elt.Nom == nom) select elt).First();
        //    }
        //}
    }
}