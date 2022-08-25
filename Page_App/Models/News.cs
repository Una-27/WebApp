using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Page_App.Models
{
    public class News : SerializableObject
    {
        public int Id { get; set; }
        public Masters IdMaster { get; set; }
        public string Info { get; set; }
        public DateTime DateofEnd { get; set; }
        public byte[] ImageData { get; set; }
    }
}
