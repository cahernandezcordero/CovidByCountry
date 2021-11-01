using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Data
    {
        public string location { get; set; }
        public int confirmed { get; set; }
        public int deaths { get; set; }
        public int recovered { get; set; }
        public int active { get; set; }

    }
    public class Application
    {
        public Data data { get; set; }
        public string dt { get; set; }
        public int ts { get; set; }

    }
}
