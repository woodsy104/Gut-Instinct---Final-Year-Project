using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Realms;
using MongoDB.Bson;


namespace Gut_Instinct.Models
{
    public class Pin : RealmObject
    {
        [PrimaryKey]
        [MapTo("_id")]
        public ObjectId Id { get; set; } = ObjectId.GenerateNewId();

        [MapTo("owner")]
        public string Owner { get; set; }

        [MapTo("address")]
        [Required]
        public string Address { get; set; }

        [MapTo("free")]
        public bool Free { get; set; }

        [MapTo("stars")]
        public int Stars { get; set; }

        [MapTo("_partition")]
        [Required]
        public string Partition { get; set; }
    }
}
