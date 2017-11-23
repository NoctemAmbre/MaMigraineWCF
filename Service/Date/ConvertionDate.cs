using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace MigraineCSMiddleware.Service.Date
{
    public class ConvertionDate
    {
        public static DateTime ConvertionStringVersDateTime(string valeur)
        {
            if (valeur == null) return new DateTime();
            string[] Table;
            if (valeur.Contains("-"))
            {
                Table = Regex.Split(valeur, "-");
                if (Table.Length == 3) return new DateTime(int.Parse(Table[0]), int.Parse(Table[1]), int.Parse(Table[2]));
            }
            return new DateTime();

        }

        public static string ConvertionDateTimeVersString(DateTime valeur)
        {
            return valeur.Year + "-" + valeur.Month + "-" + valeur.Day;
        }
        public static string[] Semaine = { "Lundi", "Mardi", "Mercredi", "Jeudi", "Vendredi", "Samedi", "Dimanche" };
    }
}