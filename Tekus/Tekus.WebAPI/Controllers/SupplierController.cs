using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Tekus.Core.Entities;
using Tekus.Core.Interfaces;

namespace Tekus.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private  readonly ISupplierRepository _supplierRepository;

        public SupplierController(ISupplierRepository supplierRepository)
        {
            _supplierRepository= supplierRepository;
        }

        [HttpGet]
        public async Task<ActionResult<Supplier>> GetSuppliers()
        {
            var supplier = await _supplierRepository.GetSupplierAsync();
            return Ok(supplier);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Supplier>> GetSupplier(int id)
        {
            return await _supplierRepository.GetSupplierByIdAsync(id); 
        } 

    }
}
