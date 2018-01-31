using MigraineCSMiddleware.Modele;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MigraineCSMiddleware.DAO
{
    public class HoraireDAO
    {
        //private List<Horaire> _ListHoraire = new List<Horaire>();
        //public List<Horaire> ListHoraire { get => _ListHoraire; set => _ListHoraire = value; }
        public HoraireDAO()
        {
            //Rafraichir();
        }
        //private void Rafraichir()
        //{
        //    using (DataClasses1DataContext entity = new DataClasses1DataContext())
        //    {
        //        var ret = entity.T_HORAIRE.Join(entity.T_PLAGEHORAIRE,
        //            H => H.IDPlageHoraire,
        //            PH => PH.ID,
        //            (H, PH) => new { IDMedecin = H.IDMedecin, IdJour = H.IDJour, Matin = PH.HeureDebut, Soir = PH.HeureFin });
        //        _ListHoraire.Clear();
        //       foreach (var element in ret)
        //       {
        //            _ListHoraire.Add(new Horaire() { IDMedecin = element.IDMedecin, IdJour = element.IdJour, Matin = element.Matin, Soir = element.Soir });
        //       }
        //    }
        //}
        /// <summary>
        /// récupération de la liste des Horaire
        /// </summary>
        /// <param name="IdMedecin"></param>
        /// <param name="ListHoraire"></param>
        /// <returns></returns>
        public Horaire[] AjoutHoraire(int IdMedecin, Horaire[] ListAjoutHoraires)
        {
            if (SiHoraireIdentique(IdMedecin, ListAjoutHoraires)) return ListAjoutHoraires;

            if (IdMedecin != -1)
            {
                using (DataClasses1DataContext entity = new DataClasses1DataContext())
                {
                    for (int IdJour = 0; IdJour < 7; IdJour++)
                    {
                        entity.AjoutHoraireOuvertureMedecin(IdMedecin, IdJour, ListAjoutHoraires[IdJour].Matin, ListAjoutHoraires[IdJour].Soir);
                    }
                    //Rafraichir();
                    return LectureHoraire(IdMedecin);
                }
            }
            return null;
        }

        /// <summary>
        /// Teste de similarité de la liste des horaires. Si elles sont identique on retour vrais
        /// </summary>
        /// <param name="IdMedecin"></param>
        /// <param name="ListAjoutHoraires"></param>
        /// <returns></returns>
        private bool SiHoraireIdentique(int IdMedecin, Horaire[] ListAjoutHoraires)
        {
            Horaire[] HorairePresent = LectureHoraire(IdMedecin);
            if (HorairePresent.Length == 7)
            {
                for (int i = 0; i < ListAjoutHoraires.Length; i++)
                {
                    Horaire UnHorairePresent = HorairePresent[i];
                    Horaire UnHoraireAchanger = ListAjoutHoraires[i];
                    if (UnHorairePresent.Matin != UnHoraireAchanger.Matin) return false;
                    if (UnHorairePresent.Soir != UnHoraireAchanger.Soir) return false;
                }
            }
            else return false;
            return true;
        }

        public Horaire[] LectureHoraire(int IdMedecin)
        {

            using (DataClasses1DataContext entity = new DataClasses1DataContext())
            {
                    var ret = entity.T_HORAIRE.Join(entity.T_PLAGEHORAIRE,
                    H => H.IDPlageHoraire,
                    PH => PH.ID,
                    (H, PH) => new { IDMedecin = H.IDMedecin, IdJour = H.IDJour, Matin = PH.HeureDebut, Soir = PH.HeureFin })
                    .Where(Id => Id.IDMedecin == IdMedecin).OrderBy(x => x.IdJour).ToArray();

                Horaire[] retour = new Horaire[ret.Length];
                for (int i = 0; i < ret.Length; i++)
                {
                    retour[i] = new Horaire() { IDMedecin = ret[i].IDMedecin, IdJour = ret[i].IdJour, Matin = ret[i].Matin, Soir = ret[i].Soir };
                }
                return retour;
            }
        }
    }
}