using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Controllers
{
    interface IControl<T>
    {
        List<T> GetAll();
        void Add(T item);

        void Remove(T item);
    }
}
