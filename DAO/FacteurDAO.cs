using MigraineCSMiddleware.Modele;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MigraineCSMiddleware.DAO
{
    public class FacteurDAO
    {
        public List<Facteur> ListeFacteurMigraine(int IDMigraine)
        {
            using (DataClasses1DataContext entity = new DataClasses1DataContext())
            {
                var retour = entity.T_FACTEUR
                    .Join(entity.T_FACTEURS_MIGRAINE, F => F.ID, FM => FM.IDFacteur, (F, FM) => new { TipeFacteur = F.TypeFacteur, Nom = F.Nom, Quantite = FM.Quantité, IDMigraine = FM.IDMigraine, Question = F.Question })
                    .Join(entity.T_MIGRAINE, FFM => FFM.IDMigraine, M => M.ID, (FFM, M) => new { TipeFacteur = FFM.TipeFacteur, Nom = FFM.Nom, Quantite = FFM.Quantite, Question = FFM.Question })
                    .ToList();

                List<Facteur> ListFacteur = new List<Facteur>();
                foreach(var Element in retour)
                {
                    ListFacteur.Add(new Facteur() { Nom = Element.Nom, Type = (bool)Element.TipeFacteur, Quantite = (int)Element.Quantite, Question = Element.Question  });
                }
                return ListFacteur;
            }
        }
        public List<Facteur> ListeFacteurPatient(int IDPatient)
        {
            using (DataClasses1DataContext entity = new DataClasses1DataContext())
            {
                var retour = entity.T_FACTEUR.Join(entity.T_FACTEURS,
                    F => F.ID,
                    FS => FS.IDFacteur,
                    (F, FS) => new { IDPatient = FS.IDPatient, TipeFacteur = F.TypeFacteur, Nom = F.Nom, Question = F.Question }).Where(elt => elt.IDPatient == IDPatient).ToList();

                List<Facteur> ListFacteur = new List<Facteur>();
                foreach (var Element in retour)
                {
                    ListFacteur.Add(new Facteur() { Nom = Element.Nom, Type = (bool)Element.TipeFacteur, Question = Element.Question });
                }
                return ListFacteur;
            }
        }
    }
}