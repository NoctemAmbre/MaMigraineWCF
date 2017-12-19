using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.ServiceModel.Syndication;
using MigraineCSMiddleware.Metier;
using MigraineCSMiddleware.Modele;
using System.ServiceModel.Activation;
using System.Security.Policy;
using System.Web;
using MigraineCSMiddleware.Service.utilisateurweb;

namespace MigraineCSMiddleware
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom d'interface "IService1" à la fois dans le code et le fichier de configuration.
   
    [ServiceContract]
    
    public interface IService1
    {
        //[OperationContract]
        //[WebInvoke(Method = "OPTIONS", UriTemplate = "/testSimple")]
        //void Getoptions();

        [WebInvoke(UriTemplate = "/testSimple", Method = "POST",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Wrapped)]
        [OperationContract]
        string testSimple(int id, string name, string description);


        #region Test

        //[WebInvoke(UriTemplate = "/testSimple", Method = "POST",
        //RequestFormat = WebMessageFormat.Json,
        //ResponseFormat = WebMessageFormat.Json,
        //BodyStyle = WebMessageBodyStyle.Wrapped)]
        //[OperationContract]
        //Product testSimple(string name, string description);

        //[WebInvoke(UriTemplate = "/testSimple", Method = "POST",
        //RequestFormat = WebMessageFormat.Json,
        //ResponseFormat = WebMessageFormat.Json,
        //BodyStyle = WebMessageBodyStyle.Wrapped)]
        //[OperationContract]
        //string testSimple(int id, string name, string description);


        [WebGet(UriTemplate = "/TestJson",
            ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        TEST GetTestJSon();


        [WebGet(UriTemplate = "/hello")]
        [OperationContract]
        string hello();



        [WebInvoke(UriTemplate = "/test?Index={Index}", Method = "POST",
        //[WebInvoke(UriTemplate = "/test", Method = "POST",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare)]
        [OperationContract]
        TEST GetTestJSon1(string Index);
        //string test(string test);

        [WebInvoke(UriTemplate = "/Post", Method = "POST",
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare)]
        [OperationContract]
        string Post();

        [WebGet(UriTemplate = "/get",
        ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        string get();

        #endregion

        #region Compte
        [WebGet(UriTemplate = "/Compte/Liste",
            ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        List<Compte> GetListComptes();

        [WebInvoke(UriTemplate = "/Compte/login?Value={Value}", Method = "POST",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare)]
        [OperationContract]
        UtilisateurWeb CompteLogin(string Value);      

        [WebInvoke(UriTemplate = "/Compte/Ajout?Value={Value}", Method = "POST",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare)]
        [OperationContract]
        UtilisateurWeb CompteAjout(string Value);

        [WebInvoke(UriTemplate = "/Compte/ChangeInformation?Value={Value}&Token={Token}", Method = "POST",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare)]
        [OperationContract]
        UtilisateurWeb ChangementInfoCompte(string Value, string Token);

        //[WebInvoke(UriTemplate = "/Compte/ChangeMDP", Method = "POST",
        //RequestFormat = WebMessageFormat.Json,
        //ResponseFormat = WebMessageFormat.Json,
        //BodyStyle = WebMessageBodyStyle.Wrapped)]
        //[OperationContract]
        //UtilisateurWeb ChangementMDPCompte(int ID, string MotDePass);

        [WebInvoke(UriTemplate = "/Compte/ChangeMDP?Value={Value}&Token={Token}", Method = "POST",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare)]
        [OperationContract]
        UtilisateurWeb ChangementMDPCompte(string Value, string Token);

        [WebInvoke(UriTemplate = "/Compte/AjoutMedecin?Value={Value}&Token={Token}", Method = "POST",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare)]
        [OperationContract]
        UtilisateurWeb AjoutMedecinAPatient(string Value, string Token);

        [WebInvoke(UriTemplate = "/Compte/AjoutPatient?Value={Value}&Token={Token}", Method = "POST",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare)]
        [OperationContract]
        UtilisateurWeb AjoutPatientAMedecin(string Value, string Token);

        [WebInvoke(UriTemplate = "/Compte/SupprPatient?Value={Value}&Token={Token}", Method = "POST",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare)]
        [OperationContract]
        UtilisateurWeb SupprPatientAMedecin(string Value, string Token);

        [WebInvoke(UriTemplate = "/Compte/SupprMedecin?Value={Value}&Token={Token}", Method = "POST",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare)]
        [OperationContract]
        UtilisateurWeb SupprMedecinAPatient(string Value, string Token);

        //    [WebInvoke(UriTemplate = "/Compte/login?Login={Login}&Pass={Pass}", Method = "POST",
        //    RequestFormat = WebMessageFormat.Json,
        //    ResponseFormat = WebMessageFormat.Json,
        //    BodyStyle = WebMessageBodyStyle.Bare)]
        //    [OperationContract]
        //    UtilisateurWeb CompteLogin(string Login, string Pass);

        //[WebInvoke(UriTemplate = "/Compte/login1?Login={Login}&Pass={Pass}", Method = "POST",
        //RequestFormat = WebMessageFormat.Json,
        //ResponseFormat = WebMessageFormat.Json,
        //BodyStyle = WebMessageBodyStyle.Bare)]
        //[OperationContract]
        //UtilisateurWeb CompteLogin1(string Login, string Pass);

        //[WebGet(UriTemplate = "/Utilisateur/loginMok",        
        //ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //UtilisateurWeb UtilisateurLoginMok();

        //[WebInvoke(UriTemplate = "/Compte/Ajout", Method = "POST",
        //    RequestFormat = WebMessageFormat.Json,
        //    ResponseFormat = WebMessageFormat.Json,
        //    BodyStyle = WebMessageBodyStyle.Wrapped)]
        //[OperationContract]
        //int AjoutCompte(string Login, string MotDePass, string Nom, string Prenom, string DateCreation, string DerniereModif, int CreePar, string Token);
        #endregion

        #region Medecin

        [WebGet(UriTemplate = "/Medecin/Voir?Value={Value}&Token={Token}",
        ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        UtilisateurWeb GetMedecin(string Value, string Token);
            

        //[WebInvoke(UriTemplate = "/Medecin/login", Method = "POST",
        //RequestFormat = WebMessageFormat.Json,
        //ResponseFormat = WebMessageFormat.Json,
        //BodyStyle = WebMessageBodyStyle.Wrapped)]
        //[OperationContract]
        //Medecin LoginMedecin(string Login, string Pass);

        //[WebGet(UriTemplate = "/Medecin/Liste",
        //ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //List<UtilisateurWeb> GetListMedecin();

        [WebGet(UriTemplate = "/Medecin/Liste?Value={Value}&Token={Token}",
        ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        List<UtilisateurWeb> GetListMedecin(string Value, string Token);
        /*
        [WebInvoke(UriTemplate = "/Medecin/Ajout", Method = "POST",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Wrapped)]
        [OperationContract]
        Medecin AjoutMedecin(string Login, string MotDePass, string Nom, string Prenom, string Adress, string Telephone, string InfoComplementair);

        [WebInvoke(UriTemplate = "/Medecin/AjoutPatient", Method = "POST",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Wrapped)]
        [OperationContract]
        Patient AjoutPatientAMedecin(int IdPatient, int IdMedecin);

        [WebInvoke(UriTemplate = "/Medecin/ChangeMDP", Method = "POST",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Wrapped)]
        [OperationContract]
        Medecin ChangementMDPMedecin(int ID, string MotDePass);

        [WebInvoke(UriTemplate = "/Medecin/ChangeInformation", Method = "POST",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Wrapped)]
        [OperationContract]
        Medecin ChangementInfoMedecin(int ID, string Login, string Nom, string Prenom, string Adress, string Telephone, string InfoComplementaire);

        [WebGet(UriTemplate = "/Medecin/{idMedecin}/ListPatient",
        ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        List<Patient> GetListMesPatient(string idMedecin);

        */
        #endregion

        #region Patient

        [WebGet(UriTemplate = "/Patient/Voir?Value={Value}&Token={Token}",
        ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        UtilisateurWeb GetPatient(string Value, string Token);

        [WebGet(UriTemplate = "/Patient/Liste?Value={Value}&Token={Token}",
        ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        List<UtilisateurWeb> GetListPatient(string Value, string Token);

        [WebInvoke(UriTemplate = "/Patient/AjoutMedicament?Value={Value}&Token={Token}", Method = "POST",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare)]
        [OperationContract]
        UtilisateurWeb PatientAjoutMedicament(string Value, string Token);

        [WebInvoke(UriTemplate = "/Patient/SupprMedicament?Value={Value}&Token={Token}", Method = "POST",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare)]
        [OperationContract]
        UtilisateurWeb PatientSupprMedicament(string Value, string Token);

        [WebGet(UriTemplate = "Patient/ListMigraine?Value={Value}&Token={Token}",
        ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        List<Migraine> GetListMigraineDuPatient(string Value, string Token);

        [WebGet(UriTemplate = "Patient/ListMedicament?Value={Value}&Token={Token}",
        ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        List<Medicament> GetListMedicamentDuPatient(string Value, string Token);

        [WebGet(UriTemplate = "Patient/ListFacteur?Value={Value}&Token={Token}",
        ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        List<Facteur> GetListFacteurDuPatient(string Value, string Token);


        //[WebInvoke(UriTemplate = "/Patient/login?username={Login}&password={Pass}", Method = "POST",
        //    RequestFormat = WebMessageFormat.Json,
        //    ResponseFormat = WebMessageFormat.Json,
        //    BodyStyle = WebMessageBodyStyle.Bare
        //    )]
        //[OperationContract]
        //bool LoginPatient(string Login, string Pass);

        //[WebInvoke(UriTemplate = "/Patient/log?username={Login}", Method = "POST",
        //RequestFormat = WebMessageFormat.Json,
        //ResponseFormat = WebMessageFormat.Json,
        //BodyStyle = WebMessageBodyStyle.Bare
        //)]
        //[OperationContract]
        //bool LoginPatient1(string Login);



        /*
        [WebInvoke(UriTemplate = "/Patient/login", Method = "POST",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Wrapped)]
        [OperationContract]
        Patient LoginPatient(string Login, string Pass);

        [WebGet(UriTemplate = "/Patient/Liste",
        ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        List<Patient> GetListPatient();

        [WebInvoke(UriTemplate = "/Patient/Ajout", Method = "POST",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Wrapped)]
        [OperationContract]
        Patient AjoutPatient(string Identifiant, string MotDePass, string Nom, string Prenom, string Token, string DateNaissance, string TelephonePortable, string TelephoneFixe, bool Sexe, string Adresse);

        [WebInvoke(UriTemplate = "/Patient/AjoutMedecin", Method = "POST",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Wrapped)]
        [OperationContract]
        Patient AjoutMedecinAPatient(int IdPatient, int IdMedecin);

        [WebInvoke(UriTemplate = "/Patient/SupprimerMedecin", Method = "POST",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Wrapped)]
        [OperationContract]
        Patient SupprimerMedecinDuPatient(int IdPatient);

        [WebInvoke(UriTemplate = "/Patient/ChangeMDP", Method = "POST",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Wrapped)]
        [OperationContract]
        Patient ChangementMDPPatient(int ID, string MotDePass);

        [WebInvoke(UriTemplate = "/Patient/ChangeInformation", Method = "POST",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Wrapped)]
        [OperationContract]
        Patient ChangementInfoPatient(int ID, string Login, string Nom, string Prenom, string DateNaissance, int IdMedecin);
    */
        #endregion

        #region Medicament

        [WebGet(UriTemplate = "/Medicament/ListeTotal?Value={Value}&Token={Token}",
        ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        List<Medicament> GetListTotalMedicaments(string Value, string Token);

        [WebGet(UriTemplate = "/Medicament/Liste?Value={Value}&Token={Token}",
        ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        List<Medicament> GetListMedicaments(string Value, string Token);

        //[WebInvoke(UriTemplate = "/Medicament/Ajout", Method = "POST",
        //    RequestFormat = WebMessageFormat.Json,
        //    ResponseFormat = WebMessageFormat.Json,
        //    BodyStyle = WebMessageBodyStyle.Wrapped)]
        //[OperationContract]
        //Medicament AjoutMedicament(string nom, string idType);


        //[WebInvoke(UriTemplate = "/Medicament/Type/Ajout", Method = "POST",
        //RequestFormat = WebMessageFormat.Json,
        //ResponseFormat = WebMessageFormat.Json,
        //BodyStyle = WebMessageBodyStyle.Wrapped)]
        //[OperationContract]
        //Modele.Type AjoutTypeMedicament(string nom);

        //[WebGet(UriTemplate = "/Medicament/Type/Liste",
        //ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //List<Modele.Type> GetListTypeMedicament();

        #endregion
    }

    // Utilisez un contrat de données comme indiqué dans l'exemple ci-après pour ajouter les types composites aux opérations de service.
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }

        [DataMember(Name = "b_login")]
        public string Login
        {
            get { return Login; }
            set { Login = value; }
        }
        [DataMember(Name = "b_pass")]
        public string Password
        {
            get { return Password; }
            set { Password = value; }
        }
    }
}
