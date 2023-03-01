﻿using MongoDB.Bson;
using Realms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gut_Instinct.Models
{
    public class Comment : RealmObject
    {
        [PrimaryKey]
        [MapTo("_id")]
        public ObjectId Id { get; set; } = ObjectId.GenerateNewId();


        [MapTo("owner")]
        public string Owner { get; set; }

        [MapTo("threadTitle")]
        public string ThreadTitle { get; set; }

        [MapTo("body")]
        public string Body { get; set; }

        [MapTo("_partition")]
        [Required]
        public string Partition { get; set; }
    }
}
