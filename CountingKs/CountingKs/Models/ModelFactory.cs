using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Routing;
using System.Web.UI.WebControls;
using CountingKs.Controllers;
using CountingKs.Data;
using CountingKs.Data.Entities;

namespace CountingKs.Models
{
  public class ModelFactory
  {
    private readonly UrlHelper _urlHelper;
    private readonly ICountingKsRepository _countingKsRepository;

    public ModelFactory(HttpRequestMessage request, ICountingKsRepository countingKsRepository)
    {
      _urlHelper = new UrlHelper(request);
      _countingKsRepository = countingKsRepository;
    }

    public FoodModel Create(Food food)
    {
      return new FoodModel
      {
        Url = _urlHelper.Link("Foods", new { foodid = food.Id }),
        Description = food.Description,
        Measures = food.Measures.Select(m => Create(m))
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

    public DiaryModel Create(Diary diary)
    {
      return new DiaryModel
      {
        Url = _urlHelper.Link("Diaries", new { diaryId = diary.CurrentDate.ToString("yyyy-MM-dd") }),
        CurrentDate = diary.CurrentDate,
        Entries = diary.Entries.Select(e => Create(e))
      };
    }

    public DiaryEntry Parse(DiaryEntryModel diaryEntry)
    {
      try
      {
        if (diaryEntry == null) throw new ArgumentNullException(nameof(diaryEntry));

        var entry = new DiaryEntry();

        if (diaryEntry.Quantity != default(double)) entry.Quantity = diaryEntry.Quantity;

        var uri = new Uri(diaryEntry.MeasureUrl);
        var measureId = int.Parse(uri.Segments.Last());
        var measure = _countingKsRepository.GetMeasure(measureId);
        entry.FoodItem = measure.Food;
        entry.Measure = measure;

        return entry;
      }
      catch
      {
        return null;
      }
    }

    public DiaryEntryModel Create(DiaryEntry diaryEntry)
    {
      var measure = Create(diaryEntry.Measure);
      var food = Create(diaryEntry.FoodItem);
      return new DiaryEntryModel
      {
        Url = _urlHelper.Link("DiaryEntries", new { diaryId = diaryEntry.Diary.CurrentDate.ToString("yyyy-MM-dd"), entryid = diaryEntry.Id }),
        Quantity = diaryEntry.Quantity,
        MeasureUrl = measure.Url,
        MeasureDescription = measure.Description,
        FoodDescription = food.Description
      };
    }
  }
}