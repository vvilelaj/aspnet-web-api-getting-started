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
  }
}
