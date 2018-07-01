using System;
using System.Collections.Generic;
using CountingKs.Models;

namespace CountingKs.Controllers
{
  public class DiaryModel
  {
    public DiaryModel()
    {
      Entries =  new List<DiaryEntryModel>();
    }
    public string Url { get; set; }
    public DateTime CurrentDate { get; set; }
    public IEnumerable<DiaryEntryModel> Entries { get; set; }
  }
}