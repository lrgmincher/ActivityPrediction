using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Strava.Common;
using Strava.Common.Domain;
using Strava.Common.FromExternalSources.Domain;

namespace Strava.Providers.ConcreteClasses
{
    public class DataAccessProvider : IDataAccessProvider
    {
        public void SaveRiderData(Cyclist[] cyclistData)
        {
            throw new NotImplementedException();
        }

        public void SaveSegmentDetails(segment[] segments)
        {
            throw new NotImplementedException();
        }
    }
}
