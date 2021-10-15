using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Notice_board.Models
{
    public class Sortinfo
    {
        public Sortinfo() { }
        public Sortinfo(string field, string dir)
        {
            this.field = field;
            this.dir = dir;
        }
        public string field { get; set; }
        public string dir { get; set; }
    }
}
