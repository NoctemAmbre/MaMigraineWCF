using MigraineCSMiddleware.DAO;
using MigraineCSMiddleware.Metier;
using MigraineCSMiddleware.Modele;
using MigraineCSMiddleware.Service.Date;
using System;
using System.Collections.Generic;

namespace MigraineCSMiddleware.Service
{
    public class ServiceCompte
    {
        public List<Compte> GetListComptes()
        {
            using (DataClasses1DataContext entity = new DataClasses1DataContext())
            {
                List<Compte> Retour = new List<Compte>();
                foreach (T_COMPTE element in entity.T_COMPTE)
                {
                    Retour.Add(new Compte() { ID = (int)element.ID, Identifiant = element.Identifiant, MotDePass = null, Nom = element.Nom, Prenom = element.Prenom, DateCreation = element.DateCreation, DernierModif = element.DerniereModif, CreePar = 0, Token = element.Token});
                }
                return Retour;
            }
        }

        public int AjoutCompte(Compte compte)
        {
            using (DataClasses1DataContext entity = new DataClasses1DataContext())
            {
                DateTime Maintenant = DateTime.Now;

                T_COMPTE T_Compte =  new T_COMPTE() { Identifiant = compte.Identifiant, MotDePass = compte.MotDePass, Nom = compte.Nom, Prenom = compte.Prenom, DateCreation = Maintenant.Ticks.ToString(), DerniereModif = Maintenant.Ticks.ToString(), CreePar = 0 };
                entity.T_COMPTE.Attach(T_Compte);
                entity.SubmitChanges();
                //int retour = entity.AjoutCompte(compte.Login, compte.MotDePass, compte.Nom, compte.Prenom, Maintenant.ToLongDateString, Maintenant.ToLongDateString, 0);
                return 0;
            }
        }
    }
}