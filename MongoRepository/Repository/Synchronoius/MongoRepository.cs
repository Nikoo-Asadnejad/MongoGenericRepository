using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoRepository.Utils;
using System.Linq.Expressions;
using MongoDB.Bson;

namespace MongoRepository.Repository
{
  public partial class MongoRepository<TDocument> : IMongoRepository<TDocument> 
  {
    public TDocument Find(Expression<Func<TDocument, bool>> filterExpression)
    => _collection.FindSync(filterExpression).FirstOrDefault();

    public TDocument FindById(string id)
    {
      ObjectId objectId = new ObjectId(id);
      var filter = Builders<TDocument>.Filter.Eq(x => x.Id, objectId);
      return _collection.Find(filter).FirstOrDefault();
    }

    public List<TDocument> FilterBy(
        Expression<Func<TDocument, bool>> filterExpression)
    => _collection.FindSync(filterExpression).ToList();

    public List<TProjected> FilterBy<TProjected>(
       Expression<Func<TDocument, bool>> filterExpression,
       Expression<Func<TDocument, TProjected>> projectionExpression)
     => _collection.Find(filterExpression).Project(projectionExpression).ToList();

    public void Insert(TDocument model)
    => _collection.InsertOne(model);

    public void InsertMany(ICollection<TDocument> models)
    => _collection.InsertMany(models);

    public void Update(TDocument model , UpdateDefinition<TDocument> updateDefinition)
    {
      var filter = Builders<TDocument>.Filter.Eq(x=> x.Id , model.Id);
      _collection.UpdateOne(filter, updateDefinition);
    }

    public void ReplaceOne(TDocument model)
    {
      var filter = Builders<TDocument>.Filter.Eq(x => x.Id, model.Id);
       _collection.ReplaceOne(filter, model);
    }

    public void ReplaceMany(List<TDocument> models)
    {
      models.ForEach(model =>
      _collection.ReplaceOne(Builders<TDocument>.Filter.Eq(x => x.Id, model.Id), model));
    }

    public void Delete(Expression<Func<TDocument, bool>> filterExpression)
    => _collection.DeleteOne(filterExpression);

    public void DeleteById(string id)
    {
      ObjectId objectId = new ObjectId(id);
      var filter = Builders<TDocument>.Filter.Eq(x => x.Id, objectId);
      _collection.DeleteOne(filter);

    }

    public void DeleteMany(Expression<Func<TDocument, bool>> filterExpression)
    => _collection.DeleteMany(filterExpression);
  }
}
