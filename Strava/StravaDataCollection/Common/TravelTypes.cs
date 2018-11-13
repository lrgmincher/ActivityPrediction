using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strava.Common
{
    public static class TravelTypes
    {
       public static Dictionary<travelEnum, string> travelTypesApiParameters = new Dictionary<travelEnum, string>()
        {
           { travelEnum.Cycling, "riding" },
           { travelEnum.Running, "running" }
        };
    }

    public enum travelEnum
    {
        Cycling,
        Running,
        Swimming
    }
}
