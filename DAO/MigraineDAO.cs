using MigraineCSMiddleware.Modele;
using MigraineCSMiddleware.Service.Date;
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
            Rafraichir();
        }

        private void Rafraichir()
        {
            _ListMigraine = new List<Migraine>();
            using (DataClasses1DataContext entity = new DataClasses1DataContext())
            {
                var ret = entity.T_MIGRAINE.ToList();
                ListMigraine.Clear();
                foreach (var element in ret)
                {
                    ListMigraine.Add(new Migraine() { Debut = ConvertionDate.ConvertionStringVersDateTime(element.Debut), Fin = ConvertionDate.ConvertionStringVersDateTime(element.Fin), Intensite = (int)element.Intensite, patient = new PatientDAO().VoirPatient(element.IDPatient) });
                }
            }
        }

        public Migraine VoirMigraine(int IDMigraine)
        {
            using (DataClasses1DataContext entity = new DataClasses1DataContext())
            {
                var retour = entity.T_MIGRAINES_PATIENT.Join(entity.T_PATIENT,
                    M => M.IDPatient,
                    P => P.IDk,
                    (M,P) => {  }
                    )
            }
        }
    }
}