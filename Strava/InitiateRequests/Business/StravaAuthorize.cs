using InitiateRequests.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InitiateRequests.Business
{
    public class StravaAuthorize : IStravaAuthorize
    {
        StravaProvider stravaProvider = new StravaProvider();
        public string GetAccessToken(string code)
        {
            return stravaProvider.GetToken(code).Result;
        }
    }
}