using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aviasalesWebAPI.ModelAPI
{
    public class ResponseV2
    {
        public  string success { get; set; }
        public IEnumerable<ticketsV2> data { get; set; }
    }
}
