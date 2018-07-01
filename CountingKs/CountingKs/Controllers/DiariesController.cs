using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CountingKs.Data;
using CountingKs.Models;
using CountingKs.Services;

namespace CountingKs.Controllers
{
  public class DiariesController : BaseApiController
  {
    private readonly ICountingKsIdentityService _identityService;

    public DiariesController(ICountingKsRepository countingKsRepository, ICountingKsIdentityService identityService) : base(countingKsRepository)
    {
      _identityService = identityService;
    }

    public IEnumerable<DiaryModel> Get()
    {
      var userName = _identityService.CurrentUser;
      return CountingKsRepository.GetDiaries(userName).ToList().Select(d => ModelFactory.Create(d));
    }

    public HttpResponseMessage Get(DateTime diaryId)
    {
      var userName = _identityService.CurrentUser;
      var diary = CountingKsRepository.GetDiary(userName, diaryId);

      if (diary == null) return Request.CreateResponse(HttpStatusCode.NotFound);

      return Request.CreateResponse(HttpStatusCode.OK,
          ModelFactory.Create(CountingKsRepository.GetDiary(userName, diaryId)));
    }

    public HttpResponseMessage Post(DateTime diaryId, [FromBody] DiaryEntryModel diaryEntry)
    {
      try
      {
        var entry = ModelFactory.Parse(diaryEntry);

        if (entry == null) return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "error in request body");

        var diary = CountingKsRepository.GetDiary(_identityService.CurrentUser, diaryId);
        if (diary == null) return Request.CreateErrorResponse(HttpStatusCode.NotFound, "diary does not exist");

        diary.Entries.Add(entry);
        CountingKsRepository.SaveAll();
        diaryEntry = ModelFactory.Create(entry);

        return Request.CreateResponse(HttpStatusCode.Created, diaryEntry);
      }
      catch (Exception ex)
      {
        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
      }
    }
  }
}
