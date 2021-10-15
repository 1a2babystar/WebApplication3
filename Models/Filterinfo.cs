using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Notice_board.Models
{
    public class Filterinfo
    {
        public string logic { get; set; }
        public List<Filter> filters { get; set; }
    }

    public class Filter
    {
        public string field { get; set; }
        public string @operator { get; set; }
        public string value { get; set; }
    }
}
