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
  public class MeasuresController : ApiController
  {
    private readonly ICountingKsRepository _repository;
    private readonly ModelFactory _modelFactory;

    public MeasuresController(ICountingKsRepository repository)
    {
      _repository = repository;
      _modelFactory = new ModelFactory();
    }
    public IEnumerable<MeasureModel> Get(int foodId)
    {
      return _repository.GetMeasuresForFood(foodId).ToList().Select(m => _modelFactory.Create(m));
    }
    public MeasureModel Get(int foodId, int id)
    {
      var measure = _repository.GetMeasure(id);

      if (measure.Food.Id == foodId) return _modelFactory.Create(measure);

      return null;
    }
  }
}
