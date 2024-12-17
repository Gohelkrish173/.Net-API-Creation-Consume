using Microsoft.AspNetCore.Mvc;
using Consume_DemoAPI_FrontEnd.Models;
using System.Diagnostics;
using Newtonsoft.Json;
using System.Text;

namespace Consume_DemoAPI_FrontEnd.Controllers
{
    public class StateController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        Uri baseAddress = new Uri("http://localhost:5223/api");

        public StateController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = baseAddress;
        }

        #region GetAllStates
        public IActionResult StateTable()
        {
            try
            {
                HttpResponseMessage response = _httpClient.GetAsync($"{_httpClient.BaseAddress}/State").Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    var states = JsonConvert.DeserializeObject<List<StateModel>>(data);
                    return View(states);
                }
                return View(new List<StateModel>());
            }
            catch (Exception ex) 
            {
                return View(ex);
            }
        }
        #endregion

        #region StateDelete
        public IActionResult StateDelete(int StateID)
        {
            try
            {
                HttpResponseMessage Response = _httpClient.DeleteAsync($"{_httpClient.BaseAddress}/State/{StateID}").Result;
                if (Response.IsSuccessStatusCode)
                {
                    TempData["Message"] = "Record Delete Successfully.";
                }
                return RedirectToAction("StateTable");
            }
            catch (Exception ex) 
            {
                return View(ex);
            }
        }
        #endregion

        #region StateAddEdit
        public async Task<IActionResult> StateAddEdit(int? StateID)
        {
            try
            {
                if (StateID.HasValue)
                {
                    LoadCountry();
                    var response = await _httpClient.GetAsync($"{_httpClient.BaseAddress}/State/{StateID}");

                    if (response.IsSuccessStatusCode) 
                    {
                        var data = await response.Content.ReadAsStringAsync();
                        var state = JsonConvert.DeserializeObject<StateModel>(data);
                        
                        return View("StateForm",state);
                    }
                }
                LoadCountry();
                return View("StateForm",new StateModel());
            }
            catch (Exception ex)
            {
                return View(ex);
            }
        }
        #endregion

        #region StateSave
        [HttpPost]
        public async Task<IActionResult> StateSave(StateModel smodel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var json = JsonConvert.SerializeObject(smodel);
                    var content = new StringContent(json,Encoding.UTF8,"application/json");
                    HttpResponseMessage response;

                    if(smodel.StateID == 0)
                    {
                        response = await _httpClient.PostAsync($"{_httpClient.BaseAddress}/State", content);
                    }
                    else
                    {
                        response = await _httpClient.PutAsync($"{_httpClient.BaseAddress}/State", content);
                    }

                    if (response.IsSuccessStatusCode) 
                    {
                        return RedirectToAction("StateTable");
                    }
                }
                LoadCountry() ;
                return View("StateTable", new StateModel());            }
            catch(Exception ex)
            {
                return View(ex);
            }
        }
        #endregion

        #region LoadCountry
        private void LoadCountry()
        {
            HttpResponseMessage Country = _httpClient.GetAsync($"{_httpClient.BaseAddress}/DropDown").Result;

            if (Country.IsSuccessStatusCode)
            {
                var data = Country.Content.ReadAsStringAsync().Result;
                var loadCoutries = JsonConvert.DeserializeObject<List<CountryDropDownModel>>(data);
                ViewBag.CountryList = loadCoutries;
            }
        }
        #endregion
    }
}
