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

        public Migraine ModifierMigraine(Migraine migraine)
        {
            using (DataClasses1DataContext entity = new DataClasses1DataContext())
            {
                if (string.IsNullOrEmpty(migraine.Fin)) migraine.Fin = migraine.Debut;  //cela permet d'éviter les incompréhentions d'Angular a la lecture d'une date vide
                migraine.ID = entity.ModifMigraine(migraine.ID, migraine.Intensite, migraine.Debut, migraine.Fin);
                new FacteurDAO().RetirerToutFacteursAMigraine(migraine.ID);
                new FacteurDAO().AjouterListeFacteursAMigraine(migraine.Facteurs, migraine.ID);
                new MedicamentDAO().SupprToutMedicamentDeLaMigraine(migraine.ID);
                new MedicamentDAO().AjoutListMedicamentAMigraine(migraine.MedicamentsPris, migraine.ID);
                return VoirMigraine(migraine.ID);
            }
        }

        public Migraine CreerMigraine(Migraine migraine)
        {
            using (DataClasses1DataContext entity = new DataClasses1DataContext())
            {
                //migraine.ID = entity.AjoutMigraine(migraine.Intensite, ConvertionDate.ConvertionDateTimeVersString(migraine.Debut), ConvertionDate.ConvertionDateTimeVersString(migraine.Fin));
                if (string.IsNullOrEmpty(migraine.Fin)) migraine.Fin = migraine.Debut;  //cela permet d'éviter les incompréhentions d'Angular a la lecture d'une date vide
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
                    (MP, M) => new { IDPatient = MP.IDPatient, Complet = MP.Complet, IDMigraine = M.ID, Intensite = M.Intensite, Debut = M.Debut, Fin = M.Fin }).Where(elt => elt.IDPatient == IDPatient).ToList();

                foreach (var Element in ListMesMigraines)
                {
                    Migraine MaMigraine = new Migraine()
                    {
                        ID = Element.IDMigraine,
                        DateTimeDebut = ConvertionDate.ConvertionStringVersDateTime(Element.Debut),
                        DateTimeFin = ConvertionDate.ConvertionStringVersDateTime(Element.Fin),
                        DateDebut = ConvertionDate.ConvertionStringVersDate(Element.Debut),
                        DateFin = ConvertionDate.ConvertionStringVersDate(Element.Fin),
                        HeureDebut = ConvertionDate.ConvertionStringVersHeure(Element.Debut),
                        HeureFin = ConvertionDate.ConvertionStringVersHeure(Element.Fin),
                        Duree = ConvertionDate.DureeEntreDateTime(Element.Debut, Element.Fin),
                        Debut = Element.Debut,
                        Fin = Element.Fin,
                        Moi = ConvertionDate.RechercheMoi(Element.Debut),
                        Intensite = (int)Element.Intensite,
                        Facteurs = new FacteurDAO().ListeFacteurMigraine(Element.IDMigraine),
                        MedicamentsPris = new MedicamentDAO().ListeMedicamentDeLaMigraine(Element.IDMigraine),
                        Complet = Element.Complet
                    };
                    MesMigraine.Add(MaMigraine);
                }
                return MesMigraine;
            }
        }

        public Patient AjouterMigraineAPatient(int IDPatient, Migraine migraine, bool Complet)
        {
            if (migraine.ID != null)
            {
                using (DataClasses1DataContext entity = new DataClasses1DataContext())
                {
                    migraine = ModifierMigraine(migraine);
                    entity.ModifMigraineAPatient(migraine.ID, IDPatient, Complet);
                    return new PatientDAO().VoirPatient(IDPatient);
                }
            }
            else
            {
                using (DataClasses1DataContext entity = new DataClasses1DataContext())
                {
                    //int IdMigraine = entity.AjoutMigraine(migraine.Intensite, migraine.Debut, migraine.Fin);
                    migraine = CreerMigraine(migraine);
                    entity.AjoutMigraineAPatient(migraine.ID, IDPatient, Complet);
                    return new PatientDAO().VoirPatient(IDPatient);
                }
            }
        }
    }
}