using MigraineCSMiddleware.Service.date;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MigraineCSMiddleware.Modele
{
    //public class Horaire
    //{
    //    private PlageHoraire[] _ListHoraire = new PlageHoraire[7];

    //    public PlageHoraire[] ListHoraire { get => _ListHoraire; set => _ListHoraire = value; }
    //}
    public class Horaire
    {
        private int _IDMedecin;
        private int _IdJour;
        private string _Matin;
        private string _Soir;
        private string _Jour;

        public int IDMedecin { get => _IDMedecin; set => _IDMedecin = value; }
        public string Jour { get { return _Jour; } set { _Jour = value; } }
        public int IdJour { get { return _IdJour; } set { _IdJour = value; _Jour = ConvertionDate.Semaine[_IdJour]; } }
        public string Matin { get => _Matin; set => _Matin = value; }
        public string Soir { get => _Soir; set => _Soir = value; }
        
    }
}