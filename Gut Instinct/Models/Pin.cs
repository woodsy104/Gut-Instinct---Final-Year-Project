using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Realms;
using MongoDB.Bson;


namespace Gut_Instinct.Models
{
    class Pin
    {
        //[PrimaryKey]
        //[MapTo("_address")]
        //[Required]
        public string Address { get; set; }

        //[MapTo("_name")]
        //[Required]
        public string Name { get; set; }

        //[MapTo("_location")]
        //public ObservableCollection<double> Location { get; set; }

        //[MapTo("_type")]
        public string type { get; set; }

    }
}
