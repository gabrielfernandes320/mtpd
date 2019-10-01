using mtpd.Repositories.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mtpd.Models;
using Microsoft.EntityFrameworkCore;

namespace mtpd.Repositories.Implementation
{
    public class SalesRepository : ImtpdRepository<Sale>
    {
        readonly mtpdContext _mtpdContext;

        public SalesRepository(mtpdContext context)
        {
            _mtpdContext = context;
        }

        public IEnumerable<Sale> GetAll()
        {
            return _mtpdContext.Sale.ToList();
        }

        public async Task<Sale> Get(int id)
        {
            var sale = await _mtpdContext.Sale.FindAsync(id);
            return sale;
        }

        public async Task<Sale> Update(int id, object obj)
        {
            _mtpdContext.Entry(obj).State = EntityState.Modified;

            try
            {
                await _mtpdContext.SaveChangesAsync();
            }
            catch
            {
                throw;
            }

            return await Get(id);

        }

        public bool Exists(int id)
        {
            return _mtpdContext.Sale.Any(e => e.Id == id);
        }

        public async Task<Sale> Add(object obj)
        {
            _mtpdContext.Sale.Add((Sale)obj);

            try
            {
                await _mtpdContext.SaveChangesAsync();
            }
            catch
            {
                throw;
            }

            return (Sale)obj;
        }

        public object Delete(object obj)
        {
            _mtpdContext.Sale.Remove((Sale)obj);

            try
            {
                _mtpdContext.SaveChanges();
            }
            catch
            {
                throw;
            }

            return obj;
        }

    }
}
