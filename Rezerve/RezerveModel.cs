using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rezerve
{
    public class RezerveModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string day { get; set; }
        public string hour { get; set; }
        public string doctor { get; set; }
        public int id_user { get; set; }
        public string rezerve_type { get; set; }
    }

}
