using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Models
{
    public class Notice
    {
        public Notice(string title, string writer, string contents, string date)
        {
            this.title = title;
            this.writer = writer;
            this.contents = contents;
            this.date = date;
        }
        public string writer { get; set; }
        public string title { get; set; }
        public string date { get; set; }
        public string contents { get; set; }
    }
}
