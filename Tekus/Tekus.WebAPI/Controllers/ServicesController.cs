using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tekus.BusinessLogic.Logic;
using Tekus.Core.Entities;
using Tekus.Core.Interfaces;
using Tekus.Core.Specifications;

namespace Tekus.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : Controller
    {
        private readonly IGenericRepository<Services> _servicesRepository;

        public ServicesController(IGenericRepository<Services> servicesRepository)
        {
            _servicesRepository = servicesRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Services>>>GetServicesAll()
        {
            var spec = new ServicesWithCountrySpecification();
            var supplier = await _servicesRepository.GetAllWithSpec(spec);
            return Ok(supplier);

        }

        /// <summary>
        /// spec : Debe incluir la Logica de la condicion de la consulta y tambien las relaciones entre las entidades,la relacion entre Servicios y Paises
        /// </summary>
        [HttpGet("{id}")]   
        public async Task<ActionResult<Services>> GetServiceById(int id)
        {
           
            var spec = new ServicesWithCountrySpecification(id);
            return await _servicesRepository.GetByIdWithSpec(spec);
        }


    }
}
