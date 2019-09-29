using Microsoft.EntityFrameworkCore;
using mtpd.Models;
using mtpd.Repositories.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mtpd.Repositories.Implementation
{
    public class ProductsRepository : ImtpdRepository<Product>
    {

            readonly mtpdContext _mtpdContext;

            public ProductsRepository(mtpdContext context)
            {
                _mtpdContext = context;
            }

        public IEnumerable<Product> GetAll()
        {
                return _mtpdContext.Product.ToList();
        }

        object ImtpdRepository<Product>.Get(int id)
        {
            var product = _mtpdContext.Product.Find(id);
            return product;
        }

        public object Update(int id, Product product)
        {
            _mtpdContext.Entry(product).State = EntityState.Modified;

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

            return product;

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
            return _mtpdContext.Product.Any(e => e.Id == id);
        }

        public object Add(object obj)
        {
            _mtpdContext.Product.Add((Product)obj);

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
            _mtpdContext.Product.Remove((Product)obj);

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
