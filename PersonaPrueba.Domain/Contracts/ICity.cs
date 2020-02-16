using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonaPrueba.Domain.Contracts
{
    public interface ICity : IGenericDataAccess<Models.CityModel>
    {
        int CityID { get; set; }
        int RegionID { get; set; }
        string CityName { get; set; }
    }
}
