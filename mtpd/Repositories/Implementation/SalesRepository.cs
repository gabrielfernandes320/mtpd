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

        object ImtpdRepository<Sale>.Get(int id)
        {
            var Sale = _mtpdContext.Sale.Find(id);
            return Sale;
        }

        public object Update(int id, Sale Sale)
        {
            _mtpdContext.Entry(Sale).State = EntityState.Modified;

            try
            {
                _mtpdContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Exists(id))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }

            return Sale;

        }

        public object Update(int id, object obj)
        {
            _mtpdContext.Entry(obj).State = EntityState.Modified;

            try
            {
                _mtpdContext.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Exists(id))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }

            return obj;
        }

        public bool Exists(int id)
        {
            return _mtpdContext.Sale.Any(e => e.Id == id);
        }

        public object Add(object obj)
        {
            _mtpdContext.Sale.Add((Sale)obj);

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
