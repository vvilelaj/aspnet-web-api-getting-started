using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CountingKs.Data;
using CountingKs.Models;
using CountingKs.Services;
using HttpStatusCode = System.Net.HttpStatusCode;

namespace CountingKs.Controllers
{
  public class DiaryEntriesController : BaseApiController
  {
    private readonly ICountingKsIdentityService _identityService;

    public DiaryEntriesController(ICountingKsRepository countingKsRepository, ICountingKsIdentityService identityService) : base(countingKsRepository)
    {
      _identityService = identityService;
    }

    public HttpResponseMessage Get(DateTime diaryId, int entryId)
    {
      try
      {
        if (diaryId == default(DateTime))
          return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "diaryId incorrect");

        if (entryId == default(int))
          return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "entryId incorrect");

        var entry = CountingKsRepository.GetDiaryEntry(_identityService.CurrentUser, diaryId, entryId);

        if (entry == null) return Request.CreateErrorResponse(HttpStatusCode.NotFound,"entry not found");

        return Request.CreateResponse(HttpStatusCode.OK, ModelFactory.Create(entry));
      }
      catch (Exception ex)
      {
        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
      }
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
