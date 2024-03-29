﻿using Microsoft.AspNetCore.Mvc;
using PaymentCalculator.Services;

namespace PaymentCalculator.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class CrashController
    {
        private CrashService CrashService;
        public CrashController(CrashService crashService)
        {
            CrashService = crashService;
        }

        /// <summary>
        /// Warning! Executing this API will crash the application.
        /// </summary>
        [HttpGet]
        public ActionResult<string> CrashIt()
        {
            CrashService.CrashIt();
            return "OK";
        }
    }
}
