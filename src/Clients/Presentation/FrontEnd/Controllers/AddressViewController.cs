using FrontEnd.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace FrontEnd.Controllers
{
    public class AddressViewController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl = "https://localhost:5001/api/v1";

        public AddressViewController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        private void AddAuthorizationHeader()
        {
            var token = HttpContext.Session.GetString("AuthToken");
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }

        public async Task<IActionResult> Index(int customerId)
        {
            AddAuthorizationHeader();
            var response = await _httpClient.GetAsync($"{_apiBaseUrl}/places/{customerId}");
            if (!response.IsSuccessStatusCode) return View(new List<AddressModel>());

            var content = await response.Content.ReadAsStringAsync();
            var addresses = JsonConvert.DeserializeObject<List<AddressModel>>(content);
            return View(addresses);
        }

        public async Task<IActionResult> Details(int addressId)
        {
            AddAuthorizationHeader();
            var response = await _httpClient.GetAsync($"{_apiBaseUrl}/place/{addressId}");
            if (!response.IsSuccessStatusCode) return NotFound();

            var content = await response.Content.ReadAsStringAsync();
            var address = JsonConvert.DeserializeObject<AddressModel>(content);
            return View(address);
        }

        public IActionResult Create(int customerId)
        {
            return View(new AddressModel { CustomerId = customerId });
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddressModel address)
        {
            AddAuthorizationHeader();
            var json = JsonConvert.SerializeObject(address);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{_apiBaseUrl}/place/{address.CustomerId}", content);

            if (!response.IsSuccessStatusCode) return View(address);
            return RedirectToAction("Index", new { customerId = address.CustomerId });
        }

        public async Task<IActionResult> Edit(int addressId)
        {
            AddAuthorizationHeader();
            var response = await _httpClient.GetAsync($"{_apiBaseUrl}/place/{addressId}");
            if (!response.IsSuccessStatusCode) return NotFound();

            var content = await response.Content.ReadAsStringAsync();
            var address = JsonConvert.DeserializeObject<AddressModel>(content);
            return View(address);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int addressId, AddressModel address)
        {
            AddAuthorizationHeader();
            var json = JsonConvert.SerializeObject(address);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{_apiBaseUrl}/place/{address.CustomerId}/{addressId}", content);

            if (!response.IsSuccessStatusCode) return View(address);
            return RedirectToAction("Index", new { customerId = address.CustomerId });
        }

        public async Task<IActionResult> Delete(int addressId, int customerId)
        {
            AddAuthorizationHeader();
            var response = await _httpClient.GetAsync($"{_apiBaseUrl}/place/{addressId}");
            if (!response.IsSuccessStatusCode) return NotFound();

            var content = await response.Content.ReadAsStringAsync();
            var address = JsonConvert.DeserializeObject<AddressModel>(content);
            return View(address);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int addressId, int customerId)
        {
            AddAuthorizationHeader();
            var response = await _httpClient.DeleteAsync($"{_apiBaseUrl}/place/{addressId}");

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Erro ao deletar o endereço.");
                return View();
            }

            TempData["CustomerId"] = customerId;
            return RedirectToAction("Index", "Customer", new { customerId });
        }

    }
}