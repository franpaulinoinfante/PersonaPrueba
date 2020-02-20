using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonaPrueba.Domain.Contracts
{
    public interface IGenericOperation<Model> where Model : class
    {
        ObjectValues.EntityState State { set; }

        string SaveChanges(Model model);

        IEnumerable<Model> GetAll();
    }
}
