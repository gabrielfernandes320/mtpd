using mtpd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mtpd.Repositories.Contract
{
    public interface ImtpdRepository<T>
    {
        IEnumerable<T> GetAll();

        object Get(int id);

        object Update(int id, object obj);

        bool Exists(int id);

        object Add(object obj);

        object Delete(object obj);

    }
}
