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

namespace MongoRepository.Repository
{
  public partial class MongoRepository<TDocument> : IMongoRepository<TDocument> 
  {
    public TDocument Find(Expression<Func<TDocument, bool>> filterExpression)
    {
      throw new NotImplementedException();
    }

    public TDocument FindById(string id)
    {
      throw new NotImplementedException();
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

    public void Update(TDocument model)
    {
      throw new NotImplementedException();
    }

    public void Delete(Expression<Func<TDocument, bool>> filterExpression)
    => _collection.DeleteOne(filterExpression);

    public void DeleteById(string id)
    {
      throw new NotImplementedException();
    }

    public void DeleteMany(Expression<Func<TDocument, bool>> filterExpression)
    => _collection.DeleteMany(filterExpression);
  }
}
