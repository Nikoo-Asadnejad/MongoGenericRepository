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
using System.Reflection;
using MongoDB.Driver.Linq;
using MongoRepository.Models;

namespace MongoRepository.Repository
{
  public partial class MongoRepository<TDocument> : IMongoRepository<TDocument>  where TDocument : MongoDocument
  {
    private readonly IConfiguration _configuration;
    private readonly IMongoCollection<TDocument> _collection;
    private readonly IMongoDatabase _database;
    private string? _databaseName = CollectionsUtils.GetDataBaseName(typeof(TDocument));
    private readonly string _collectionName = CollectionsUtils.GetCollectionName(typeof(TDocument));
    public MongoRepository(IConfiguration configuration)
    {
      _configuration = configuration;
      _databaseName = string.IsNullOrWhiteSpace(_databaseName) ? _configuration["MongoDb:DatabaseName"] : _databaseName;

      string connectionString  = _configuration["MongoDb:ConnectionString"];

      _database = new MongoClient(connectionString).GetDatabase(_databaseName);
      _collection = _database.GetCollection<TDocument>(_collectionName);
    }

    public async Task<IQueryable<TDocument>> AsQueryable()
    => _collection.AsQueryable();
    public async Task<List<TDocument>> FilterByAsync(
        Expression<Func<TDocument, bool>> filterExpression)
    => await _collection.FindAsync(filterExpression).Result.ToListAsync();

    public async Task<List<TProjected>> FilterByAsync<TProjected>(
        Expression<Func<TDocument, bool>> filterExpression,
        Expression<Func<TDocument, TProjected>> projectionExpression)
    => await _collection.Find(filterExpression).Project(projectionExpression).ToListAsync();

    public async Task<TDocument> FindAsync(Expression<Func<TDocument, bool>> filterExpression)
    => await _collection.FindAsync<TDocument>(filterExpression).Result.FirstOrDefaultAsync();

    public async Task<TDocument> FindByIdAsync(string id)
    {
      ObjectId objectId = new ObjectId(id);
      var filter = Builders<TDocument>.Filter.Eq(x=> x.Id, objectId);
      return await _collection.FindAsync(filter).Result.FirstOrDefaultAsync();
    }

    public async Task InsertAsync(TDocument model)
    => await _collection.InsertOneAsync(model);

    public async Task InsertManyAsync(ICollection<TDocument> models)
    => await _collection.InsertManyAsync(models);

    public async Task UpdateAsync(TDocument model , UpdateDefinition<TDocument> updateDefenition)
    {
      var filter = Builders<TDocument>.Filter.Eq(x => x.Id, model.Id);
      await _collection.UpdateOneAsync(filter, updateDefenition);
    }

    public async Task ReplaceOneAsync(TDocument model)
    {
      var filter = Builders<TDocument>.Filter.Eq(x => x.Id, model.Id);
      await _collection.ReplaceOneAsync(filter,model);
    }

    public async Task ReplaceManyAsync(List<TDocument> models)
    {
      models.ForEach(async model =>
         await _collection.ReplaceOneAsync(Builders<TDocument>.Filter.Eq(x => x.Id, model.Id), model));
    }

    public async Task DeleteAsync(Expression<Func<TDocument, bool>> filterExpression)
    => await _collection.FindOneAndDeleteAsync(filterExpression);

    public async Task DeleteAsync(TDocument model)
    {
      var filter = Builders<TDocument>.Filter.Eq(x=> x.Id, model.Id);
      await _collection.DeleteOneAsync(filter);
    }

    public async Task DeleteByIdAsync(string id)
    {
      ObjectId objectId = new ObjectId(id);
      var filter = Builders<TDocument>.Filter.Eq(x=> x.Id, objectId);
      await _collection.DeleteOneAsync(filter);
    }

    public async Task DeleteManyAsync(Expression<Func<TDocument, bool>> filterExpression)
    => await _collection.DeleteManyAsync(filterExpression);

    public async Task DeleteManyAsync(List<TDocument> models)
    {
      var filter = Builders<TDocument>.Filter.Where(x => models.Contains(x));
      await _collection.DeleteManyAsync(filter);
    }

    public async Task<bool> IsExist(Expression<Func<TDocument, bool>> query)
      =>await  _collection.AsQueryable().AnyAsync(query);
    
    public async Task<bool> IsExist(string id)
      => await _collection.AsQueryable().AnyAsync(x=> x.Id.Equals(id));
    
    public void DropCollection()
    => _database.DropCollection(_collectionName);

    public async Task RenameCollectionAsync(string newName)
      => await _database.RenameCollectionAsync(_collectionName ,newName);

   




  }
}
