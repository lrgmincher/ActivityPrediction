using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strava.Common
{
    public class SegmentToSearch
    {

        public int Id { get; }
        public bool searched { get; set; }
        public SegmentToSearch(int id)
        {
            Id = id;
            searched = false;
        }

    }
}
