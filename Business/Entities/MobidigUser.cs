using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Entities
{
    public class MobidigUser
    {
        public string id { get; set; }
        public string username { get; set; }
        public List<string> rolename { get; set; }
    }
}
