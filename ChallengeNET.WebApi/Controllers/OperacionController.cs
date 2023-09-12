using ChallengeNET.Application.Dto;
using ChallengeNET.Application.Services.Operaciones;
using EjercicioPOO.Application.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.WebSockets;

namespace ChallengeNET.WebApi.Controllers
{
    [Route("api/operaciones")]
    [ApiController]
    public class OperacionController : ControllerBase
    {
        private readonly IOperacionService _operacionService;
        private readonly ILogger<OperacionController> _logger;

        public OperacionController(IOperacionService operacionService, ILogger<OperacionController> logger)
        {
            _operacionService = operacionService;
            _logger = logger;
        }

        [HttpGet]
        public List<OperacionDto> Get()
        {
            var response = _operacionService.GetAll();

            return response;
        }
        
        [HttpGet("with-card-number")]
        public List<OperacionDto> GetAllWithCardNumber(string nro_tarjeta)
        {
            if (!ModelState.IsValid)
            {
                throw new BadRequestException("nro_tarjeta is missing.");
            }
            var response = _operacionService.GetAllWithCardNumber(nro_tarjeta);

            return response;
        }

        [HttpGet("{id}")]
        public OperacionDto Get(int id)
        {
            if (id <= 0) 
            {
                throw new BadRequestException("id must be grater then zero");
            }
            var response = _operacionService.GetOperacion(id);

            return response;
        }

        [HttpPost]
        public IActionResult Post(OperacionRequestDto operacion)
        {
            _logger.LogDebug($"the request is: {JsonConvert.SerializeObject(operacion)}");
            if (!ModelState.IsValid)
            {
                throw new BadRequestException("Error in entry data.");
            }
            _operacionService.CreateOperacion(operacion);

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
            {
                throw new BadRequestException("id must be grater then zero.");
            }
            _operacionService.DeleteOperacion(id);

            return Ok();
        }
    }
}
