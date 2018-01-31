using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MigraineCSMiddleware.Modele
{
    public class Migraine
    {
        public int ID { get; set; }
        public int Intensite { get; set; }
        public string Debut { get; set; }
        public string Fin { get; set; }
        public DateTime DateTimeDebut { get; set; }
        public DateTime DateTimeFin { get; set; }
        public DateStructure DateDebut { get; set; }
        public DateStructure DateFin { get; set; }
        public TimeStructure HeureDebut { get; set; }
        public TimeStructure HeureFin { get; set; }
        public string DebutPresentation { get; set; }
        public string FinPresentation { get; set; }
        public int Duree { get; set; }
        public string Moi { get; set; }
        public bool Complet { get; set; }

        public List<Medicament> MedicamentsPris { get; set; }
        public List<Facteur> Facteurs { get; set; }
    }
    public class DateStructure
    {
        public int year { get; set; }
        public int month { get; set; }
        public int day { get; set; }
    }
    public class TimeStructure
    {
        public int hour { get; set; }
        public int minute { get; set; }
        public int second { get; set; }
    }
}