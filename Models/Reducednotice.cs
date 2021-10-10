using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Models
{
    public class Reducednotice
    {
        public Reducednotice(string title, string date, int id)
        {
            this.title = title;
            this.date = date;
            this.id = id;
        }
        public string title { get; set; }
        public string date { get; set; }
        public int id { get; set; }
    }
}