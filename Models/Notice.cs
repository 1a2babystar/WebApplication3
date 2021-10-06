using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Models
{
    public class Notice
    {
        public Notice()
        {
            this.title = null;
            this.writer = null;
            this.contents = null;
            this.date = null;
            this.id = -1;
        }
        public Notice(int id, string title, string writer, string contents, string date)
        {
            this.id = id;
            this.title = title;
            this.writer = writer;
            this.contents = contents;
            this.date = date;
        }
        public int id { get; set; }
        public string writer { get; set; }
        public string title { get; set; }
        public string date { get; set; }
        public string contents { get; set; }
    }
}
