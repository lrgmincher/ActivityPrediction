using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompileResults
{
    public class CountryDetails
    {
        public CountryDetails(string country, string popuation)
        {
            this.country = country;
            this.population = Convert.ToInt32(population);
        }
        public string country { get; set; }
        public int population { get; set; }
    }
}
