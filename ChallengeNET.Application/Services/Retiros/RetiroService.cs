using AutoMapper;
using ChallengeNET.Application.Dto;
using ChallengeNET.Application.Services.Balances;
using ChallengeNET.Application.Services.Operaciones;
using ChallengeNET.Application.Services.TarjetaService;
using ChallengeNET.DataAccess.Entitys;
using EjercicioPOO.Application.Exceptions;
using EjercicioPOO.Application.Services.Repository;
using Microsoft.EntityFrameworkCore;

namespace ChallengeNET.Application.Services.Retiros
{
    public class RetiroService : IRetiroService
    {
        private readonly IGenericRepository<Retiro> _retiro;
        private readonly ITarjetaService _tajetaService;
        private readonly IBalanceService _balanceService;
        private readonly IOperacionService _operacionService;
        private readonly IMapper _mapper;
        public RetiroService(IGenericRepository<Retiro> retiro,
                             IMapper mapper,
                             ITarjetaService tajetaService,
                             IBalanceService balanceService,
                             IOperacionService operacionService)
        {
            _retiro = retiro;
            _mapper = mapper;
            _tajetaService = tajetaService;
            _balanceService = balanceService;
            _operacionService = operacionService;
        }

        public void CreateRetiro(RetiroRequestDto retiro)
        {
            try
            {
                var tarjeta = _tajetaService.GetCardWithNumber(retiro.nro_tarjeta);
                var balance = _balanceService.GetBalance(retiro.nro_tarjeta);
                if (retiro.monto > balance.saldo)
                {
                    throw new InternalErrorException($"The current balance in account is '{balance.saldo}'. Please enter a valid amount.");
                }
                var operacion = CreateRetiroOperacion(retiro);

                var entity = _mapper.Map<Retiro>(retiro);
                entity.tarjeta_id = tarjeta.tarjeta_id;
                entity.operacion_id = operacion.operacion_id;
                entity.fecha_retiro = DateTime.UtcNow;
                _retiro.Insert(entity);
                _retiro.Save();

                UpdateBalance(retiro);
            }
            catch (Exception ex)
            {
                throw new InternalErrorException(ex.Message);
            }
        }
        public void DeleteRetiro(int id)
        {
            try
            {
                var retiro = _retiro.GetById(id);
                _retiro.Delete(retiro.retiro_id);
                _retiro.Save();
            }
            catch (Exception ex)
            {
                throw new InternalErrorException(ex.Message);
            }
        }

        public RetiroDto GetRetiro(int retiro_id) 
        {
            var retiro = _retiro.GetAll().Include(x => x.Tarjeta).Include(a => a.Operacion).FirstOrDefault(x => x.retiro_id == retiro_id) ?? throw new NotFoundException("The entity cannot be found.");
            var dto = _mapper.Map<RetiroDto>(retiro);

            return dto;
        }
        public List<RetiroDto> GetAll() 
        {
            var retiro = _retiro.GetAll().Include(x => x.Tarjeta).Include(a => a.Operacion).ToList() ?? throw new NotFoundException("The entity cannot be found.");
            var dto = _mapper.Map<List<RetiroDto>>(retiro);

            return dto;
        }

        private void UpdateBalance(RetiroRequestDto retiro)
        {
            var newBalance = new BalanceRequestDto
            {
                nro_tarjeta = retiro.nro_tarjeta,
                saldo = retiro.monto,
            };
            _balanceService.UpdateExtractedBalance(newBalance);
        }

        private OperacionDto CreateRetiroOperacion(RetiroRequestDto retiro)
        {
            var operacion = new OperacionRequestDto
            {
                cod_operacion = Enum.CodigoOperacionEnum.Retiro,
                nro_tarjeta = retiro.nro_tarjeta,
                fecha_operacion = DateTime.UtcNow
            };
            var dto = _operacionService.CreateOperacion(operacion);

            return dto;
        }
    }
}
