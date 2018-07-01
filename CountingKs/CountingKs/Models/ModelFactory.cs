using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Routing;
using CountingKs.Controllers;
using CountingKs.Data.Entities;

namespace CountingKs.Models
{
  public class ModelFactory
  {
    private readonly UrlHelper _urlHelper;

    public ModelFactory(HttpRequestMessage request)
    {
      _urlHelper = new UrlHelper(request);
    }

    public FoodModel Create(Food food)
    {
      return new FoodModel
      {
        Url = _urlHelper.Link("Foods",new { foodid = food.Id}),
        Description = food.Description,
        Measures = food.Measures.Select( m => Create(m))
      };
    }

    public MeasureModel Create(Measure measure)
    {
      return new MeasureModel
      {
        Url = _urlHelper.Link("Measures", new { foodid = measure.Food.Id, id = measure.Id }),
        Description = measure.Description,
        Calories = measure.Calories
      };
    }

    internal DiaryModel Create(Diary diary)
    {
      return new DiaryModel
      {
        Url = _urlHelper.Link("Diaries", new { diaryId = diary.CurrentDate.ToString("yyyy-MM-dd") }),
        CurrentDate = diary.CurrentDate
  };
    }
  }
}