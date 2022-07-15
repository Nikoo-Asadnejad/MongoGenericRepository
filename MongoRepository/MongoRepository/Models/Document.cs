using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoRepository.Models;


public abstract class Document 
{
  [BsonId]
  [BsonRepresentation(BsonType.String)]
  public ObjectId Id { get; set; }

  public DateTime CreateDate => Id.CreationTime;
}

