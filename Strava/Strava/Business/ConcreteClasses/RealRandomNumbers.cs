using Strava.Business.Interfaces;
using Strava.Common.FromExternalSources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strava.Business.ConcreteClasses
{
    public class RealRandomNumbers : IRandomNumberGen
    {


        List<Coordinates> IRandomNumberGen.SaveRandomCoordinates(int NumberOfCoordinates)
        {
            throw new NotImplementedException();
        }
    }
}
