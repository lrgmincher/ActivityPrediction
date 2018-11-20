using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CompileResults.Statistics;

namespace CompileResults
{
    public interface IGeoNames
    {
        Task<CountryDetails> getCounty(double latt, double longt);
    }
}
