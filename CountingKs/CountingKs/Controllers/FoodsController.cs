using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CountingKs.Data;
using CountingKs.Data.Entities;
using CountingKs.Models;

namespace CountingKs.Controllers
{
  public class FoodsController : BaseApiController
  {
    public FoodsController(ICountingKsRepository repository):base(repository)
    {
    }
    public IEnumerable<FoodModel> Get(bool includeMeasures = true)
    {
      IQueryable<Food> query;

      if (includeMeasures)
        query = CountingKsRepository.GetAllFoodsWithMeasures();
      else
        query = CountingKsRepository.GetAllFoods();

      var results = query
        .OrderBy(x => x.Description)
        .Take(25)
        .ToList()
        .Select(f => ModelFactory.Create(f));
      return results;
    }

    public FoodModel Get(int foodId)
    {
      return ModelFactory.Create(CountingKsRepository.GetFood(foodId));
    }
  }
}
