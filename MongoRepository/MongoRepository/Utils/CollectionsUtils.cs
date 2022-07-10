using MongoRepository.Atrributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoRepository.Utils
{
  public static class CollectionsUtils
  {
    public static string GetCollectionName(Type collection)
    => ((MongoCollectionAttribute)collection
              .GetCustomAttributes(typeof(MongoCollectionAttribute),true)
              .FirstOrDefault())?.CollectionName;


    
  }
}
