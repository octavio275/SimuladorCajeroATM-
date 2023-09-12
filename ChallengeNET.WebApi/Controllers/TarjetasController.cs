using ChallengeNET.Application.Dto;
using ChallengeNET.Application.Services.TarjetaService;
using EjercicioPOO.Application.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChallengeNET.WebApi.Controllers
{
    [Route("api/tarjetas")]
    [ApiController]
    public class TarjetasController : ControllerBase
    {
        private readonly ITarjetaService _tarjetaService;
        public TarjetasController(ITarjetaService tarjetaService)
        {
            _tarjetaService = tarjetaService;
        }

        [HttpPost]
        public IActionResult Post(CreateCardDto tarjeta)
        {
            if (!ModelState.IsValid)
            {
                throw new BadRequestException("Error in entry data");
            }
            _tarjetaService.CreateCard(tarjeta);

            return Ok();
        }

        [Route("{nro_tarjeta}")]
        [HttpGet]
        public TarjetaDto GetWithNumber(string nro_tarjeta)
        {
            if (!ModelState.IsValid)
            {
                throw new BadRequestException("nro_tarjeta is missing.");
            }
            var response = _tarjetaService.GetCardWithNumber(nro_tarjeta);

            return response;
        }

        [Route("validate-cvv")]
        [HttpPost]
        public IActionResult ValidateCVV(ValidateCVVRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                throw new BadRequestException("Error in the entry data.");
            }
            _tarjetaService.ValidateCVV(request);

            return Ok();
        }

        [HttpGet]
        public List<TarjetaDto> GetAll()
        {
            var response = _tarjetaService.GetAll();

            return response;
        }

        [HttpPut]
        public IActionResult Put(CardValidDto tarjeta)
        {
            if (!ModelState.IsValid)
            {
                throw new BadRequestException("Error in entry data");
            }
            _tarjetaService.UpdateStateCard(tarjeta);

            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(string nro_tarjeta)
        {
            if (!ModelState.IsValid)
            {
                throw new BadRequestException("nro_tarjeta is missing.");
            }
            _tarjetaService.DeleteCard(nro_tarjeta);

            return Ok();
        }
    }
}
