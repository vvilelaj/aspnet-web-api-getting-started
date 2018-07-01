using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CountingKs.Data;
using CountingKs.Data.Entities;

namespace CountingKs.Controllers
{
  public class FoodsController : ApiController
  {
    private ICountingKsRepository _repository;

    public FoodsController(ICountingKsRepository repository)
    {
      _repository = repository;
    }
    public IEnumerable<Food> Get()
    {
      var results = _repository.GetAllFoods().OrderBy(x => x.Description).Take(25).ToList();
      return results;
    }
  }
}
