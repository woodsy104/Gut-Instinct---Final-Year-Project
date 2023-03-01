using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gut_Instinct.Models
{
    public class ActivePin
    {
        public Location Location { get; set; }
        public string Address { get; set; }
        public string Label { get; set;}
    }
}
//This class exists as a middle man for pins as Realm doesn't currently support Location Type