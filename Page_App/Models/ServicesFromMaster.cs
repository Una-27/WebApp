using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Page_App.Models
{
    public class ServicesFromMaster : SerializableObject
    {
        public int Id { get; set; }
        public Masters IdMaster { get; set; }
        public Services IdService { get; set; }
        public double TimeOfWork { get; set; }
        public int Price { get; set; }
    }
}
