using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MigraineCSMiddleware.Modele;
using MigraineCSMiddleware.DAO;

namespace MigraineCSMiddleware.Service.medicament
{
    public class ServiceMedicament
    {
        public ServiceMedicament()
        {

        }
        
        public List<MigraineCSMiddleware.Modele.Medicament> ListeTotalMedicaments()
        {
            return new MedicamentDAO().ListeMedicements();
        }

        public List<MigraineCSMiddleware.Modele.Medicament> ListeMedicaments(string Nom)
        {
            return new MedicamentDAO().ChercheMedicament(Nom);
        }
        //public Modele.Medicament AjoutMedicament(string nom, string idType)
        //{
        //    if (new MedicamentTypeDAO().GetType(idType) == null) new TypeMedicamentInconnuException("Ce type de médicament n'existe pas encore");
        //    return new MedicamentDAO().(nom, idType);
        //}

    }
}