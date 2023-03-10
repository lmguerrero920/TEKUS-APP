using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tekus.Core.Entities;
using Tekus.Core.Interfaces;

namespace Tekus.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : Controller
    {

        private readonly IGenericRepository<Country> _countryRepository;

        public CountriesController(IGenericRepository<Country>
            countriesRepository)
        {
            _countryRepository = countriesRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Country>>>
            GetCountriesAll()
        {
            return Ok(await _countryRepository.GetAllAsync());

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Country>> GetCountriesById(int id)
        {
            return await _countryRepository.GetByIdAsync(id);
        }





    }
}
