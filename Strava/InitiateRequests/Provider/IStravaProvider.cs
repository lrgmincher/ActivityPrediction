using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitiateRequests.Provider
{
    public interface IStravaProvider
    {
        Task<string> GetToken(string code);
    }
}
