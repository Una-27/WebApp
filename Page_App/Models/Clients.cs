using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Page_App.Models
{
    public class Clients : SerializableObject
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string AdvertisingChannel { get; set; }
        public float Discount { get; set; }
        public DateTime DateAdded { get; set; }
    }
}