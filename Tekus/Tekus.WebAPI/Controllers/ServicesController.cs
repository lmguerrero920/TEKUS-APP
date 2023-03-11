using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tekus.BusinessLogic.Logic;
using Tekus.Core.Entities;
using Tekus.Core.Interfaces;
using Tekus.Core.Specifications;

namespace Tekus.WebAPI.Controllers
{
    
    public class ServicesController : BaseApiController
    {
        private readonly IGenericRepository<Services> _servicesRepository;

        public ServicesController(IGenericRepository<Services> servicesRepository)
        {
            _servicesRepository = servicesRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Services>>>GetServicesAll()
        {
            ServicesWithCountrySpecification spec = new ServicesWithCountrySpecification();
            IReadOnlyList<Services> supplier = await _servicesRepository.GetAllWithSpec(spec);
            return Ok(supplier);

        }

        /// <summary>
        /// spec : Debe incluir la Logica de la condicion de la consulta y tambien las relaciones entre las entidades,la relacion entre Servicios y Paises
        /// </summary>
        [HttpGet("{id}")]   
        public async Task<ActionResult<Services>> GetServiceById(int id)
        {
            ServicesWithCountrySpecification spec = new ServicesWithCountrySpecification(id);
            return await _servicesRepository.GetByIdWithSpec(spec);
        }

        [HttpPost]
        /// <summary>
        /// Metodo encargado de insertar modelo de datos
        /// </summary>
        public async Task<ActionResult<Services>> Post(Services services)
        {
            int result = await _servicesRepository.Add(services);
            if (result == 0)
            {
                throw new Exception("No se inserto el producto");
            }
            return Ok(services);
        }


        [HttpPut("{id}")]
        /// <summary>
        /// Metodo encargado de actualizar registro,
        /// se recibe por parametro el Id Servicio y se genera actualización si coincide con el dato en BD
        /// </summary>
        public async Task<ActionResult<Services>> Put(int id, Services services)
        {
            services.Id = id;
            int result = await _servicesRepository.Update(services);

            if (result == 0)
            {
                throw new Exception("No se Actualizo el producto");
            }
            return Ok(services);
        }

        [HttpDelete("{Id}")]
        /// <summary>
        /// Metodo encargado de actualizar registro,
        /// se recibe por parametro el Id Proveedor y se genera actualización si coincide con el dato en BD
        /// </summary>
        public async Task<ActionResult<Supplier>> Delete(int id)
        {
            int result = await _servicesRepository.Delete(id);
            if (result == 0)
            {
                throw new Exception("No se Borrar  el Pais");
            }
            return Ok(result);
        }


    }
}
