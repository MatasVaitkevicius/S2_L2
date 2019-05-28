using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L2_Code
{
    public sealed class ResidentsNode
    {
        public Residents ResidentsData { get; set; }
        public ResidentsNode NextObject { get; set; }

        public ResidentsNode(Residents residentsValue, ResidentsNode nextObjectAddress)
        {
            ResidentsData = residentsValue;
            NextObject = nextObjectAddress;
        }
    }
}
