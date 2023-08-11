using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MongoRepository.Repository
{
  public partial interface IMongoRepository<TDocument> 
  {
    TDocument Find(Expression<Func<TDocument, bool>> filterExpression);

    TDocument FindById(string id);

    List<TDocument> FilterBy(
        Expression<Func<TDocument, bool>> filterExpression);

    List<TProjected> FilterBy<TProjected>(
       Expression<Func<TDocument, bool>> filterExpression,
       Expression<Func<TDocument, TProjected>> projectionExpression);

    void Insert(TDocument model);

    void InsertMany(ICollection<TDocument> models);

    void Update(TDocument model , UpdateDefinition<TDocument> updateDefinition);

    void ReplaceOne(TDocument model);

    void ReplaceMany(List<TDocument> models);

    void Delete(Expression<Func<TDocument, bool>> filterExpression);

    void DeleteById(string id);

    void DeleteMany(Expression<Func<TDocument, bool>> filterExpression);

  }
}
