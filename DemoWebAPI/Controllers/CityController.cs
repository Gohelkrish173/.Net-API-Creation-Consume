using DemoWebAPI.Data;
using DemoWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Data;
using System.Text.Json;

namespace DemoWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly CityRepository _cityRepository;

        public CityController(CityRepository cityRepository) 
        { 
            _cityRepository = cityRepository;
        }

        #region GetAllCities
        [HttpGet]
        public IActionResult Index()
        {
            var cities = _cityRepository.GetAllCities();

            return Ok(cities);
        }
        #endregion

        #region InsertCity
        [HttpPost]
        public IActionResult InsertCity(CityModel cmodel)
        {
            bool Reflected = _cityRepository.InsertCity(cmodel);
            return Ok(Reflected);
        }
        #endregion

        #region UpdateCity
        [HttpPut]
        public IActionResult UpdateCity(CityModel cityModel)
        {
            bool Reflected = _cityRepository.UpdateCity(cityModel);
            return Ok(Reflected);
        }
        #endregion

        #region DeleteCity
        [HttpDelete]
        public IActionResult DeleteCity(int CityID)
        {
            bool Reflected = _cityRepository.DeleteCity(CityID);
            return Ok(Reflected);
        }
        #endregion
    }
}
