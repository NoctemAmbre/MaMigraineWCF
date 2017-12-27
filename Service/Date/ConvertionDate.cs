﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace MigraineCSMiddleware.Service.date
{
    public class ConvertionDate
    {
        public static DateTime ConvertionStringVersDateTime(string valeur)
        {
            if (valeur == null) return new DateTime();

            string heureString;
            string dateString;
            DateTime Retour = new DateTime();

            if (valeur.Contains("T"))
            {
                dateString  = Regex.Split(valeur, "T")[0];
                heureString = Regex.Split(valeur, "T")[1];
                if (dateString.Contains("-"))
                {
                    string[] date = Regex.Split(dateString, "-");
                    if (date.Length == 3) Retour = new DateTime(int.Parse(date[0]), int.Parse(date[1]), int.Parse(date[2]));
                }
                if (heureString.Contains(":"))
                {
                    string[] heure = Regex.Split(heureString, ":");
                    if (heure.Length == 2) Retour = Retour.AddHours(double.Parse(heure[0])).AddMinutes(double.Parse(heure[1]));
                }
                return Retour;
            }
            else dateString = valeur;
            if (dateString.Contains("-"))
            {
                string[] date = Regex.Split(dateString, "-");
                if (date.Length == 3) Retour = new DateTime(int.Parse(date[0]), int.Parse(date[1]), int.Parse(date[2]));
            }
            return new DateTime();
        }

        public static int DureeEntreDateTime(string debut, string fin)
        {
            DateTime DTDebut = ConvertionStringVersDateTime(debut);
            DateTime DTFin = ConvertionStringVersDateTime(fin);
            if (fin.CompareTo(debut) > 0)
            {
                TimeSpan valeur = DTFin.Subtract(DTDebut);
                return (int)valeur.TotalHours;
            }
            else return -1;
        }

        public static string ConvertionDateTimeVersString(DateTime valeur)
        {
            return valeur.Year + "-" + valeur.Month + "-" + valeur.Day;
        }

        public static string RechercheMoi(string debut)
        {
            DateTime DTDebut = ConvertionStringVersDateTime(debut);
            return Moi[DTDebut.Month].ToString() + "-" + DTDebut.Year.ToString();
        }
        public static string[] Semaine = { "Lundi", "Mardi", "Mercredi", "Jeudi", "Vendredi", "Samedi", "Dimanche" };
        public static string[] Moi = { "Janvier", "Fevrier", "Mars", "Avril", "Mai", "Juin", "Juillet", "Aout", "Septembre", "Octobre", "Novembre", "Décembre" };
    }
}