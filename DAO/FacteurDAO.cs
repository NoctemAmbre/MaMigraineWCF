using MigraineCSMiddleware.Modele;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MigraineCSMiddleware.DAO
{
    public class FacteurDAO
    {
        public List<TypeFacteur> ListTypeFacteur()
        {
            using (DataClasses1DataContext entity = new DataClasses1DataContext())
            {
                var retour = entity.T_TYPEFACTEUR.ToList();
                List<TypeFacteur> ListTypeFacteur = new List<TypeFacteur>();
                foreach(var Element in retour)
                {
                    ListTypeFacteur.Add(new TypeFacteur() { ID = Element.ID, Type = Element.Type });
                }
                return ListTypeFacteur;
            }
        }

        public List<TypeReponse> ListTypeReponse()
        {
            using (DataClasses1DataContext entity = new DataClasses1DataContext())
            {
                var retour = entity.T_TYPEREPONSE.ToList();
                List<TypeReponse> ListTypeReponse = new List<TypeReponse>();
                foreach (var Element in retour)
                {
                    ListTypeReponse.Add(new TypeReponse() { ID = Element.ID, Type = Element.TypeReponse, Information = Element.Information});
                }
                return ListTypeReponse;
            }
        }

        public List<Facteur> ListeFacteurs()
        {
            using (DataClasses1DataContext entity = new DataClasses1DataContext())
            {
                var retour = entity.T_FACTEUR.ToList();
                List<Facteur> ListFacteur = new List<Facteur>();
                foreach (var Element in retour)
                {
                    var retourTypeFacteur = entity.T_TYPEFACTEUR.FirstOrDefault(elt => elt.ID == Element.IDTypeFacteur);
                    TypeFacteur typeFacteur = new TypeFacteur() { ID = retourTypeFacteur.ID, Type = retourTypeFacteur.Type };

                    var retourTypeReponse = entity.T_TYPEREPONSE.FirstOrDefault(elt => elt.ID == Element.IDTypeResponse);
                    TypeReponse typeReponse = new TypeReponse() { ID = retourTypeReponse.ID, Type = retourTypeReponse.TypeReponse, Information = retourTypeReponse.Information };

                    Facteur facteur = new Facteur()
                    {
                        ID = Element.ID,
                        Nom = Element.Nom,
                        Question = Element.Question,
                        Reponse = (int)Element.Reponse,
                        TypeDeFacteur = typeFacteur,
                        TypeDeReponse = typeReponse
                    };
                    ListFacteur.Add(facteur);
                }
                return ListFacteur;
            }
        }

        public List<Facteur> ListeFacteurMigraine(int IDmigraine)
        {
            using (DataClasses1DataContext entity = new DataClasses1DataContext())
            {
                var retour = entity.T_FACTEUR
                    .Join(entity.T_FACTEURS_MIGRAINE, F => F.ID, FM => FM.IDFacteur, (F, FM) => new { ID = F.ID, IDTypeFacteur = F.IDTypeFacteur, IDTypeResponse = F.IDTypeResponse, Nom = F.Nom, IDMigraine = FM.IDMigraine, Question = F.Question, Reponse = FM.Reponse })
                    .Join(entity.T_MIGRAINE, FFM => FFM.IDMigraine, M => M.ID, (FFM, M) => new {IDMigraine = FFM.IDMigraine, ID = FFM.ID, IDTypeFacteur = FFM.IDTypeFacteur, IDTypeResponse = FFM.IDTypeResponse, Nom = FFM.Nom, Question = FFM.Question, Reponse = FFM.Reponse }).Where(elt => elt.IDMigraine == IDmigraine)
                    .ToList();

                //var retour1 = entity.T_FACTEURS_MIGRAINE.
                //    Join(entity.T_FACTEUR, FM => FM.IDFacteur, F => F.ID, (FM,F) => new { }


                List<Facteur> ListFacteur = new List<Facteur>();
                foreach(var Element in retour)
                {
                    var retourTypeFacteur = entity.T_TYPEFACTEUR.FirstOrDefault(elt => elt.ID == Element.IDTypeFacteur);
                    TypeFacteur typeFacteur = new TypeFacteur() { ID = retourTypeFacteur.ID, Type = retourTypeFacteur.Type };

                    var retourTypeReponse = entity.T_TYPEREPONSE.FirstOrDefault(elt => elt.ID == Element.IDTypeResponse);
                    TypeReponse typeReponse = new TypeReponse() { ID = retourTypeReponse.ID, Type = retourTypeReponse.TypeReponse, Information = retourTypeReponse.Information };

                    Facteur facteur = new Facteur()
                    {
                        ID = Element.ID,
                        Nom = Element.Nom,
                        Question = Element.Question,
                        Reponse = (int)Element.Reponse,
                        TypeDeFacteur = typeFacteur,
                        TypeDeReponse = typeReponse
                    };

                    ListFacteur.Add(facteur);
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
                    (F, FS) => new { ID = F.ID, IDPatient = FS.IDPatient, IDTypeFacteur = F.IDTypeFacteur, IDTypeResponse = F.IDTypeResponse, Nom = F.Nom, Question = F.Question, Reponse = F.Reponse }).Where(elt => elt.IDPatient == IDPatient).ToList();

                List<Facteur> ListFacteur = new List<Facteur>();
                foreach (var Element in retour)
                {
                    var retourTypeFacteur = entity.T_TYPEFACTEUR.FirstOrDefault(elt => elt.ID == Element.IDTypeFacteur);
                    TypeFacteur typeFacteur = new TypeFacteur() { ID = retourTypeFacteur.ID, Type = retourTypeFacteur.Type };

                    var retourTypeReponse = entity.T_TYPEREPONSE.FirstOrDefault(elt => elt.ID == Element.IDTypeResponse);
                    TypeReponse typeReponse = new TypeReponse() { ID = retourTypeReponse.ID, Type = retourTypeReponse.TypeReponse, Information = retourTypeReponse.Information };

                    Facteur facteur = new Facteur()
                    {
                        ID = Element.ID,
                        Nom = Element.Nom,
                        Question = Element.Question,
                        Reponse = (int)Element.Reponse,
                        TypeDeFacteur = typeFacteur,
                        TypeDeReponse = typeReponse
                    };

                    ListFacteur.Add(facteur);
                }
                return ListFacteur;
            }
        }

        public Facteur VoirFacteur(int IdFacteur)
        {
            using (DataClasses1DataContext entity = new DataClasses1DataContext())
            {
                var retour = entity.T_FACTEUR.Where(elt => elt.ID == IdFacteur).FirstOrDefault();


                var retourTypeFacteur = entity.T_TYPEFACTEUR.FirstOrDefault(elt => elt.ID == retour.IDTypeFacteur);
                TypeFacteur typeFacteur = new TypeFacteur() { ID = retourTypeFacteur.ID, Type = retourTypeFacteur.Type };

                var retourTypeReponse = entity.T_TYPEREPONSE.FirstOrDefault(elt => elt.ID == retour.IDTypeResponse);
                TypeReponse typeReponse = new TypeReponse() { ID = retourTypeReponse.ID, Type = retourTypeReponse.TypeReponse, Information = retourTypeReponse.Information };

                Facteur facteur = new Facteur()
                {
                    ID = retour.ID,
                    Nom = retour.Nom,
                    Question = retour.Question,
                    Reponse = (int)retour.Reponse,
                    TypeDeFacteur = typeFacteur,
                    TypeDeReponse = typeReponse
                };
                return facteur;
            }
        }

        public List<Facteur> AjouterFacteur(Facteur NouveauFacteur)
        {
            using (DataClasses1DataContext entity = new DataClasses1DataContext())
            {
                int retour = entity.AjoutFacteur(NouveauFacteur.Nom, NouveauFacteur.Question, NouveauFacteur.Reponse, NouveauFacteur.TypeDeFacteur.ID, NouveauFacteur.TypeDeReponse.ID);
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
                return entity.ModifFacteur(facteur.ID, facteur.Nom, facteur.Question, facteur.Reponse, facteur.TypeDeFacteur.ID, facteur.TypeDeReponse.ID);
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

        public void AjouterListeFacteursAMigraine(List<Facteur> facteurs, int IdMigraine)
        {
            if (facteurs != null)
            {
                foreach (Facteur facteur in facteurs)
                {
                    AjouterFacteurAMigraine(facteur.ID, IdMigraine, facteur.Reponse);
                }
            }
        }

        public int AjouterFacteurAMigraine(int IdFacteur, int IdMigraine, int Quantite)
        {
            using (DataClasses1DataContext entity = new DataClasses1DataContext())
            {
                return entity.AjoutFacteurAMigraine(IdFacteur, IdMigraine, Quantite);
            }
        }

        public void RetirerToutFacteursAMigraine(int IdMigraine)
        {
            using (DataClasses1DataContext entity = new DataClasses1DataContext())
            {
                var reponse  = entity.T_FACTEURS_MIGRAINE.Where(elt => elt.IDMigraine == IdMigraine).ToList();
                foreach(var Element in reponse)
                {
                    RetirerFacteurAMigraine(Element.IDFacteur, Element.IDMigraine);
                }
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