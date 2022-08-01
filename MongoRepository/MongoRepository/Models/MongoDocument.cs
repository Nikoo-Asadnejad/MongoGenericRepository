using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoRepository.Models;


public abstract class MongoDocument 
{
  [BsonId]
  [BsonRequired]
  [BsonRepresentation(BsonType.ObjectId)]
  public ObjectId Id { get; set; }
  public long CreateDate { get => CreateDate; set => Id.CreationTime.ToUniversalTime(); }
  public long CreateBy { get; set; }
  public long? DeleteDate { get; set; }
  public long? DeleteBy { get; set; }

}

