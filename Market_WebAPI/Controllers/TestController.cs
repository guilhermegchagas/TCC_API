﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Market_WebAPI.Controllers
{
    public class TestController : ApiController
    {
        public string Get()
        {
            return "OK";
        }
    }
}