﻿using MongoDB.Bson;
using Realms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gut_Instinct.Models
{
    public class Appointment : RealmObject
    {
        [PrimaryKey]
        [MapTo("_id")]
        public ObjectId Id { get; set; } = ObjectId.GenerateNewId();

        [MapTo("owner")]
        public string Owner { get; set; }

        [MapTo("name")]
        [Required]
        public string Name { get; set; }

        [MapTo("completed")]
        public bool Completed { get; set; }

        [MapTo("colour")]
        public string Colour { get; set; }

        [MapTo("date")]
        public DateTimeOffset Date { get; set; }

        [MapTo("_partition")]
        [Required]
        public string Partition { get; set; }
    }
}
