using ChallengeNET.Application.Dto;
using ChallengeNET.Application.Services.Retiros;
using EjercicioPOO.Application.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace ChallengeNET.WebApi.Controllers
{
    [Route("api/retiro")]
    [ApiController]
    public class RetiroController : ControllerBase
    {
        private readonly IRetiroService _retiroService;

        public RetiroController(IRetiroService retiroService)
        {
            _retiroService = retiroService;
        }

        [HttpGet]
        public List<RetiroDto> GetAll()
        {
            var response = _retiroService.GetAll();
            return response;
        }

        [HttpGet("{id}")]
        public RetiroDto Get(int id)
        {
            if (id <= 0) 
            {
                throw new BadRequestException("id must be greater then zero");
            }
            var response = _retiroService.GetRetiro(id);

            return response;
        }

        [HttpPost]
        public IActionResult Post(RetiroRequestDto retiro)
        {
            if (!ModelState.IsValid)
            {
                throw new BadRequestException("Error in the entry data.");
            }
            _retiroService.CreateRetiro(retiro);

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
            {
                throw new BadRequestException("id must be greater then zero.");
            }
            _retiroService.DeleteRetiro(id);

            return Ok();
        }
    }
}
