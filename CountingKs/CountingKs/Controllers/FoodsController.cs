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
  public class FoodsController : ApiController
  {
    private readonly ICountingKsRepository _repository;
    private readonly ModelFactory _modelFactory;

    public FoodsController(ICountingKsRepository repository)
    {
      _repository = repository;
      _modelFactory= new ModelFactory();
    }
    public IEnumerable<FoodModel> Get()
    {
      var results = _repository.GetAllFoodsWithMeasures()
        .OrderBy(x => x.Description)
        .Take(25)
        .ToList()
        .Select(f => _modelFactory.Create(f));
      return results;
    }

    public FoodModel Get(int id)
    {
      return _modelFactory.Create(_repository.GetFood(id));
    }
  }
}
