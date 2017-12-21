using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MigraineCSMiddleware.Modele
{
    public class Facteur
    {
        public int ID { get; set; }
        public string Nom { get; set; }
        public string Question { get; set; }
        public int Reponse { get; set; }
        public TypeFacteur TypeDeFacteur { get; set; }
        public TypeReponse TypeDeReponse { get; set; }
    }
    public class TypeFacteur
    {
        public int ID { get; set; }
        public string Type { get; set; }
    }
    public class TypeReponse
    {
        public int ID { get; set; }
        public string Type { get; set; }
        public string Information { get; set; }
    }
    
}