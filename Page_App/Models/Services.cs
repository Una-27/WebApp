using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Page_App.Models
{
    public class Services : SerializableObject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Price { get; set; }
        public string TimeOfWork { get; set; }
        public string Info { get; set; }
        public byte[] ImageData { get; set; }

    }
}