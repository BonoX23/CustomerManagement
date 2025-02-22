using FrontEnd.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace FrontEnd.Controllers
{
    public class CustomerController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl;

        public CustomerController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClient = httpClientFactory.CreateClient();
            _apiBaseUrl = configuration["ApiSettings:BaseUrl"] + "/customer";
        }

        private void AddAuthorizationHeader()
        {
            var token = HttpContext.Session.GetString("AuthToken");
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }

        public async Task<IActionResult> Index()
        {
            AddAuthorizationHeader();
            var response = await _httpClient.GetAsync($"{_apiBaseUrl}");
            if (!response.IsSuccessStatusCode) return View(new List<CustomerModel>());

            var content = await response.Content.ReadAsStringAsync();
            var customers = JsonConvert.DeserializeObject<List<CustomerModel>>(content);
            return View(customers);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            AddAuthorizationHeader();
            var response = await _httpClient.GetAsync($"{_apiBaseUrl}/{id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var customer = JsonConvert.DeserializeObject<CustomerModel>(content);
                return View(customer);
            }

            ViewData["ErrorMessage"] = "Cliente não encontrado.";
            return NotFound();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CustomerModel customer)
        {
            AddAuthorizationHeader();
            var json = JsonConvert.SerializeObject(customer);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{_apiBaseUrl}", content);

            if (!response.IsSuccessStatusCode) return View(customer);

            var responseContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Resposta da API: {responseContent}");

            var responseObject = JsonConvert.DeserializeObject<Dictionary<string, string>>(responseContent);

            if (responseObject != null && responseObject.ContainsKey("item1"))
            {
                var mensagem = responseObject["item1"];
                TempData["SenhaGerada"] = mensagem;
            }

            return RedirectToAction("SenhaGerada");
        }

        public IActionResult SenhaGerada()
        {
            return View();
        }

        public async Task<IActionResult> Edit(int id)
        {
            AddAuthorizationHeader();
            var response = await _httpClient.GetAsync($"{_apiBaseUrl}/{id}");
            if (!response.IsSuccessStatusCode) return NotFound();

            var content = await response.Content.ReadAsStringAsync();
            var customer = JsonConvert.DeserializeObject<CustomerModel>(content);
            return View(customer);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, CustomerModel customer)
        {
            AddAuthorizationHeader();
            var json = JsonConvert.SerializeObject(customer);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{_apiBaseUrl}/{id}", content);

            if (!response.IsSuccessStatusCode)
            {
                return View(customer);
            }

            return RedirectToAction("Details", new { id = id });
        }

        public async Task<IActionResult> Delete(int id)
        {
            AddAuthorizationHeader();
            var response = await _httpClient.GetAsync($"{_apiBaseUrl}/{id}");
            if (!response.IsSuccessStatusCode) return NotFound();

            var content = await response.Content.ReadAsStringAsync();
            var customer = JsonConvert.DeserializeObject<CustomerModel>(content);
            return View(customer);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            AddAuthorizationHeader();
            var response = await _httpClient.DeleteAsync($"{_apiBaseUrl}/{id}");

            if (!response.IsSuccessStatusCode)
            {
                ViewData["ErrorMessage"] = "Erro ao excluir o cliente.";
                return View();
            }

            return RedirectToAction("Index", "Home");
        }

    }
}
