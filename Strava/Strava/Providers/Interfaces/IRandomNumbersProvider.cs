using Strava.Common.FromExternalSources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strava.Providers.Interfaces
{
    public interface IRandomNumbersProvider
    {
        Coordinates getRandomCoordinates();
    }
}
