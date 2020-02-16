using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonaPrueba.DataAccess.Repository.Entities
{
    public class CityEntity
    {
        public int CityID { get; set; }
        public int RegionID { get; set; }
        public string CityName { get; set; }
    }
}
