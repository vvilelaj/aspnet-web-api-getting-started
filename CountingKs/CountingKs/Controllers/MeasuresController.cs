using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CountingKs.Data;
using CountingKs.Models;

namespace CountingKs.Controllers
{
  public class MeasuresController : BaseApiController
  {
    public MeasuresController(ICountingKsRepository repository):base(repository)
    {
    }
    public IEnumerable<MeasureModel> Get(int foodId)
    {
      return CountingKsRepository.GetMeasuresForFood(foodId).ToList().Select(m => ModelFactory.Create(m));
    }
    public MeasureModel Get(int foodId, int id)
    {
      var measure = CountingKsRepository.GetMeasure(id);

      if (measure.Food.Id == foodId) return ModelFactory.Create(measure);

      return null;
    }
  }
}
