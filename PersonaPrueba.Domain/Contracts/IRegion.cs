using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonaPrueba.Domain.Contracts
{
    public interface IRegion : IGenericDataAccess<Models.RegionModel>
    {
        int RegionID { get; set; }
        string RegionName { get; set; }
    }
}
