using MigraineCSMiddleware.Modele;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MigraineCSMiddleware.DAO
{
    public class AdresseDAO
    {
        //private List<Adresse> _ListAdresse = new List<Adresse>();
        //public List<Adresse> ListAdresse { get => _ListAdresse; set => _ListAdresse = value; }
        public AdresseDAO()
        {
            //Rafraichir();
        }

        //private void Rafraichir()
        //{
        //    using (DataClasses1DataContext entity = new DataClasses1DataContext())
        //    {
        //        var ret = entity.T_ADRESSE.ToList();
        //        _ListAdresse.Clear();
        //        foreach (var element in ret)
        //        {
        //            _ListAdresse.Add(new Adresse() { IdCompte = element.IdCompte, Numero = (int)element.Numero, NomRue = element.NomRue, CodePostal = (int)element.CodePostal, Ville = element.Ville });
        //        }
        //    }
        //}
        public Adresse AjoutAdresse(int IdCompte, Patient patient)
        {
            Ajout(IdCompte, patient.Adresse.Numero, patient.Adresse.NomRue, patient.Adresse.CodePostal, patient.Adresse.Ville);

            return LectureAdresse(IdCompte);

        }
        public Adresse AjoutAdresse(int IdCompte, Medecin medecin)
        {
            Ajout(IdCompte, medecin.Adresse.Numero, medecin.Adresse.NomRue, medecin.Adresse.CodePostal, medecin.Adresse.Ville);

            return LectureAdresse(IdCompte);
        }
        private void Ajout(int IdCompte, int numero, string nomrue, int codepostal, string ville)
        {
            using (DataClasses1DataContext entity = new DataClasses1DataContext())
            {
                T_ADRESSE RetourAdresse = entity.T_ADRESSE.FirstOrDefault(elt => elt.IdCompte == IdCompte);

                if (RetourAdresse == null)
                {
                    entity.AjoutAdresse(IdCompte, numero, nomrue, codepostal, ville);
                }
                else
                {
                    if (RetourAdresse.Numero != numero) RetourAdresse.Numero = numero;
                    if (RetourAdresse.NomRue != nomrue) RetourAdresse.NomRue = nomrue;
                    if (RetourAdresse.CodePostal != codepostal) RetourAdresse.CodePostal = codepostal;
                    if (RetourAdresse.Ville != ville) RetourAdresse.Ville = ville;
                    entity.SubmitChanges();
                }
            }
        }
        public Adresse LectureAdresse(int IdCompte)
        {
            using (DataClasses1DataContext entity = new DataClasses1DataContext())
            {
                T_ADRESSE RetourAdresse = entity.T_ADRESSE.FirstOrDefault(elt => elt.IdCompte == IdCompte);
                if (RetourAdresse == null) return null;
                return new Adresse() { IdCompte = RetourAdresse.IdCompte, Numero = (int)RetourAdresse.Numero, NomRue = RetourAdresse.NomRue, CodePostal = (int)RetourAdresse.CodePostal, Ville = RetourAdresse.Ville };
            }
        }
        //public Adresse LectureAdresse(Patient patient)
        //{
        //   return _ListAdresse.Where(Id => Id.IdCompte == patient.ID).First();
        //}
        //public Adresse LectureAdresse(Medecin medecin)
        //{
        //   Adresse retour = _ListAdresse.Where(Id => Id.IdCompte == medecin.ID).First();
        //    return retour;
        //}
    }
}