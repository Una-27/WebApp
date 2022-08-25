using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Page_App.Models
{
    public class Finance : SerializableObject
    {
        public int Id { get; set; }
        public Masters IdMaster { get; set; }
        public string Category { get; set; }
        public bool Type { get; set; }
        public int Summa { get; set; }
        public string Info { get; set; }
        public DateTime Date { get; set; }
    }
}
