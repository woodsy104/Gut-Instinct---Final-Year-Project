using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gut_Instinct.Models
{
    class Pin
    {
        public ObservableCollection<double> Location { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string type { get; set; }

    }
}
