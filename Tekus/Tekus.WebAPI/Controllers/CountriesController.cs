using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tekus.BusinessLogic.Logic;
using Tekus.Core.Entities;
using Tekus.Core.Interfaces;

namespace Tekus.WebAPI.Controllers
{
   
    public class CountriesController : BaseApiController
    {

        private readonly IGenericRepository<Country> _countryRepository;

        public CountriesController(IGenericRepository<Country>
            countriesRepository)
        {
            _countryRepository = countriesRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Country>>>GetCountriesAll()
        {
            return Ok(await _countryRepository.GetAllAsync()); 
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Country>> GetCountriesById(int id)
        {
            return await _countryRepository.GetByIdAsync(id);
        }


        [HttpPost]
        /// <summary>
        /// Metodo encargado de insertar modelo de datos
        /// </summary>
        public async Task<ActionResult<Supplier>> Post(Country country)
        {
            int result = await _countryRepository.Add(country);
            if (result == 0)
            {
                throw new Exception("No se inserto el pais");
            }
            return Ok(country);
        }


        [HttpPut("{id}")]
        /// <summary>
        /// Metodo encargado de actualizar registro,
        /// se recibe por parametro el Id Proveedor y se genera actualización si coincide con el dato en BD
        /// </summary>
        public async Task<ActionResult<Supplier>> Put(int id, Country country)
        {
            country.Id = id;
            int result = await _countryRepository.Update(country);

            if (result == 0)
            {
                throw new Exception("No se Actualizo el pais");
            }
            return Ok(country);
        }


        [HttpDelete("{Id}")]
        /// <summary>
        /// Metodo encargado de actualizar registro,
        /// se recibe por parametro el Id Proveedor y se genera actualización si coincide con el dato en BD
        /// </summary>
        public async Task<ActionResult<Supplier>> Delete(int id)
        {
            int result = await _countryRepository.Delete(id); 
            if (result == 0)
            {
                throw new Exception("No se Borrar  el Pais");
            }
            return Ok(result);
        }




    }
}
