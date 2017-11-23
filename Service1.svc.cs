using MigraineCSMiddleware.Service;
using System.Collections.Generic;
using MigraineCSMiddleware.Metier;
using MigraineCSMiddleware.DAO;
using MigraineCSMiddleware.Modele;
using MigraineCSMiddleware.Service.Medicament;
using MigraineCSMiddleware.Service.Utilisateur;
using System.Web;
using System.ServiceModel.Activation;
using System;
using MigraineCSMiddleware.Service.Securite;
using System.ServiceModel.Web;

namespace MigraineCSMiddleware
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "Service1" dans le code, le fichier svc et le fichier de configuration.
    // REMARQUE : pour lancer le client test WCF afin de tester ce service, sélectionnez Service1.svc ou Service1.svc.cs dans l'Explorateur de solutions et démarrez le débogage.
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class Service1 : IService1
    {
        #region Compte
        public UtilisateurWeb CompteLogin(string Value)
        {
            //var token = HttpContext.Current.Request.Headers["Authorisation"];
            //return new ServiceCompte().Login(Login, Pass);

            //var messageProperty = (HttpRequestMessageProperty)
            //OperationContext.Current.IncomingMessageProperties[HttpRequestMessageProperty.Name];
            //string cookie = messageProperty.Headers.Get("Set-Cookie");
            //string[] nameValue = cookie.Split('=', ',');
            //string userName = string.Empty;



            //HttpContext httpContext = HttpContext.Current;
            //NameValueCollection headerList = httpContext.Request.Headers;
            //var authorizationField = headerList.Get("Authorization");
            //byte[] test = Convert.FromBase64String(Pass);
            //var plainTextSecurityKey = "This is my shared, not so secret, secret!";
            //var signingKey = new InMemorySymmetricSecurityKey(Encoding.UTF8.GetBytes(plainTextSecurityKey));
            //var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256Signature, SecurityAlgorithms.Sha256Digest);

            //X509Certificate Certificat = new X509Certificate(Pass);

            //string key = Encoding.UTF8.GetString(Convert.FromBase64String(Token));


            try
            {
                return new ServiceUtilisateursWeb().Login(Value);
            }
            catch (AutentificationIncorrecteException Exp)
            {
                return new UtilisateurWeb() { Identifiant = Exp.Identifiant, Erreur = Exp.Message };
            }
            catch (TokenInvalidException Exp)
            {
                return new UtilisateurWeb() { Erreur = Exp.Message };
            }
            catch (TokenExpireException Exp)
            {
                //return new UtilisateurWeb() { Erreur = Exp.Message };
                throw new WebFaultException<string>("Votre Token a expiré", System.Net.HttpStatusCode.ServiceUnavailable);
            }
            catch (CompteException Exp)
            {
                return new UtilisateurWeb() { Erreur = Exp.Message };
            }

        }

        public UtilisateurWeb CompteAjout(string Value)
        {
            try
            {
                return new ServiceUtilisateursWeb().CreationCompte(Value);
            }
            catch (TokenInvalidException Exp)
            {
                return new UtilisateurWeb() { Erreur = Exp.Message };
            }
            catch (TokenExpireException Exp)
            {
                //return new UtilisateurWeb() { Erreur = Exp.Message };
                throw new WebFaultException<string>("Votre Token a expiré", System.Net.HttpStatusCode.ServiceUnavailable);
            }
            catch (CompteException Exp)
            {
                return new UtilisateurWeb() { Erreur = Exp.Message };
            }
        }
        public UtilisateurWeb ChangementInfoCompte(string Value)
        {
            try
            {
                return new ServiceUtilisateursWeb().ModificationCompte(Value);
            }
            catch (TokenInvalidException Exp)
            {
                return new UtilisateurWeb() { Erreur = Exp.Message };
            }
            catch (TokenExpireException Exp)
            {
                //return new UtilisateurWeb() { Erreur = Exp.Message };
                throw new WebFaultException<string>("Votre Token a expiré", System.Net.HttpStatusCode.ServiceUnavailable);
            }
            catch (CompteException Exp)
            {
                return new UtilisateurWeb() { Erreur = Exp.Message };
            }
            catch (CompteModificationException Exp)
            {
                return new UtilisateurWeb() { Erreur = Exp.Message };
            }
            
        }


        public UtilisateurWeb CompteLogin1(string Login, string Pass)
        {
            //var token = HttpContext.Current.Request.Headers["Authorisation"];
            //return new ServiceCompte().Login(Login, Pass);

            //var messageProperty = (HttpRequestMessageProperty)
            //OperationContext.Current.IncomingMessageProperties[HttpRequestMessageProperty.Name];
            //string cookie = messageProperty.Headers.Get("Set-Cookie");
            //string[] nameValue = cookie.Split('=', ',');
            //string userName = string.Empty;



            //HttpContext httpContext = HttpContext.Current;
            //NameValueCollection headerList = httpContext.Request.Headers;
            //var authorizationField = headerList.Get("Authorization");


            //X509Certificate Certificat = new X509Certificate(Pass);
            //return new ServiceUtilisateursWeb().Login("Noctem", "651acbf7f2eb57698c78f2d0e9ccc8b32a8d8e8d7868b3c41429f0f64019c6b8");
            return null;
        }

        public UtilisateurWeb UtilisateurLoginMok()
        {
            //return null;
            //return new ServiceUtilisateursWeb().Login("Noctem", "651acbf7f2eb57698c78f2d0e9ccc8b32a8d8e8d7868b3c41429f0f64019c6b8");
            //return CompteLogin("Noctem", "651acbf7f2eb57698c78f2d0e9ccc8b32a8d8e8d7868b3c41429f0f64019c6b8");
            return null;
        }
        public int AjoutCompte(string Identifiant, string motdepass, string nom, string prenom, string DateCreation, string DerniereModif, int CreePar, string Token)
        {
            return new ServiceCompte().AjoutCompte(new Compte() { Identifiant = Identifiant, MotDePass = motdepass, Nom = nom, Prenom = prenom, DateCreation = DateCreation, DernierModif = DerniereModif, CreePar = CreePar, Token = Token });
        }
        public List<Compte> GetListComptes()
        {
            return new ServiceCompte().GetListComptes();
        }
        public UtilisateurWeb ChangementMDPCompte(string Value)
        {
            try
            {
                return new ServiceUtilisateursWeb().ChangeMotDePass(Value);
            }
            catch (TokenInvalidException Exp)
            {
                return new UtilisateurWeb() { Erreur = Exp.Message };
            }
            catch (TokenExpireException Exp)
            {
                throw new WebFaultException<string>("Votre Token a expiré", System.Net.HttpStatusCode.ServiceUnavailable);
                //return new UtilisateurWeb() { Erreur = Exp.Message };
            }
            catch (CompteException Exp)
            {
                return new UtilisateurWeb() { Erreur = Exp.Message };
            }
            catch (CompteModificationException Exp)
            {
                return new UtilisateurWeb() { Erreur = Exp.Message };
            }
        }

        public UtilisateurWeb AjoutMedecinAPatient(string Value)
        {
            try
            {
                return new ServiceUtilisateursWeb().AttributionMedecin(Value);
            }
            catch (PatientIncorectException Exp)
            {
                return null;

            }
            catch (MedecinIncorectException Exp)
            {
                return null;

            }
            catch (DejaMedecinAttribueException Exp)
            {
                return new ServiceUtilisateursWeb().Conversion(Exp.Patient);
            }
            catch (TokenExpireException Exp)
            {
                throw new WebFaultException<string>("Votre Token a expiré", System.Net.HttpStatusCode.ServiceUnavailable);
            }
        }

        public UtilisateurWeb AjoutPatientAMedecin(string Value)
        {
            try
            {
                return new ServiceUtilisateursWeb().AttributionPatient(Value);
            }
            catch (PatientIncorectException Exp)
            {
                return null;
            }
            catch (MedecinIncorectException Exp)
            {
                return null;
            }
            catch (DejaMedecinAttribueException Exp)
            {
                return null;
            }
            catch (TokenExpireException Exp)
            {
                throw new WebFaultException<string>("Votre Token a expiré", System.Net.HttpStatusCode.ServiceUnavailable);
            }
        }


        #endregion

        #region Patient
        public Patient AjoutPatient(string Identifiant, string motdepass, string nom, string prenom, string DateNaissance)
        {
            //Patient patient = new Patient();
            return new ServicePatient().AjoutPatient(new Patient() { Identifiant = Identifiant, MotDePass = motdepass, Nom = nom, Prenom = prenom, DateNaissance = new DateTime(long.Parse(DateNaissance))});
        }
        public UtilisateurWeb GetPatient(string Value)
        {
            try
            {
                return new ServiceUtilisateursWeb().GetPatient(Value);
            }
            catch (TokenInvalidException Exp)
            {
                throw new WebFaultException<string>("Votre Token est incorrecte", System.Net.HttpStatusCode.ServiceUnavailable);
            }
            catch (TokenExpireException Exp)
            {
                throw new WebFaultException<string>("Votre Token a expiré", System.Net.HttpStatusCode.ServiceUnavailable);
            }

        }

        public List<Patient> GetListPatient()
        {
            return new ServicePatient().GetListPatient();
        }
        public List<UtilisateurWeb> GetListPatient(string Value)
        {
            try
            {
                return new ServiceUtilisateursWeb().GetListPatient(Value);
            }
            catch (TokenInvalidException Exp)
            {
                throw new WebFaultException<string>("Votre Token est incorrecte", System.Net.HttpStatusCode.ServiceUnavailable);
            }
            catch (TokenExpireException Exp)
            {
                throw new WebFaultException<string>("Votre Token a expiré", System.Net.HttpStatusCode.ServiceUnavailable);
            }

        }

        //public List<Patient> GetListMesPatient(string idMedecin)
        //{
        //    return new ServicePatient().GetListMesPatient(int.Parse(idMedecin));
        //}

        public Patient LoginPatient(string Login, string Pass)
        {
            //IncomingWebRequestContext context = WebOperationContext.Current.IncomingRequest;

            //string hdrVal = context.Headers["Authorization"]; //always empty.
            //NameValueCollection pa = context.Headers;
            //foreach (string key in pa.Keys)
            //{

            //}
            //bool test = Authenticate(context);

            return new ServicePatient().Login(Login, Pass);
        }


        //public bool LoginPatient1(string Login)
        //{
        //    return new ServicePatient().Login(Login, "");
        //}


        //public Patient ChangementMDPPatient(int ID, string MotDePass)
        //{
        //    return new ServicePatient().NouveauMotDePass(ID, MotDePass);
        //}
        //public Patient ChangementInfoPatient(int ID, string Identifiant, string Nom, string Prenom, string DateNaissance, int IdMedecin)
        //{
        //    try
        //    {
        //        return new ServicePatient().ModificationPatient(ID, Identifiant, Nom, Prenom, DateNaissance, IdMedecin);

        //    }
        //    catch (LoginTropLongException exp)
        //    {
        //        return new Patient() { IDPatient = ID, ID = exp.IdCompte, Identifiant = Identifiant, MotDePass = "", Nom = Nom, Prenom = Prenom, DateNaissance = new DateTime(long.Parse(DateNaissance)), MesMedecin = null, Erreur = exp.Message };
        //        //new Compte() { ID = (int)element.ID, Login = element.Login, MotDePass = null, Nom = element.Nom, Prenom = element.Prenom });
        //    }
        //    catch (NomTropLongException exp)
        //    {
        //        return new Patient() { IDPatient = ID, ID = exp.IdCompte, Identifiant = Identifiant, MotDePass = "", Nom = Nom, Prenom = Prenom, DateNaissance = new DateTime(long.Parse(DateNaissance)), MesMedecin = null, Erreur = exp.Message };
        //    }
        //    catch (PrenomTropLongException exp)
        //    {
        //        return new Patient() { IDPatient = ID, ID = exp.IdCompte, Identifiant = Identifiant, MotDePass = "", Nom = Nom, Prenom = Prenom, DateNaissance = new DateTime(long.Parse(DateNaissance)), MesMedecin = null, Erreur = exp.Message };
        //    }
        //}

        public List<Patient> SupprimerMonPatient(int IdMedecin, int IdPatient)
        {
            try
            {
                return new ServicePatient().SupprimerMedecin(IdPatient, IdMedecin);
            }
            catch (PatientIncorectException exp)
            {
                List<Patient> retour = new List<Patient>();
                retour.Add(new Patient() { IDPatient = IdPatient, Erreur = exp.Message });
                return retour;
            }
            catch (MedecinMalAttribueAuPatientException exp)
            {
                List<Patient> retour = new List<Patient>();
                retour.Add(new Patient() { IDPatient = IdPatient, Erreur = exp.Message });
                return retour;
            }
            catch (MedecinNonAttribueAuPatientException exp)
            {
                List<Patient> retour = new List<Patient>();
                retour.Add(new Patient() { IDPatient = IdPatient, Erreur = exp.Message });
                return retour;
            }
        }
        #endregion

        #region Medecin

        public UtilisateurWeb GetMedecin(string Value)
        {
            try
            {
                return new ServiceUtilisateursWeb().GetMedecin(Value);
            }
            catch (TokenInvalidException Exp)
            {
                throw new WebFaultException<string>("Votre Token est incorrecte", System.Net.HttpStatusCode.ServiceUnavailable);
            }
            catch (TokenExpireException Exp)
            {
                throw new WebFaultException<string>("Votre Token a expiré", System.Net.HttpStatusCode.ServiceUnavailable);
            }
        }

        //public List<UtilisateurWeb> GetListMedecin()
        //{
        //    try
        //    {
        //        return new ServiceUtilisateursWeb().GetListMedecin();
        //    }
        //    catch (MedecinIntrouvableException Exp)
        //    {
        //        return null;
        //    }
        //    catch (TokenExpireException Exp)
        //    {
        //        return null;
        //    }
        //}

        public List<UtilisateurWeb> GetListMedecin(string Value)
        {
            try
            {
                return new ServiceUtilisateursWeb().GetListMedecin(Value);
            }
            catch (MedecinIntrouvableException Exp)
            {
                return null;
            }
            catch (TokenInvalidException Exp)
            {
                throw new WebFaultException<string>("Votre Token est incorrecte", System.Net.HttpStatusCode.ServiceUnavailable);
            }
            catch (TokenExpireException Exp)
            {
                throw new WebFaultException<string>("Votre Token a expiré", System.Net.HttpStatusCode.ServiceUnavailable);
            }
        }

        public Medecin LoginMedecin(string Login, string Pass)
        {
            return new ServiceMedecin().Login(Login, Pass);
        }
        public Medecin AjoutMedecin(string Identifiant, string motdepass, string nom, string prenom, string AdresseCabinet, string TelephoneCabinet, string infocomplementair)
        {
            return new ServiceMedecin().AjoutMedecin(new Medecin() { Identifiant = Identifiant, MotDePass = motdepass, Nom = nom, Prenom = prenom, Telephone = TelephoneCabinet, InfoComplementaire = infocomplementair });
        }


        //public Patient AjoutMedecinAPatient(int IdPatient, int IdMedecin)
        //{
        //    try
        //    {
        //        return new ServicePatient().AttributionMedecin(IdPatient, IdMedecin);
        //    }
        //    catch (PatientIncorectException Exp)
        //    {
        //        return null;

        //    }
        //    catch (MedecinIncorectException Exp)
        //    {
        //        return null;

        //    }
        //    catch (DejaMedecinAttribueException Exp)
        //    {
        //        return null;
        //    }
        //}
        //public Patient SupprimerMedecinDuPatient(int IdPatient)
        //{
        //    return new ServicePatient().ModificationPatient(IdPatient, null, null, null, null, 0);
        //}
        //public Medecin ChangementMDPMedecin(int ID, string MotDePass)
        //{
        //    try
        //    {
        //        return new ServiceMedecin().NouveauMotDePass(ID, MotDePass);
        //    }
        //    catch (MDPNullException exp)
        //    {
        //        return new Medecin() { ID = ID, MotDePass = MotDePass, Erreur = exp.Message };
        //    }

        //}

        #endregion

        #region Medicament
        //public Medicament AjoutMedicament(string nom, string idType)
        //{
        //    return new ServiceMedicament().AjoutMedicament(nom, idType);
        //}

        //public Modele.Type AjoutTypeMedicament(string nom)
        //{
        //    return new ServiceType().AjoutTypeMedicament(nom);
        //}

        //public List<Modele.Type> GetListTypeMedicament()
        //{
        //    return new ServiceType().GetListType();
        //}

        #endregion

        public string hello()
        {
            return "ça marche !!!!! ";
        }
        public string testSimple(string Test)
        {
            return Test;
        }
        public string test(string Test)
        {
            return "coucou";
        }

        public TEST GetTestJSon()
        {
            return new TEST() { Nom = "Verne" };
        }

        public TEST GetTestJSon1(string Index)
        {
            return new TEST() { Nom = Index };
        }


        public string Post()
        {
            return "Retour Poste";
        }

        public string get()
        {
            var token = HttpContext.Current.Request.Headers["Authorisation"];
            return "information";
        }


    }

}
