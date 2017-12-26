using MigraineCSMiddleware.DAO;
using MigraineCSMiddleware.Modele;
using MigraineCSMiddleware.Service.securite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Runtime.Serialization.Json;


namespace MigraineCSMiddleware.Service.facteur
{
    public class ServiceFacteur
    {
        public List<TypeFacteur> ListTypeFacteur(string Token)
        {
            ServiceSecurite.IsTokenValid(Token); //teste du token long
            return new FacteurDAO().ListTypeFacteur();
        }

        public List<TypeReponse> ListTypeReponse(string Token)
        {
            ServiceSecurite.IsTokenValid(Token); //teste du token long
            return new FacteurDAO().ListTypeReponse();
        }

        public List<Facteur> ListeFacteurTotal(string Token)
        {
            ServiceSecurite.IsTokenValid(Token); //teste du token long
            return new FacteurDAO().ListeFacteurs();
        }
        public List<Facteur> AjoutFacteur(string ValueJSON, string Token)
        {

            ServiceSecurite.IsTokenValid(Token); //teste du token long
            Facteur facteur = FacteurDepuisValeur(ValueJSON);

            return new FacteurDAO().AjouterFacteur(facteur);
        }

        public List<Facteur> ModificationFacteur(string ValueJSON, string Token)
        {
            ServiceSecurite.IsTokenValid(Token); //teste du token long
            Facteur facteur = FacteurDepuisValeur(ValueJSON);

            new FacteurDAO().ModifierFacteur(facteur);
            return new FacteurDAO().ListeFacteurs();
        }

        public List<Facteur> SuppressionFacteur(string ValueJSON, string Token)
        {
            ServiceSecurite.IsTokenValid(Token); //teste du token long
            Facteur facteur = FacteurDepuisValeur(ValueJSON);

            new FacteurDAO().SupprimerFacteur(facteur.ID);
            return new FacteurDAO().ListeFacteurs();
        }

        private Facteur FacteurDepuisValeur(string ValueJSON)
        {
            byte[] teste = Convert.FromBase64String(ValueJSON);
            string key = Encoding.UTF7.GetString(teste);
            //string key = Encoding.UTF8.GetString(Convert.FromBase64String(ValueJSON));
            using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(key)))
            {
                DataContractJsonSerializer deserializer = new DataContractJsonSerializer(typeof(Facteur));
                Facteur facteur = (Facteur)deserializer.ReadObject(ms);
                return facteur;
            }
        }
    }
}