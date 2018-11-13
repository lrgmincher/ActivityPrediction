using InitiateRequests.Business;
using Strava;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InitiateRequests.Controllers
{
    public class HomeController : Controller
    {
        StravaAuthorize stravaAuthorize;
        public HomeController()
        {
            stravaAuthorize = new StravaAuthorize();
        }
        public ActionResult Index()
        {
            return Redirect("https://www.strava.com/oauth/authorize?client_id=29906&redirect_uri=http://localhost:56869/Home/GetAccessToken&response_type=code&scope=public");
        }

        public ActionResult GetAccessToken(string code)
        {
            if (String.IsNullOrEmpty(code))
            {
                throw new ArgumentNullException();
            }
            ViewBag.Code = code;
            return View("GetAccessToken");
        }

        public ActionResult Execute(string accessToken)
        {
            ViewBag.AccessToken = accessToken;
            if (String.IsNullOrEmpty(accessToken))
            {
                throw new ArgumentNullException();
            }
            Executions executions = new Executions();            
            executions.StandardExecution(accessToken);
            return View("Finished");
        }

    }
}