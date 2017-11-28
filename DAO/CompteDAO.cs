using MigraineCSMiddleware.Modele;
using MigraineCSMiddleware.Service.compte;
using MigraineCSMiddleware.Service.date;
using MigraineCSMiddleware.Service.securite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MigraineCSMiddleware.DAO
{
    public class CompteDAO
    {
        /// <summary>
        /// Methode pour le changement d'une information du compte
        /// </summary>
        /// <param name="IdCompte"></param>
        /// <param name="Login"></param>
        /// <param name="MotDePass"></param>
        /// <param name="Nom"></param>
        /// <param name="Prenom"></param>
        /// <returns>On retourne une valeur booléenne définissant si l'opération c'est bien passé</returns>
        public int ChangementInformation(int IdCompte, string Identifiant, string MotDePass, string Nom, string Prenom, string DateCreation, string DerniereModif, int CreePar, string AdressMail, string Token)
        {
            using (DataClasses1DataContext entity = new DataClasses1DataContext())
            {
                T_COMPTE t_compte = entity.T_COMPTE.Single(c => c.ID == IdCompte);

                if (Identifiant != null) t_compte.Identifiant = Identifiant;
                if (MotDePass != null) t_compte.MotDePass = t_compte.MotDePass;
                if (Nom != null) t_compte.Nom = t_compte.Nom;
                if (Prenom != null) t_compte.Prenom = t_compte.Prenom;
                t_compte.DerniereModif = ConvertionDate.ConvertionDateTimeVersString(DateTime.Now);
                if (AdressMail != null) t_compte.AdressMail = t_compte.AdressMail;
                t_compte.Token = Token;
                entity.SubmitChanges();
                return IdCompte;
            }
            //if (Identifiant.Count() > 20) throw new LoginTropLongException("Login trop long", IdCompte);
            //if (MotDePass.Count() > 20) throw new MDPTropLongException("Mot de passe trop long", IdCompte);
            //if (Nom.Count() > 30) throw new NomTropLongException("Nom trop long", IdCompte);
            //if (Prenom.Count() > 30) throw new PrenomTropLongException("Prénom trop long", IdCompte);
            //if (MotDePass == null | MotDePass == "") throw new MDPNullException("Le mot de passe est vide ou null", IdCompte);
            //using (DataClasses1DataContext entity = new DataClasses1DataContext())
            //{
            //    return entity.ModifCompte(IdCompte, Identifiant, MotDePass, Nom, Prenom, DateCreation, DerniereModif, CreePar, AdressMail, Token);
            //}
        }
        public Compte GetCompte(string Identifiant)
        {
            List<Compte> _ListCompte = new List<Compte>();
            using (DataClasses1DataContext entity = new DataClasses1DataContext())
            {
                T_COMPTE retour = entity.T_COMPTE.Where(compte => compte.Identifiant == Identifiant).SingleOrDefault();
                //T_COMPTE res = (from elt in entity.T_COMPTE where (elt.Identifiant == Logincompte) select elt).First();
                if (retour == null) throw new AutentificationIncorrecteException(Identifiant, "Identifiant ou mot de passe incorrecte");
                else return new Compte() { ID = retour.ID, Identifiant = retour.Identifiant, MotDePass = retour.MotDePass, Nom = retour.Nom, Prenom = retour.Prenom};
            }
     
        }

        private void AjoutToken(int idCompte)
        {
            using (DataClasses1DataContext entity = new DataClasses1DataContext())
            {
                T_COMPTE t_compte = entity.T_COMPTE.Single(c => c.ID == idCompte);
                t_compte.Token = ServiceSecurite.GenererToken(t_compte.Identifiant, t_compte.MotDePass, DateTime.UtcNow.Ticks);
                entity.SubmitChanges();
            }
        }

        public int Autentification(string Identifiant, string MotDePasse)
        {
            using (DataClasses1DataContext entity = new DataClasses1DataContext())
            {
                foreach(T_COMPTE element in entity.T_COMPTE)
                {
                    if (element.Identifiant.ToLower() == Identifiant.ToLower() & element.MotDePass == MotDePasse)
                    {
                        AjoutToken(element.ID);
                        return element.ID;
                        //return new Compte() { ID = element.ID, Identifiant = element.Identifiant, MotDePass = element.MotDePass, Nom = element.Nom, Prenom = element.Prenom };
                    }
                }
                throw new AutentificationIncorrecteException(Identifiant, "Identifiant ou mot de passe incorrecte");
                //T_COMPTE retour = entity.T_COMPTE.Where(compte => compte.Identifiant == Identifiant & compte.MotDePass == MotDePasse).First();
                //T_COMPTE retour = (from elt in entity.T_COMPTE where (elt.Identifiant == Identifiant & elt.MotDePass == MotDePasse) select elt).First();
                //if (retour == null) throw new AutentificationIncorrecteException(Identifiant, MotDePasse, "Identifiant ou mot de passe incorrecte");
                //else return new Compte() { ID = retour.ID, Identifiant = retour.Identifiant, MotDePass = retour.MotDePass, Nom = retour.Nom, Prenom = retour.Prenom };
            }
        }
        public string GetToken(string Identifiant)
        {
            using (DataClasses1DataContext entity = new DataClasses1DataContext())
            {
                try
                {
                    return entity.T_COMPTE.Where(elm => elm.Identifiant == Identifiant).SingleOrDefault().Token;
                }
                catch(Exception)
                {
                    return "";
                }
            }
        }
        public string GetMotDePass(string Identifiant)
        {
            using (DataClasses1DataContext entity = new DataClasses1DataContext())
            {
                try
                {
                    return entity.T_COMPTE.Where(elm => elm.Identifiant.ToLower() == Identifiant.ToLower()).SingleOrDefault().MotDePass;
                }
                catch (Exception)
                {
                    return "";
                }
            }
        }
        //private string SiIdentiqueOuNull(string Actuel, string Nouveau)
        //{
        //    return (String.Compare(Actuel, Nouveau) == 0) ? null : Nouveau;
        //}
    }
}