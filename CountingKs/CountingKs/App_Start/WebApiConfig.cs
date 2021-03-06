﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Newtonsoft.Json.Serialization;

namespace CountingKs
{
  public static class WebApiConfig
  {
    public static void Register(HttpConfiguration config)
    {
      config.Routes.MapHttpRoute(
          name: "Foods",
          routeTemplate: "api/nutrition/{controller}/{foodId}",
          defaults: new { Controller="foods", foodId = RouteParameter.Optional }
      );

      config.Routes.MapHttpRoute(
        name: "Measures",
        routeTemplate: "api/nutrition/foods/{foodId}/measures/{id}",
        defaults: new { Controller = "measures", id = RouteParameter.Optional }
      );

      config.Routes.MapHttpRoute(
        name: "Diaries",
        routeTemplate: "api/user/diaries/{diaryid}",
        defaults: new { Controller = "diaries", diaryid = RouteParameter.Optional }
      );

      config.Routes.MapHttpRoute(
        name: "DiaryEntries",
        routeTemplate: "api/user/diaries/{diaryid}/entries/{entryid}",
        defaults: new { Controller = "diaryEntries", entryid = RouteParameter.Optional }
      );

      // Uncomment the following line of code to enable query support for actions with an IQueryable or IQueryable<T> return type.
      // To avoid processing unexpected or malicious queries, use the validation settings on QueryableAttribute to validate incoming queries.
      // For more information, visit http://go.microsoft.com/fwlink/?LinkId=279712.
      //config.EnableQuerySupport();

      // serialize properties to camelcase 
      var jsonFormatter = config.Formatters.JsonFormatter;
      jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
    }
  }
} 