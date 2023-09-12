using ChallengeNET.Application.Dto;
using ChallengeNET.Application.Enum;
using ChallengeNET.Client.Dto;
using ChallengeNET.Client.Models;
using ChallengeNET.Shared.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text;

namespace ChallengeNET.Client.Controllers
{
    public class OperacionController : Controller
    {
        private readonly ApiOptions _options;
        public OperacionController(IOptions<ApiOptions> options)
        {
            _options = options.Value;
        }
        public async Task<IActionResult> Operaciones()
        {
            var httpClient = new HttpClient();
            try
            {
                var nro_tarjeta = Request.Cookies["nro_tarjeta"].ToString();
                var response = await httpClient.GetAsync(_options.Operaciones + "with-card-number?nro_tarjeta=" + nro_tarjeta);
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();
                var operaciones = JsonConvert.DeserializeObject<List<OperacionesVM>>(json);

                return View(operaciones);
            }
            catch (Exception ex)
            {
                TempData["Mensaje"] = ex.Message;
                return StatusCode(500, ex.Message);
            }
        }

        public async Task<IActionResult> Balance()
        {
            var httpClient = new HttpClient();
            try
            {
                var nro_tarjeta = Request.Cookies["nro_tarjeta"].ToString();
                var response = await httpClient.GetAsync(_options.Balance + nro_tarjeta);
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();
                var balance = JsonConvert.DeserializeObject<BalanceVM>(json);
                var operacionRequest = new OperacionRequestDto
                {
                    nro_tarjeta = nro_tarjeta,
                    fecha_operacion = DateTime.Now,
                    cod_operacion = CodigoOperacionEnum.Balance
                };

                var jsonOperacion = JsonConvert.SerializeObject(operacionRequest);
                var content = new StringContent(jsonOperacion, Encoding.UTF8, "application/json");

                var grabarOperacion = await httpClient.PostAsync(_options.Operaciones, content);
                grabarOperacion.EnsureSuccessStatusCode();

                return View(balance);
            }
            catch (Exception ex)
            {
                TempData["Mensaje"] = ex.Message;
                return Redirect("/Home/Error");
            }
        }

        public async Task<IActionResult> ValidateMontoRetiro(string monto_retiro) 
        {
            var httpClient = new HttpClient();
            try
            {
                var nro_tarjeta = Request.Cookies["nro_tarjeta"].ToString();
                var response = await httpClient.GetAsync(_options.Balance + nro_tarjeta);
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();
                var balance = JsonConvert.DeserializeObject<BalanceVM>(json);
                if (balance.saldo < double.Parse(monto_retiro))
                {
                    throw new Exception(balance.saldo.ToString());
                }

                return Content(json);
            }
            catch (Exception ex)
            {
                TempData["Mensaje"] = ex.Message;
                return StatusCode(500, ex.Message);
            }
        }
        public async Task<IActionResult> RetiroDinero(string monto_retiro) 
        {
            var httpClient = new HttpClient();
            try
            {
                var nro_tarjeta = Request.Cookies["nro_tarjeta"].ToString();
                var requestRetiro = new RetiroRequestDto
                {
                    monto = double.Parse(monto_retiro),
                    nro_tarjeta = nro_tarjeta
                };
                var jsonRetiro = JsonConvert.SerializeObject(requestRetiro);
                var contentRetiro = new StringContent(jsonRetiro, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync(_options.Retiro, contentRetiro);
                response.EnsureSuccessStatusCode();

                var requestBalance = new BalanceRequestDto 
                {
                    nro_tarjeta = nro_tarjeta,
                    saldo = double.Parse(monto_retiro)
                };
                var jsonBalance = JsonConvert.SerializeObject(requestBalance);
                var contentBalance = new StringContent(jsonBalance, Encoding.UTF8, "application/json");

                var updateBalance = await httpClient.PutAsync(_options.Balance + "extracted-balance", contentBalance);
                updateBalance.EnsureSuccessStatusCode();

                return RedirectToAction("RetiroExitoso");
            }
            catch (Exception ex)
            {
                TempData["Mensaje"] = ex.Message;
                return StatusCode(500, ex.Message);
            }
        }
        public IActionResult Retiro()
        {
            return View();
        }

        public IActionResult RetiroExitoso() 
        {
            return View();
        }
    }
}
