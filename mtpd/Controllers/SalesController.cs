using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mtpd.Models;
using mtpd.Repositories.Contract;

namespace mtpd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private readonly ImtpdRepository<Sale> _SaleRepository;

        public SaleController(ImtpdRepository<Sale> SaleRepository)
        {
            _SaleRepository = SaleRepository;
        }

        // GET: api/Sale/GetAllSale
        [HttpGet]
        [Route("GetAllSale")]
        public ActionResult<IEnumerable<Sale>> GetAllSale()
        {
            IEnumerable<Sale> Sale = _SaleRepository.GetAll();
            return Ok(Sale);
        }

        // GET: api/Sale/GetSale/1
        [HttpGet("GetSale/{id}")]
        //[Route("GetSale")]
        public ActionResult<Sale> GetSale(int id)
        {
            var Sale = _SaleRepository.Get(id);

            if (Sale == null)
            {
                return NotFound();
            }

            return Ok(Sale);
        }

        //api/Sale/UpdateSale/1
        [HttpPut("UpdateSale/{id}")]
        public ActionResult<Sale> UpdateSale(int id, Sale Sale)
        {

            if (id != Sale.Id)
            {
                return BadRequest();
            }

            var updateReturn = _SaleRepository.Update(id, Sale);

            if (updateReturn != null)
            {
                return Ok(Sale);
            }

            return BadRequest();
        }

        //api/Sale/AddSale
        [HttpPost]
        [Route("AddSale")]
        public ActionResult<Sale> AddSale(Sale Sale)
        {
            var addReturn = _SaleRepository.Add(Sale);

            if (addReturn != null)
            {
                return CreatedAtAction("GetSale", new { id = Sale.Id }, Sale);
            }

            return BadRequest();
        }

        // GET: api/Sale/DeleteSale/1
        [HttpDelete("DeleteSale/{id}")]
        public ActionResult<Sale> DeleteSale(int id)
        {
            var Sale = (Sale)_SaleRepository.Get(id);
            if (Sale == null)
            {
                return NotFound();
            }

            var deleteReturn = _SaleRepository.Delete(Sale);

            if (deleteReturn != null)
            {
                return CreatedAtAction("GetSale", new { id = Sale.Id }, Sale);
            }

            return BadRequest();


        }
    }
}
