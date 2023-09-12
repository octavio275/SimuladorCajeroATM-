using AutoMapper;
using ChallengeNET.Application.Dto;
using ChallengeNET.Application.Services.TarjetaService;
using ChallengeNET.DataAccess.Entitys;
using EjercicioPOO.Application.Exceptions;
using EjercicioPOO.Application.Services.Repository;
using Microsoft.EntityFrameworkCore;

namespace ChallengeNET.Application.Services.Balances
{
    public class BalanceService : IBalanceService
    {
        private readonly IGenericRepository<Balance> _balanceRepository;
        private readonly IMapper _mapper;
        private readonly ITarjetaService _tajetaService;
        public BalanceService(IGenericRepository<Balance> balanceRepository, IMapper mapper, ITarjetaService tajetaService)
        {
            _balanceRepository = balanceRepository;
            _mapper = mapper;
            _tajetaService = tajetaService;
        }

        public void CreateBalance(BalanceRequestDto balanceDto)
        {
            try
            {
                var balanceCreated = _balanceRepository.GetAll().FirstOrDefault(x => x.Tarjeta.nro_tarjeta.Equals(balanceDto.nro_tarjeta));
                if (balanceCreated != null)
                {
                    throw new InternalErrorException("The balance was created before.");
                }
                var tarjeta = _tajetaService.GetCardWithNumber(balanceDto.nro_tarjeta);
                var entity = _mapper.Map<Balance>(balanceDto);
                entity.tarjeta_id = tarjeta.tarjeta_id;
                _balanceRepository.Insert(entity);
                _balanceRepository.Save();
            }
            catch (Exception ex)
            {

                throw new InternalErrorException(ex.Message);
            }
        }

        public void UpdateExtractedBalance(BalanceRequestDto balanceDto)
        {
            try
            {
                var balanceCreated = _balanceRepository.GetAll().FirstOrDefault(x => x.Tarjeta.nro_tarjeta.Equals(balanceDto.nro_tarjeta)) ?? throw new InternalErrorException("The balance not exist in db.");
                balanceCreated.saldo -= balanceDto.saldo;
                _balanceRepository.Update(balanceCreated);
                _balanceRepository.Save();
            }
            catch (Exception ex)
            {

                throw new InternalErrorException(ex.Message);
            }
        }
        public void UpdateAddedBalance(BalanceRequestDto balanceDto)
        {
            try
            {
                var balanceCreated = _balanceRepository.GetAll().FirstOrDefault(x => x.Tarjeta.nro_tarjeta.Equals(balanceDto.nro_tarjeta)) ?? throw new InternalErrorException("The balance not exist in db.");
                balanceCreated.saldo += balanceDto.saldo;
                _balanceRepository.Update(balanceCreated);
                _balanceRepository.Save();
            }
            catch (Exception ex)
            {

                throw new InternalErrorException(ex.Message);
            }
        }

        public BalanceDto GetBalance(string nro_tarjeta)
        {
            try
            {
                var balanceCreated = _balanceRepository.GetAll().Include(a => a.Tarjeta).FirstOrDefault(x => x.Tarjeta.nro_tarjeta.Equals(nro_tarjeta)) ?? throw new InternalErrorException("The balance not exist in db.");
                var dto = _mapper.Map<BalanceDto>(balanceCreated);
                return dto;
            }
            catch (Exception ex)
            {

                throw new InternalErrorException(ex.Message);
            }
        }

        public void DeleteBalance(string nro_tarjeta)
        {
            try
            {
                var balanceCreated = _balanceRepository.GetAll().FirstOrDefault(x => x.Tarjeta.nro_tarjeta.Equals(nro_tarjeta)) ?? throw new InternalErrorException("The balance not exist in db.");
                _balanceRepository.Delete(balanceCreated.balance_id);
                _balanceRepository.Save();
            }
            catch (Exception ex)
            {

                throw new InternalErrorException(ex.Message);
            }
        }
    }
}
