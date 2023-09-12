using AutoMapper;
using ChallengeNET.Application.Dto;
using ChallengeNET.Application.Services.Retiros;
using ChallengeNET.Application.Services.TarjetaService;
using ChallengeNET.DataAccess.Entitys;
using EjercicioPOO.Application.Exceptions;
using EjercicioPOO.Application.Services.Repository;
using Microsoft.EntityFrameworkCore;

namespace ChallengeNET.Application.Services.Operaciones
{
    public class OperacionService : IOperacionService
    {
        private readonly IGenericRepository<Operacion> _operacion;
        private readonly IMapper _mapper;
        private readonly ITarjetaService _tajetaService;
        private readonly IGenericRepository<Retiro> _retiro;
        private readonly IGenericRepository<Balance> _balance;

        public OperacionService(IGenericRepository<Operacion> operacion,
                                IMapper mapper,
                                ITarjetaService tajetaService,
                                IGenericRepository<Retiro> retiro,
                                IGenericRepository<Balance> balance)
        {
            _operacion = operacion;
            _mapper = mapper;
            _tajetaService = tajetaService;
            _retiro = retiro;
            _balance = balance;
        }

        public OperacionDto CreateOperacion(OperacionRequestDto operacion)
        {
            try
            {
                var entity = BuildOperacionEntity(operacion);
                _operacion.Insert(entity);
                _operacion.Save();
                var dto = _mapper.Map<OperacionDto>(entity);

                return dto;
            }
            catch (Exception ex)
            {
                throw new InternalErrorException(ex.Message);
            }
        }

        public OperacionDto GetOperacion(int operacion_id)
        {
            var entity = _operacion.GetAll().Include(x => x.Tarjeta).FirstOrDefault(x => x.operacion_id == operacion_id) ?? throw new NotFoundException("The entity cannot be found.");
            var dto = _mapper.Map<OperacionDto>(entity);

            return dto;
        }

        public List<OperacionDto> GetAll()
        {
            var entity = _operacion.GetAll().Include(x => x.Tarjeta).ThenInclude(x => x.Operaciones).Include(x => x.Tarjeta).ThenInclude(s => s.Balance).ToList() ?? throw new NotFoundException("The entity cannot be found.");
            var dto = _mapper.Map<List<OperacionDto>>(entity);

            return dto;
        }
        public List<OperacionDto> GetAllWithCardNumber(string nro_tarjeta)
        {
            var entity = _operacion.GetAll()
            .Include(x => x.Tarjeta)
            .Where(x => x.Tarjeta.nro_tarjeta.Equals(nro_tarjeta))
            .ToList() ?? throw new NotFoundException("The entity cannot be found.");
            var dto = _mapper.Map<List<OperacionDto>>(entity);
            foreach (var row in dto)
            {
                var balance = _balance.GetAll().FirstOrDefault(x => x.tarjeta_id == row.Tarjeta.tarjeta_id);
                var retiro = _retiro.GetAll().FirstOrDefault(x => x.operacion_id == row.operacion_id);
                var retiroDto = _mapper.Map<RetiroDto>(retiro);
                var balanceDto = _mapper.Map<BalanceDto>(balance);
                row.Balance = balanceDto;
                if (retiroDto != null)
                {
                    row.Retiros.Add(retiroDto);
                }
            }

            return dto;
        }

        public void DeleteOperacion(int id)
        {
            var entity = _operacion.GetById(id) ?? throw new NotFoundException("The entity cannot be found.");
            _operacion.Delete(entity.operacion_id);
            _operacion.Save();
        }

        private Operacion BuildOperacionEntity(OperacionRequestDto operacion)
        {
            var tarjeta = _tajetaService.GetCardWithNumber(operacion.nro_tarjeta);
            var operacionEntity = _mapper.Map<Operacion>(operacion);
            operacionEntity.tarjeta_id = tarjeta.tarjeta_id;

            return operacionEntity;
        }
    }
}
