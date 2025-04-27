using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.InterfacesRepositorios
{
    public interface IRepositorio<T>
    {
        void Add(T obj);
        void Update(T obj);
        void Remove(int id);
        T FindById(int id);
        IEnumerable<T> FindAll();
    }
}
