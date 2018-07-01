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
    public abstract class BaseApiController : ApiController
    {
      private ModelFactory _modelFactory;

      protected BaseApiController(ICountingKsRepository countingKsRepository)
      {
        CountingKsRepository = countingKsRepository;
      }

      public ICountingKsRepository CountingKsRepository { get; }

      public ModelFactory ModelFactory
      {
        get
        {
          if(_modelFactory == null) _modelFactory =  new ModelFactory(this.Request);

          return _modelFactory;
        }
      }
  }
}
