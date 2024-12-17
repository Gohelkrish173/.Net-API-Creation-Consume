﻿using DemoWebAPI.Data;
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

        #region InsertCountry
        [HttpPost]
        public IActionResult InsertCountry(CountryModel cmodel)
        {
            bool Reflected = _countryRepository.InsertCountry(cmodel);
            return Ok(Reflected);
        }
        #endregion

        #region UpdateCountry
        [HttpPut]
        public IActionResult UpdateCountry(CountryModel countryModel)
        {
            bool Reflected = _countryRepository.UpdateCountry(countryModel);
            return Ok(Reflected);
        }
        #endregion

        #region DeleteCountry
        [HttpDelete("{CountryID:int}")]
        public IActionResult DeleteCountry(int CountryID)
        {
            bool Reflected = _countryRepository.DeleteCountry(CountryID);
            return Ok(Reflected);
        }
        #endregion

        #region GetCountryByPK
        [HttpGet("{CountryID:int}")]
        public IActionResult GetCountryByPK(int CountryID)
        {
            CountryModel cmodel = _countryRepository.GetCountryByPK(CountryID);
            return Ok(cmodel);
        }
        #endregion
    }
}
