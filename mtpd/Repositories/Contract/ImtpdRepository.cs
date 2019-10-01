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

        Task<T> Get(int id);

        Task<T> Update(int id, object obj);

        bool Exists(int id);

        Task<T> Add(object obj);

        object Delete(object obj);

    }
}
