using DemoWebAPI.Data;
using DemoWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DropDownController : ControllerBase
    {
        private readonly DropDownRepository _dropDownRepository;

        public DropDownController(DropDownRepository dropDownRepository)
        {
            _dropDownRepository = dropDownRepository;
        }

        #region GetCountryDropDown
        [HttpGet("CountryDropDown")]
        public IActionResult GetCountryDropDown()
        {
            var CDDM = _dropDownRepository.GetCountryDropDown();
            return Ok(CDDM);
        }
        #endregion

        #region GetStateDropDownByCountry
        [HttpGet("StateDropDown/{CountryID:int}")]
        public IActionResult GetStateDropDownByCountry(int CountryID) 
        {
            var SDDM = _dropDownRepository.GetStateDropDownByCountry(CountryID);
            return Ok(SDDM);
        }
        #endregion
    }
}
