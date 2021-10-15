using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Notice_board.Models
{
    public class Notice
    {
        public Notice() { }
        public Notice(string title, string writer, string content)
        {
            this.title = title;
            this.writer = writer;
            this.content = content;
        }
        public Notice(int id, string title, string writer, string content, string date)
        {
            this.id = id;
            this.title = title;
            this.writer = writer;
            this.content = content;
            this.date = date;
        }
        public int id { get; set; }
        public string writer { get; set; }
        public string title { get; set; }
        public string date { get; set; }
        public string content { get; set; }
    }
}
