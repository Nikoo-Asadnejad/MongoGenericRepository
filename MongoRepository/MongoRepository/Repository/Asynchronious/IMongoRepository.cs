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
    Task<IQueryable<TDocument>> AsQueryable();
    Task<List<TDocument>> FilterByAsync(
        Expression<Func<TDocument, bool>> filterExpression);
    Task<List<TProjected>> FilterByAsync<TProjected>(
        Expression<Func<TDocument, bool>> filterExpression,
        Expression<Func<TDocument, TProjected>> projectionExpression);
    Task<TDocument> FindAsync(Expression<Func<TDocument, bool>> filterExpression);
    Task<TDocument> FindByIdAsync(string id);
    Task InsertAsync(TDocument model);
    Task InsertManyAsync(ICollection<TDocument> models);
    Task UpdateAsync(TDocument model , UpdateDefinition<TDocument> updateDefinition);
    Task ReplaceOneAsync(TDocument model);
    Task ReplaceManyAsync(List<TDocument> models);
    Task DeleteAsync(Expression<Func<TDocument, bool>> filterExpression);
    Task DeleteByIdAsync(string id);
    Task DeleteManyAsync(Expression<Func<TDocument, bool>> filterExpression);
    Task<bool> IsExist(Expression<Func<TDocument, bool>> query);
    Task<bool> IsExist(string id);
    void DropCollection();
  }
}
