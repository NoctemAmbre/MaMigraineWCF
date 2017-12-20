using MigraineCSMiddleware.Modele;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MigraineCSMiddleware.DAO
{
    public class FacteurDAO
    {
       

        public List<Facteur> ListeFacteurs()
        {
            using (DataClasses1DataContext entity = new DataClasses1DataContext())
            {

                var retour = entity.T_FACTEUR.ToList();
                List<Facteur> ListFacteur = new List<Facteur>();
                foreach (var Element in retour)
                {
                    ListFacteur.Add(new Facteur() { ID = Element.ID, Nom = Element.Nom, Type = (bool)Element.TypeFacteur, Question = Element.Question });
                }
                return ListFacteur;
            }
        }

        public List<Facteur> ListeFacteurMigraine(int IDMigraine)
        {
            using (DataClasses1DataContext entity = new DataClasses1DataContext())
            {
                var retour = entity.T_FACTEUR
                    .Join(entity.T_FACTEURS_MIGRAINE, F => F.ID, FM => FM.IDFacteur, (F, FM) => new { ID = F.ID, TipeFacteur = F.TypeFacteur, Nom = F.Nom, Quantite = FM.Quantité, IDMigraine = FM.IDMigraine, Question = F.Question })
                    .Join(entity.T_MIGRAINE, FFM => FFM.IDMigraine, M => M.ID, (FFM, M) => new {ID = FFM.ID, TipeFacteur = FFM.TipeFacteur, Nom = FFM.Nom, Quantite = FFM.Quantite, Question = FFM.Question })
                    .ToList();

                List<Facteur> ListFacteur = new List<Facteur>();
                foreach(var Element in retour)
                {
                    ListFacteur.Add(new Facteur() { ID = Element.ID, Nom = Element.Nom, Type = (bool)Element.TipeFacteur, Quantite = (int)Element.Quantite, Question = Element.Question  });
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
                    (F, FS) => new { ID = F.ID, IDPatient = FS.IDPatient, TipeFacteur = F.TypeFacteur, Nom = F.Nom, Question = F.Question }).Where(elt => elt.IDPatient == IDPatient).ToList();

                List<Facteur> ListFacteur = new List<Facteur>();
                foreach (var Element in retour)
                {
                    ListFacteur.Add(new Facteur() { ID = Element.ID, Nom = Element.Nom, Type = (bool)Element.TipeFacteur, Question = Element.Question });
                }
                return ListFacteur;
            }
        }

        public Facteur VoirFacteur(int IdFacteur)
        {
            using (DataClasses1DataContext entity = new DataClasses1DataContext())
            {
                var retour = entity.T_FACTEUR.Where(elt => elt.ID == IdFacteur).FirstOrDefault();
                return new Facteur() { ID = retour.ID, Nom = retour.Nom, Type = (bool)retour.TypeFacteur, Question = retour.Question };
            }
        }

        public List<Facteur> AjouterFacteur(Facteur NouveauFacteur)
        {
            using (DataClasses1DataContext entity = new DataClasses1DataContext())
            {
                int retour = entity.AjoutFacteur(NouveauFacteur.Type, NouveauFacteur.Nom, NouveauFacteur.Question);
                if (retour != -1) return ListeFacteurs();
            }
            return null;
        }
        public int SupprimerFacteur(int IdFacteur)
        {
            using (DataClasses1DataContext entity = new DataClasses1DataContext())
            {
                return entity.SupprFacteur(IdFacteur);
            }
        }

        public int ModifierFacteur(Facteur facteur)
        {
            using (DataClasses1DataContext entity = new DataClasses1DataContext())
            {
                return entity.ModifFacteur(facteur.ID, facteur.Type, facteur.Nom, facteur.Question);
            }
        }

        public int AjouterFacteurAPatient(int IdFacteur, int IdPatient)
        {
            using (DataClasses1DataContext entity = new DataClasses1DataContext())
            {
                return entity.AjoutFacteurAPatient(IdFacteur, IdPatient);
            }
        }

        public int RetirerFacteurAPatient(int IdFacteur, int IdPatient)
        {
            using (DataClasses1DataContext entity = new DataClasses1DataContext())
            {
                return entity.SupprFacteurDuPatient(IdFacteur, IdPatient);
            }
        }

        public int AjouterFacteurAMigraine(int IdFacteur, int IdMigraine, int Quantite)
        {
            using (DataClasses1DataContext entity = new DataClasses1DataContext())
            {
                return entity.AjoutFacteurAMigraine(IdFacteur, IdMigraine, Quantite);
            }
        }

        public int RetirerFacteurAMigraine(int IdFacteur, int IdMigraine)
        {
            using (DataClasses1DataContext entity = new DataClasses1DataContext())
            {
                return entity.SupprFacteurDeMigraine(IdFacteur, IdMigraine);
            }
        }
    }
}