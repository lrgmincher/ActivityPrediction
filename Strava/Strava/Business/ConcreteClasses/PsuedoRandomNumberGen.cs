using Strava.Business.Interfaces;
using Strava.Common.FromExternalSources;
using Strava.Common.InternalData;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strava.Business.ConcreteClasses
{
    public class PsuedoRandomNumberGen : IRandomNumberGen
    {
        Random random;
        string path = "";
        public PsuedoRandomNumberGen()
        {
            random = new Random();
        }

        public List<Coordinates> SaveRandomCoordinates(int NumberOfCoordinates)
        {
            path = DataAccessContext.RandomCoordinatePath + "/" + DateTime.Now.Ticks;
            List<Coordinates> coordinateCollection = new List<Coordinates>();
            Coordinates coordinates;
            using (StreamWriter file = new StreamWriter(path))
            {
                file.WriteLine("{");
                
                for (int i = 0; i < NumberOfCoordinates; i++)
                {
                    coordinates = GetPsuedoRandomCoords();
                    coordinateCollection.Add(coordinates);
                    file.WriteLine("[" + coordinates.Lattitude + "," + coordinates.Longtitude + "]");
                }
                file.WriteLine("}");
            }
            return coordinateCollection;
        }

        private Coordinates GetPsuedoRandomCoords()
        {
            Coordinates coordinates = new Coordinates();

            coordinates.Lattitude = (float)random.NextDouble() * random.Next(-180,180);
            coordinates.Longtitude = (float)random.NextDouble() * random.Next(-180, 180);

            return coordinates;
        }
    }
}
