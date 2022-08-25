using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Page_App.Models
{
    public class Masters : SerializableObject
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Profession { get; set; }
        public string Salary { get; set; }
        public float Rate { get; set; }
        public byte[] ImageData { get; set; }
        public string Info { get; set; }
    }
}