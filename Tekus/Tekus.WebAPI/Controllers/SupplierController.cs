using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Tekus.Core.Entities;
using Tekus.Core.Interfaces;
using Tekus.Core.Specifications;

namespace Tekus.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private  readonly IGenericRepository<Supplier> _supplierRepository;

        public SupplierController(IGenericRepository<Supplier> supplierRepository)
        {
            _supplierRepository= supplierRepository;
        }

        [HttpGet]
        public async Task<ActionResult<Supplier>> GetSuppliers()
        {
            var spec = new SupplierWithServicesSpecification();
            var supplier = await _supplierRepository.GetAllWithSpec(spec);
            return Ok(supplier);

        }

        /// <summary>
        /// spec : Debe incluir la Logica de la condicion de la consulta y tambien las relaciones entre las entidades,la relacion entre Servicios y Paises
        /// </summary>
        [HttpGet("{id}")] 
        public async Task<ActionResult<Supplier>> GetSupplier(int id)
        {
            var spec = new SupplierWithServicesSpecification(id);
            return await _supplierRepository.GetByIdWithSpec(spec); 
        } 

    }
}
