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

        public async Task<Product> Get(int id)
        {
            var product = await _mtpdContext.Product.FindAsync(id);
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

        public async Task<Product> Update(int id, object obj)
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
            return _mtpdContext.Product.Any(e => e.Id == id);
        }

        public async Task<Product> Add(object obj)
        {
            _mtpdContext.Product.Add((Product)obj);

            try
            {
                await _mtpdContext.SaveChangesAsync();
            }
            catch
            {
                throw;
            }

            return (Product)obj;
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
