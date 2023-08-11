using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoRepository.Atrributes;

[AttributeUsage(AttributeTargets.Class)]
public class MongoCollectionAttribute : Attribute
{
  public string CollectionName { get; }
  public string DataBaseName { get; }
  public MongoCollectionAttribute(string collectionName, string dataBaseName = null)
  {
    CollectionName = collectionName;
    DataBaseName = dataBaseName;
  }
}



