using Strava.Common.FromExternalSources;
using Strava.Providers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strava.Providers.ConcreteClasses
{
    public class RandomNumbersProvider : IRandomNumbersProvider
    {
        public Coordinates getRandomCoordinates()
        {
            throw new NotImplementedException();
        }
    }
}
