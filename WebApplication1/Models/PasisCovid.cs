using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class PasisCovid
    {
        public string location { get; set; }
        public string codIso { get; set; }
        public int confirmed { get; set; }
        public int deaths { get; set; }
        public int recovered { get; set; }
        public int active { get; set; }
        public DateTime dt { get; set; }

    }
}
