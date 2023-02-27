using MongoDB.Bson;
using Realms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gut_Instinct.Models
{
    public class Food : RealmObject
    {
        [PrimaryKey]
        [MapTo("_id")]
        public ObjectId Id { get; set;} = ObjectId.GenerateNewId();

        [MapTo ("owner")]
        public string Owner { get; set;}

        [MapTo("foodname")]
        public string FoodName { get; set;}

        [MapTo("description")]
        public string Description { get; set;}

        [MapTo("colour")]
        public string Colour { get; set;}
    }       
}
