using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonaPrueba.DataAccess.Repository.Contracts
{
    public interface IGenericRepository<Entity> where Entity : class
    {
        int Add(Entity entity);
        void Edit(Entity entity);
        void Delete(int entity);
        IEnumerable<Entity> GetAll();
    }
}
