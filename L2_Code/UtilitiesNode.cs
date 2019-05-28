using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L2_Code
{
    public sealed class UtilitiesNode
    {
        public Utilities UtilitiesData { get; set; }
        public UtilitiesNode NextObject { get; set; }
        
        public UtilitiesNode(Utilities utilitiesValue, UtilitiesNode nextObjectAddress)
        {
            UtilitiesData = utilitiesValue;
            NextObject = nextObjectAddress;
        }
    }
}
