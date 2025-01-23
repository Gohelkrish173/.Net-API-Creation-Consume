using Consume_DemoAPI_FrontEnd.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace Consume_DemoAPI_FrontEnd.Controllers
{
    public class CountryController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        Uri baseAddress = new Uri("http://localhost:5000/api");

        public CountryController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = baseAddress;
        }

        #region GetAllStates
        public IActionResult CountryTable()
        {
            try
            {
                HttpResponseMessage response = _httpClient.GetAsync($"{_httpClient.BaseAddress}/Country").Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    var countries = JsonConvert.DeserializeObject<List<CountryModel>>(data);
                    return View(countries);
                }
                return View(new List<CountryModel>());
            }
            catch (Exception ex)
            {
                return View(ex);
            }
        }
        #endregion

        #region CountryDelete
        public IActionResult CountryDelete(int CountryID)
        {
            try
            {
                HttpResponseMessage Response = _httpClient.DeleteAsync($"{_httpClient.BaseAddress}/Country/{CountryID}").Result;
                if (Response.IsSuccessStatusCode)
                {
                    TempData["Message"] = "Record Delete Successfully.";
                }
                return RedirectToAction("CountryTable");
            }
            catch (Exception ex)
            {
                return View(ex);
            }
        }
        #endregion

        #region CountryAddEdit
        public async Task<IActionResult> CountryAddEdit(int? CountryID)
        {
            try
            {
                if (CountryID.HasValue)
                {
                    var response = await _httpClient.GetAsync($"{_httpClient.BaseAddress}/Country/{CountryID}");

                    if (response.IsSuccessStatusCode)
                    {
                        var data = await response.Content.ReadAsStringAsync();
                        var countries = JsonConvert.DeserializeObject<CountryModel>(data);

                        return View("CountryForm", countries);
                    }
                }
                return View("CountryForm", new CountryModel());
            }
            catch (Exception ex)
            {
                return View(ex);
            }
        }
        #endregion

        #region CountrySave
        [HttpPost]
        public async Task<IActionResult> CountrySave(CountryModel cmodel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var json = JsonConvert.SerializeObject(cmodel);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    HttpResponseMessage response;

                    if (cmodel.CountryID == 0)
                    {
                        response = await _httpClient.PostAsync($"{_httpClient.BaseAddress}/Country", content);
                    }
                    else
                    {
                        response = await _httpClient.PutAsync($"{_httpClient.BaseAddress}/Country", content);
                    }

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("CountryTable");
                    }
                }
                return View("CountryTable", new StateModel());
            }
            catch (Exception ex)
            {
                return View(ex);
            }
        }
        #endregion
    }
}
