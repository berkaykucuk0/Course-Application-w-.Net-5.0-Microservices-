using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Course.Services.Catalog.Entities
{
    public class Courses
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]  //Specifying to db
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        [BsonRepresentation(BsonType.Decimal128)] //Specifying to db
        public decimal Price { get; set; }

        [BsonRepresentation(BsonType.DateTime)] //Specifying to db
        public DateTime CreatedTime { get; set; }

        public string UserId { get; set; }
        public string Picture { get; set; }

        public Feature Feature { get; set; }      // for one to one

        [BsonRepresentation(BsonType.ObjectId)]
        public string CategoryId { get; set; }

        [BsonIgnore]                          //Ignore this when fetching datas from db. For list course category
        public Category Category { get; set; }

    }
}
