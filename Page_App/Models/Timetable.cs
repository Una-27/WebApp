using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Page_App.Models
{
    public class Timetable : SerializableObject
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public double TimeStart { get; set; }
        public double TimeEnd { get; set; }
        public Masters IdMaster { get; set; }
        public Clients IdClient { get; set; }
        public Services IdService { get; set; }
        public int Price { get; set; }
        public string Message { get; set; }
        public int Status { get; set; }
        public int Grade { get; set; }
    }
}
