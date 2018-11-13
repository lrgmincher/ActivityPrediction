using Strava.Common.FromExternalSources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strava.Business.Interfaces
{
    public interface IRandomNumberGen
    {
        List<Coordinates> SaveRandomCoordinates(int NumberOfCoordinates);
    }
}
