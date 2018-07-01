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
    public IEnumerable<Food> Get()
    {

      try
      {
        using (var context = new CountingKsContext())
        {
          var repo = new CountingKsRepository(context);

          var results = repo.GetAllFoods().OrderBy(x => x.Description).Take(25).ToList();

          return results;
        }
      }
      catch (Exception ex)
      {
        return null;
      }
    }
  }
}
