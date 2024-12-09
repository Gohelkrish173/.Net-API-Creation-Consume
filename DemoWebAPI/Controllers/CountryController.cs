using DemoWebAPI.Data;
using DemoWebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DemoWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly CountryRepository _countryRepository;
        
        public CountryController(CountryRepository countryRepository) 
        {
            _countryRepository = countryRepository;
        }

        #region GetAllCountries
        [HttpGet]
        public IActionResult Index()
        {
            var contries = _countryRepository.GetAllCountries();
            return Ok(contries);
        }
        #endregion

        #region InsertCity
        [HttpPost]
        public IActionResult InsertCountry(CountryModel cmodel)
        {
            bool Reflected = _countryRepository.InsertCountry(cmodel);
            return Ok(Reflected);
        }
        #endregion

        #region UpdateCity
        [HttpPut]
        public IActionResult UpdateCountry(CountryModel countryModel)
        {
            bool Reflected = _countryRepository.UpdateCountry(countryModel);
            return Ok(Reflected);
        }
        #endregion

        #region DeleteCountry
        [HttpDelete]
        public IActionResult DeleteCountry(int CountryID)
        {
            bool Reflected = _countryRepository.DeleteCountry(CountryID);
            return Ok(Reflected);
        }
        #endregion
    }
}
