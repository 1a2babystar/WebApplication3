using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Notice_board.Models
{
    public class Pageinfo
    {
        public int take { get; set; }
        public int skip { get; set; }
        public int page { get; set; }
        public int pageSize { get; set; }

        public Sortinfo sort { get; set; }

        public Filterinfo filter { get; set; }
    }
}
