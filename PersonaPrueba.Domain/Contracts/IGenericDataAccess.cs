using System.Collections;
using System.Collections.Generic;

namespace PersonaPrueba.Domain.Contracts
{
    public interface IGenericDataAccess<Model> where Model : class
    {
        ObjectValues.EntityState State { set; }

        string SaveChanges();

        IEnumerable<Model> GetAll();
    }
}
