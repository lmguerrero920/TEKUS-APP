using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
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
        public async Task<ActionResult<Pagination<SupplierDto>>> GetSuppliers([FromQuery] SupplierSpecificationParams supplierParams)
        {
            SupplierWithServicesSpecification spec = new SupplierWithServicesSpecification(supplierParams);
            System.Collections.Generic.IReadOnlyList<Supplier> supplier = await _supplierRepository.GetAllWithSpec(spec);
            SupplierForCountSpecification specCount = new SupplierForCountSpecification(supplierParams);
            int totalSupplier = await _supplierRepository.CountAsync(specCount);
            decimal rounded = Math.Ceiling(Convert.ToDecimal(totalSupplier / supplierParams.PageSize));
            int totalPages = Convert.ToInt32(rounded);
            IReadOnlyList<SupplierDto> data = _mapper.Map<IReadOnlyList<Supplier>,IReadOnlyList<SupplierDto>> (supplier);
            return Ok(new Pagination<SupplierDto>
            {
                Count = totalSupplier,
                Data = data,
                PageCount  = totalPages,
                PageIndex = supplierParams.PageIndex,
                PageSize = supplierParams.PageSize
            }); 
        }

        /// <summary>
        /// spec : Debe incluir la Logica de la condicion de la consulta y tambien las relaciones entre las entidades,la relacion entre Servicios y Paises
        /// </summary>
        [HttpGet("{id}")] 
        public async Task<ActionResult<SupplierDto>> GetSupplier(int id)
        {
            SupplierWithServicesSpecification spec = new SupplierWithServicesSpecification(id);
            Supplier supplier = await _supplierRepository.GetByIdWithSpec(spec);
            if(supplier == null) { return NotFound(
                new CodeErrorResponse(404,"No existe el proveedor")); }
          
            return _mapper.Map<Supplier, SupplierDto>(supplier); 
         
        }

        [HttpPost]
        public async Task<ActionResult<Supplier>> Post(Supplier supplier)
        {
           var result=  await _supplierRepository.Add(supplier);
            if (result == 0)
            {
                throw new Exception("No se inserto el producto");
            }
            return Ok(supplier);
        }


        [HttpPut("{id}")]
        /// <summary>
        /// se recibe por parametro el Id Proveedor y se genera actualización si coincide con el dato en BD
        /// </summary>
        public async Task<ActionResult<Supplier>> Put(int id,Supplier supplier)
        {
            supplier.Id= id;
            var result = await _supplierRepository.Update(supplier);
           
            if (result == 0)
            {
                throw new Exception("No se Actualizo el producto");
            }
            return Ok(supplier);
        }

    }
}
