using Domain.DTOs;
using FrontEnd.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace FrontEnd.Controllers
{
    public class AuthViewController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl;
        private readonly ILogger<AuthViewController> _logger;

        public AuthViewController(IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<AuthViewController> logger)
        {
            _httpClient = httpClientFactory.CreateClient();
            _apiBaseUrl = configuration["ApiSettings:BaseUrl"];
            _logger = logger;
        }

        public IActionResult Login()
        {
            ViewData["Layout"] = "_LoginLayout";
            _logger.LogInformation("Ação Login chamada.");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserModel userModel)
        {
            _logger.LogInformation("Tentativa de login com o usuário: {Username}", userModel.Login);
            var json = JsonConvert.SerializeObject(userModel);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{_apiBaseUrl}/auth", content);

            if (response.IsSuccessStatusCode)
            {
                var authResponse = await response.Content.ReadAsStringAsync();
                var tokenData = JsonConvert.DeserializeObject<AuthResponseDto>(authResponse);

                HttpContext.Session.SetString("AuthToken", tokenData.Token);
                HttpContext.Session.SetInt32("CustomerId", tokenData.CustomerId);

                HttpContext.Session.SetString("UserEmail", userModel.Login);

                var customerId = tokenData.CustomerId;

                if (customerId > 0)
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenData.Token);
                    var customerResponse = await _httpClient.GetAsync($"{_apiBaseUrl}/customer/{customerId}");

                    if (customerResponse.IsSuccessStatusCode)
                    {
                        var customerContent = await customerResponse.Content.ReadAsStringAsync();
                        var customer = JsonConvert.DeserializeObject<CustomerModel>(customerContent);

                        TempData["SuccessMessage"] = "Login realizado com sucesso!";
                        return RedirectToAction("Details", "Customer", new { id = customer.Id });
                    }
                }
            }

            TempData["ErrorMessage"] = "Login ou senha inválidos.";
            _logger.LogWarning("Login ou senha inválidos para o usuário: {Username}", userModel.Login);
            return View(userModel);
        }
    }
}
