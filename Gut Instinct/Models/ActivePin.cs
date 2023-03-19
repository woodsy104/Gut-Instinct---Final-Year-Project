using MongoDB.Bson;
using Realms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gut_Instinct.Models
{
    public class ActivePin : RealmObject
    {
        [PrimaryKey]
        [MapTo("_id")]
        public ObjectId Id { get; set; } = ObjectId.GenerateNewId();

        [Required]
        [MapTo("_partition")]
        public string Partition { get; set; }

        [MapTo("owner")]
        public string Owner { get; set; }

        [MapTo("location")]
        public string Location { get; set; }

        [MapTo("address")]
        public string Address { get; set; }

        [MapTo("label")]
        public string Label { get; set;}
    }
}
