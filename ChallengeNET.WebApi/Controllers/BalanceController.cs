using ChallengeNET.Application.Dto;
using ChallengeNET.Application.Services.Balances;
using EjercicioPOO.Application.Exceptions;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ChallengeNET.WebApi.Controllers
{
    [Route("api/balance")]
    [ApiController]
    public class BalanceController : ControllerBase
    {
        private readonly IBalanceService _balanceService;
        public BalanceController(IBalanceService balanceService)
        {
            _balanceService = balanceService;
        }

        [HttpGet("{nro_tarjeta}")]
        public IActionResult Get(string nro_tarjeta)
        {
            if (!ModelState.IsValid)
            {
                throw new BadRequestException("nro_tarjeta is missing.");
            }
            var response = _balanceService.GetBalance(nro_tarjeta);

            return Ok(response);
        }

        [HttpPost]
        public IActionResult Post(BalanceRequestDto balance)
        {
            if (!ModelState.IsValid)
            {
                throw new BadRequestException("Error in the entry data."); 
            }
            _balanceService.CreateBalance(balance);

            return Ok();
        }

        [HttpPut("extracted-balance")]
        public IActionResult PutExtractedBalance(BalanceRequestDto balance)
        {
            if (!ModelState.IsValid) 
            {
                throw new BadRequestException("Error in the entry data.");
            }
            _balanceService.UpdateExtractedBalance(balance);

            return Ok();
        }
        
        [HttpPut("add-balance")]
        public IActionResult PutAddedBalance(BalanceRequestDto balance)
        {
            if (!ModelState.IsValid) 
            {
                throw new BadRequestException("Error in the entry data.");
            }
            _balanceService.UpdateAddedBalance(balance);

            return Ok();
        }

        [HttpDelete("{nro_tarjeta}")]
        public IActionResult Delete(string nro_tarjeta)
        {
            if (!ModelState.IsValid)
            {
                throw new BadRequestException("nro_tarjeta is missing.");
            }
            _balanceService.DeleteBalance(nro_tarjeta);

            return Ok();
        }
    }
}
