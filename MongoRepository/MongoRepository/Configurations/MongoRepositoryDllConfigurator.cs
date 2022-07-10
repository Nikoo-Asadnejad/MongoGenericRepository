using Microsoft.Extensions.DependencyInjection;
using MongoRepository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoRepository.Configurations;
  public static class MongoRepositoryDllConfigurator
  {
    public static void InjectServices(IServiceCollection services)
    {
      services.AddTransient(typeof(IMongoRepository<>) , typeof(MongoRepository<>));
    }
  }

