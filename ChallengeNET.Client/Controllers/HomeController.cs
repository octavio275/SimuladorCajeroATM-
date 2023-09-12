using ChallengeNET.Application.Dto;
using ChallengeNET.Client.Dto;
using ChallengeNET.Client.Models;
using ChallengeNET.Shared.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;

namespace ChallengeNET.Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApiOptions _options;

        public HomeController(ILogger<HomeController> logger, IOptions<ApiOptions> options)
        {
            _logger = logger;
            _options = options.Value;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult IngresaPinView()
        {
            return View();
        }

        public async Task<IActionResult> ValidateTarjeta(string nro_tarjeta)
        {
            var httpClient = new HttpClient();
            try
            {
                nro_tarjeta = nro_tarjeta.Replace("-", "");
                var response = await httpClient.GetAsync(_options.Tarjetas + nro_tarjeta);
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();
                var tarjeta = JsonConvert.DeserializeObject<TarjetaVM>(json);
                if (tarjeta.tarjeta_bloqueada)
                {
                    throw new Exception("Tarjeta bloqueada");
                }
                Response.Cookies.Append("nro_tarjeta", tarjeta.nro_tarjeta);
                return RedirectToAction("IngresaPinView", tarjeta);
            }
            catch (Exception ex)
            {
                TempData["Mensaje"] = ex.Message;
                return StatusCode(500, ex.Message);
            }
        }

        public async Task<IActionResult> ValidateCVV(string pin_tarjeta)
        {
            var httpClient = new HttpClient();
            try
            {
                var nro_tarjeta = Request.Cookies["nro_tarjeta"].ToString();
                var body = new ValidateCVVRequestDto{ nro_tarjeta = nro_tarjeta, pin_tarjeta = pin_tarjeta };
                var json = JsonConvert.SerializeObject(body);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync(_options.Tarjetas + "validate-cvv", content);
                response.EnsureSuccessStatusCode();

                return RedirectToAction("Operaciones", "Operacion");
            }
            catch (Exception ex)
            {
                TempData["Mensaje"] = ex.Message;
                return StatusCode(500, ex.Message);
            }
        }
        public async Task<IActionResult> BloquearTarjeta()
        {
            var httpClient = new HttpClient();
            try
            {
                var nro_tarjeta = Request.Cookies["nro_tarjeta"].ToString();
                var body = new BloquearTarjetaRequestDto{ nro_tarjeta = nro_tarjeta, bloquear = true };
                var json = JsonConvert.SerializeObject(body);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await httpClient.PutAsync(_options.Tarjetas, content);
                response.EnsureSuccessStatusCode();

                return View("TarjetaBloqueada");
            }
            catch (Exception ex)
            {
                TempData["Mensaje"] = ex.Message;
                return StatusCode(500, ex.Message);
            }
        }

        public IActionResult TarjetaBloqueada() 
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Error(ErrorViewModel error)
        {
            error.ErrorMessage = TempData["Mensaje"].ToString();
            return View(error);
        }
    }
}