using MigraineCSMiddleware.Modele;
using MigraineCSMiddleware.Service.date;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MigraineCSMiddleware.DAO
{
    public class MigraineDAO
    {
        private List<Migraine> _ListMigraine = new List<Migraine>();
        public List<Migraine> ListMigraine { get => _ListMigraine; set => _ListMigraine = value; }

        public MigraineDAO()
        {
            //Rafraichir();
        }

        //private void Rafraichir()
        //{
        //    _ListMigraine = new List<Migraine>();
        //    using (DataClasses1DataContext entity = new DataClasses1DataContext())
        //    {
        //        var ret = entity.T_MIGRAINE.ToList();
        //        ListMigraine.Clear();
        //        foreach (var element in ret)
        //        {
        //            ListMigraine.Add(new Migraine() { Debut = ConvertionDate.ConvertionStringVersDateTime(element.Debut), Fin = ConvertionDate.ConvertionStringVersDateTime(element.Fin), Intensite = (int)element.Intensite, patient = new PatientDAO().VoirPatient(element.IDPatient) });
        //        }
        //    }
        //}

        public Migraine VoirMigraine(int IDMigraine)
        {
            using (DataClasses1DataContext entity = new DataClasses1DataContext())
            {
                var retourMigraine = entity.T_MIGRAINE.FirstOrDefault(elt => elt.ID == IDMigraine);
                List<Medicament> ListMedicament = new MedicamentDAO().ListeMedicamentDeLaMigraine((int)retourMigraine.IDMedicamentsMigraine);
                List<Facteur> ListFacteurs = new FacteurDAO().ListeFacteurMigraine(IDMigraine);
                Patient patient = new PatientDAO().VoirPatient((int)entity.T_MIGRAINES_PATIENT.FirstOrDefault(elt => elt.IDMigraine == IDMigraine).IDPatient);
                return new Migraine() { ID = retourMigraine.ID, Debut = ConvertionDate.ConvertionStringVersDateTime(retourMigraine.Debut), Fin = ConvertionDate.ConvertionStringVersDateTime(retourMigraine.Fin), Facteurs = ListFacteurs, MedicamentsPris = ListMedicament, Intensite = (int)retourMigraine.Intensite };
            }
        }

        public List<Migraine> ListeMigrainePatient(int IDPatient)
        {
            using (DataClasses1DataContext entity = new DataClasses1DataContext())
            {
                List<Migraine> MesMigraine = new List<Migraine>();
                var ListMesMigraines = entity.T_MIGRAINES_PATIENT.Join(entity.T_MIGRAINE,
                    MP => MP.IDMigraine,
                    M => M.ID,
                    (MP, M) => new { IDPatient = MP.IDPatient, IDMigraine = M.ID, IDMedicamentsMigraine = M.IDMedicamentsMigraine, IDFacteursMigraine = M.IDFacteursMigraine, Intensite = M.Intensite, Debut = M.Debut, Fin = M.Fin }).Where(elt => elt.IDPatient == IDPatient).ToList();

                foreach (var Element in ListMesMigraines)
                {
                    Migraine MaMigraine = new Migraine() { ID = Element.IDMigraine, Debut = ConvertionDate.ConvertionStringVersDateTime(Element.Debut), Fin = ConvertionDate.ConvertionStringVersDateTime(Element.Fin), Intensite = (int)Element.Intensite, Facteurs = new FacteurDAO().ListeFacteurMigraine(Element.IDMigraine), MedicamentsPris = new MedicamentDAO().ListeMedicamentDeLaMigraine(Element.IDMigraine) };
                    MesMigraine.Add(MaMigraine);
                }
                return MesMigraine;
            }
        }
    }
}