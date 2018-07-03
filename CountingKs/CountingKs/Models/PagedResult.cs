using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CountingKs.Models
{
  public class PagedResult<T>  where T : IEnumerable
  {
    public string PrevUrl { get; set; }
    public string NextUrl { get; set; }
    public int PageIndex { get; set; }
    public int TotalRows { get; set; }
    public int TotalPages { get; set; }
    public T Result { get; set; }
  }
}