using Consume_DemoAPI_FrontEnd.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Newtonsoft.Json;
using System.Text;
using Microsoft.VisualBasic;

namespace Consume_DemoAPI_FrontEnd.Controllers
{
    public class CityController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        Uri baseAddress = new Uri("http://localhost:5223/api");

        public CityController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = baseAddress;
        }

        #region GetAllCities
        public IActionResult CityTable()
        {
            List<CountryModel> countryModels = new List<CountryModel>();
            HttpResponseMessage response = _httpClient.GetAsync($"{_httpClient.BaseAddress}/City").Result;
            //var responce = await _httpClient.GetAsync("City");
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                var cities = JsonConvert.DeserializeObject<List<CityModel>>(data);
                return View(cities);
            }
            return View(new List<CityModel>());
        }
        #endregion

        #region CityAddEdit
        public async Task<IActionResult> CityAddEdit(int? CityID)
        {
            try
            {
                if (CityID.HasValue)
                {
                    LoadCountryList();
                    var responce = await _httpClient.GetAsync($"{_httpClient.BaseAddress}/City/{CityID}");
                    if (responce.IsSuccessStatusCode)
                    {
                        var data = await responce.Content.ReadAsStringAsync();
                        var city = JsonConvert.DeserializeObject<CityModel>(data);
                        //city.CityID = CityID.Value;
                        ViewBag.StateList = GetStateByCountryID(city.CountryID);
                        return View("CityForm", city);
                    }
                }
                LoadCountryList();
                return View("CityForm", new CityModel());
            }
            catch (Exception ex) 
            {
                return View(ex);
            }
        }
        #endregion

        #region CitySave
        [HttpPost]
        public async Task<IActionResult> CitySave(CityModel city)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var json = JsonConvert.SerializeObject(city);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    HttpResponseMessage responce;

                    if (city.CityID == 0)
                    {
                        responce = await _httpClient.PostAsync($"{_httpClient.BaseAddress}/City", content);
                    }
                    else
                    {
                        responce = await _httpClient.PutAsync($"{_httpClient.BaseAddress}/City", content);
                    }

                    if (responce.IsSuccessStatusCode)
                    {
                        return RedirectToAction("CityTable");
                    }
                }
                LoadCountryList();
                return View("CityForm", city);
            }
            catch (Exception ex) 
            {
                return View(ex);
            }
        }
        #endregion

        #region CityDelete
        public async Task<IActionResult> CityDelete(int CityID)
        {
            try
            {
                HttpResponseMessage responce = await _httpClient.DeleteAsync($"{_httpClient.BaseAddress}/City/{CityID}");
                if (responce.IsSuccessStatusCode)
                {
                    TempData["Message"] = "Record Delete SuccessFully.";
                }
                return RedirectToAction("CityTable");
            }
            catch(Exception ex)
            {
                return View(ex);
            }
        }
        #endregion

        #region LoadCountryList
        private void LoadCountryList()
        {
            HttpResponseMessage response = _httpClient.GetAsync($"{_httpClient.BaseAddress}/DropDown/CountryDropDown").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                var countries = JsonConvert.DeserializeObject<List<CountryDropDownModel>>(data);
                ViewBag.CountryList = countries;
            }
        }
        #endregion

        #region JsonGetStateByCountry
        [HttpPost]
        public JsonResult GetStateByCountry(int CountryID)
        {
            var states = GetStateByCountryID(CountryID);
            return Json(states);
        }
        #endregion

        #region GetStateByCountryID
        private List<StateDropDownModel> GetStateByCountryID(int CountryID)
        {
            HttpResponseMessage response = _httpClient.GetAsync($"{_httpClient.BaseAddress}/DropDown/StateDropDown/{CountryID}").Result;
            //var response = await _httpClient.GetAsync($"DropDown/{CountryID}");
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<List<StateDropDownModel>>(data);
            }
            return new List<StateDropDownModel>();
        }
        #endregion
    }
}
