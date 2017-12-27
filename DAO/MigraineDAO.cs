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
        public Migraine CreerMigraine(Migraine migraine)
        {
            using (DataClasses1DataContext entity = new DataClasses1DataContext())
            {
                //migraine.ID = entity.AjoutMigraine(migraine.Intensite, ConvertionDate.ConvertionDateTimeVersString(migraine.Debut), ConvertionDate.ConvertionDateTimeVersString(migraine.Fin));
                migraine.ID = entity.AjoutMigraine(migraine.Intensite, migraine.Debut, migraine.Fin);
                new FacteurDAO().AjouterListeFacteursAMigraine(migraine.Facteurs, migraine.ID);
                new MedicamentDAO().AjoutListMedicamentAMigraine(migraine.MedicamentsPris, migraine.ID);
                return VoirMigraine(migraine.ID);
            }
        }

        public Migraine VoirMigraine(int IDMigraine)
        {
            using (DataClasses1DataContext entity = new DataClasses1DataContext())
            {
                var retourMigraine = entity.T_MIGRAINE.FirstOrDefault(elt => elt.ID == IDMigraine);

                List<Medicament> ListMedicament = new MedicamentDAO().ListeMedicamentDeLaMigraine((int)retourMigraine.ID);
                List<Facteur> ListFacteurs = new FacteurDAO().ListeFacteurMigraine(IDMigraine);

                //Patient patient = new PatientDAO().VoirPatient((int)entity.T_MIGRAINES_PATIENT.FirstOrDefault(elt => elt.IDMigraine == IDMigraine).IDPatient);
                //return new Migraine() { ID = retourMigraine.ID, Debut = ConvertionDate.ConvertionStringVersDateTime(retourMigraine.Debut), Fin = ConvertionDate.ConvertionStringVersDateTime(retourMigraine.Fin), Facteurs = ListFacteurs, MedicamentsPris = ListMedicament, Intensite = (int)retourMigraine.Intensite };
                return new Migraine() { ID = retourMigraine.ID, Debut = retourMigraine.Debut, Fin = retourMigraine.Fin, Facteurs = ListFacteurs, MedicamentsPris = ListMedicament, Intensite = (int)retourMigraine.Intensite };
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
                    (MP, M) => new { IDPatient = MP.IDPatient, IDMigraine = M.ID, Intensite = M.Intensite, Debut = M.Debut, Fin = M.Fin }).Where(elt => elt.IDPatient == IDPatient).ToList();

                foreach (var Element in ListMesMigraines)
                {
                    Migraine MaMigraine = new Migraine()
                    {
                        ID = Element.IDMigraine,
                        DateDebut = ConvertionDate.ConvertionStringVersDateTime(Element.Debut),
                        DateFin = ConvertionDate.ConvertionStringVersDateTime(Element.Fin),
                        Duree = ConvertionDate.DureeEntreDateTime(Element.Debut, Element.Fin),
                        Debut = Element.Debut,
                        Fin = Element.Fin,
                        Moi = ConvertionDate.RechercheMoi(Element.Debut),
                        Intensite = (int)Element.Intensite,
                        Facteurs = new FacteurDAO().ListeFacteurMigraine(Element.IDMigraine),
                        MedicamentsPris = new MedicamentDAO().ListeMedicamentDeLaMigraine(Element.IDMigraine)
                    };
                    MesMigraine.Add(MaMigraine);
                }
                return MesMigraine;
            }
        }

        public Patient AjouterMigraineAPatient(int IDPatient, Migraine migraine)
        {



            using (DataClasses1DataContext entity = new DataClasses1DataContext())
            {
                //int IdMigraine = entity.AjoutMigraine(migraine.Intensite, migraine.Debut, migraine.Fin);
                migraine = CreerMigraine(migraine);
                entity.AjoutMigraineAPatient(migraine.ID, IDPatient);
                return new PatientDAO().VoirPatient(IDPatient);
            }
        }
    }
}