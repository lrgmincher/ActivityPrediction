using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitiateRequests.Business
{
    public interface IStravaAuthorize
    {
        string GetAccessToken(string code);
    }
}
