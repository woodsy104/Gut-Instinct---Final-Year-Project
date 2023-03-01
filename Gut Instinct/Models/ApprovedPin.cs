using MongoDB.Bson;
using Realms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gut_Instinct.Models
{
    public class ApprovedPin : RealmObject
    {
        [PrimaryKey]
        [MapTo("_id")]
        public ObjectId Id { get; set; } = ObjectId.GenerateNewId();

        [MapTo("name")]
        public string Name { get; set; }

        [MapTo("coordx")]
        public double XCoord { get; set; }

        [MapTo("coordy")]
        public double YCoord { get; set; }

        [MapTo("location")]
        public string Coords { get; set; }

        [Required]
        [MapTo("_partition")]
        public string Partition { get; set;}
    }
}
