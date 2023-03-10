using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tekus.Core.Entities;
using Tekus.Core.Interfaces;
using Tekus.Core.Specifications;
using Tekus.WebAPI.DTOs;
using Tekus.WebAPI.Errors;

namespace Tekus.WebAPI.Controllers
{
 
    public class SupplierController : BaseApiController
    {
        private  readonly IGenericRepository<Supplier> _supplierRepository;
        private readonly IMapper _mapper;
        public SupplierController(IGenericRepository<Supplier> supplierRepository,IMapper mapper)
        {
            _supplierRepository= supplierRepository;
            _mapper = mapper;
        }

        [HttpGet] 
        public async Task<ActionResult<Supplier>> GetSuppliers([FromQuery] SupplierSpecificationParams supplierParams)
        {
            SupplierWithServicesSpecification spec = 
                new SupplierWithServicesSpecification(supplierParams);
            System.Collections.Generic.IReadOnlyList<Supplier> supplier = await _supplierRepository.GetAllWithSpec(spec);
            return Ok(_mapper.Map<IReadOnlyList<Supplier>,IReadOnlyList<SupplierDto>>(supplier));

        }

        /// <summary>
        /// spec : Debe incluir la Logica de la condicion de la consulta y tambien las relaciones entre las entidades,la relacion entre Servicios y Paises
        /// </summary>
        [HttpGet("{id}")] 
        public async Task<ActionResult<SupplierDto>> GetSupplier(int id)
        {
            SupplierWithServicesSpecification spec = new SupplierWithServicesSpecification(id);
            Supplier supplier = await _supplierRepository.GetByIdWithSpec(spec);
            if(supplier == null) { return NotFound(new CodeErrorResponse(404)); }
            return _mapper.Map<Supplier, SupplierDto>(supplier); 
        } 

    }
}
