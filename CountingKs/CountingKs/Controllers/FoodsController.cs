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
    private readonly int PAGE_SIZE_DEFAULT = 10;

    public FoodsController(ICountingKsRepository repository):base(repository)
    {
    }
    public HttpResponseMessage Get(bool includeMeasures = true,int pageIndex = 0,int pageSize =0)
    {
      IQueryable<Food> query;
      if (pageIndex < 0) pageIndex = 0;

      if (pageSize <= 0) pageSize = PAGE_SIZE_DEFAULT;

      if (includeMeasures)
        query = CountingKsRepository.GetAllFoodsWithMeasures();
      else
        query = CountingKsRepository.GetAllFoods();

      query = query.OrderBy(x => x.Description);

      var totalRows = query.Count();
      
      var result = query
        .Skip(pageIndex * pageSize)
        .Take(pageSize)
        .ToList()
        .Select(f => ModelFactory.Create(f));

      var pagedResult =
        ModelFactory.CreatePagedResult(includeMeasures, pageIndex, pageSize, totalRows, result, Request);

      return Request.CreateResponse(HttpStatusCode.OK, pagedResult);
    }

    public FoodModel Get(int foodId)
    {
      return ModelFactory.Create(CountingKsRepository.GetFood(foodId));
    }
  }
}
