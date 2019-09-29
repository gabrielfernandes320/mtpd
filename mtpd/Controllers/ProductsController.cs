using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mtpd.Models;
using mtpd.Repositories.Contract;
using mtpd.Repositories.Implementation;

namespace mtpd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ImtpdRepository<Product> _productsRepository;

        public ProductsController(ImtpdRepository<Product> productsRepository)
        {
            _productsRepository = productsRepository;
        }

        // GET: api/products/GetAllProducts
        [HttpGet]
        [Route("GetAllProducts")]
        public ActionResult<IEnumerable<Product>> GetAllProducts()
        {
            IEnumerable<Product> products = _productsRepository.GetAll();
            return Ok(products);
        }

        // GET: api/products/GetProduct/1
        [HttpGet("GetProduct/{id}")]
        //[Route("GetProduct")]
        public ActionResult<Product> GetProduct(int id)
        {
            var product =  _productsRepository.Get(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        //api/products/UpdateProduct/1
        [HttpPut("UpdateProduct/{id}")]
        public ActionResult<Product> UpdateProduct(int id, Product product)
        {

            if (id != product.Id)
            {
                return BadRequest();
            }

            var updateReturn = _productsRepository.Update(id, product);

            if (updateReturn != null)
            {
                return Ok(product);
            }

            return BadRequest();
        }

        //api/products/AddProduct
        [HttpPost]
        [Route("AddProduct")]
        public ActionResult<Product> AddProduct(Product product)
        {
            var addReturn = _productsRepository.Add(product);

            if (addReturn != null)
            {
                return CreatedAtAction("GetProduct", new { id = product.Id }, product);
            }

            return BadRequest();
        }

        // GET: api/products/DeleteProduct/1
        [HttpDelete("DeleteProduct/{id}")]
        public ActionResult<Product> DeleteProduct(int id)
        {
            var product = (Product)_productsRepository.Get(id);
            if (product == null)
            {
                return NotFound();
            }

            var deleteReturn = _productsRepository.Delete(product);

            if (deleteReturn != null)
            {
                return CreatedAtAction("GetProduct", new { id = product.Id }, product);
            }

            return BadRequest();


        }
    }
}
