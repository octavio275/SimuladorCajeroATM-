using AutoMapper;
using ChallengeNET.Application.Dto;
using ChallengeNET.Application.Services.TarjetaService;
using ChallengeNET.DataAccess.Entitys;
using EjercicioPOO.Application.Exceptions;
using EjercicioPOO.Application.Services.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeNET.Application.Services.Tarjetas
{
    public class TarjetaService : ITarjetaService
    {
        private readonly IGenericRepository<Tarjeta> _tarjeta;
        private readonly IMapper _mapper;

        public TarjetaService(IGenericRepository<Tarjeta> tarjeta, IMapper mapper)
        {
            _tarjeta = tarjeta;
            _mapper = mapper;
        }

        public void CreateCard(CreateCardDto tarjeta)
        {
            try
            {
                var tarjetaExistente = _tarjeta.GetAll().FirstOrDefault(x => x.nro_tarjeta.Equals(tarjeta.nro_tarjeta));
                if (tarjetaExistente != null)
                {
                    throw new InternalErrorException("The entity has already exist");
                }

                var entity = _mapper.Map<Tarjeta>(tarjeta);
                _tarjeta.Insert(entity);
                _tarjeta.Save();
            }
            catch (Exception ex)
            {
                throw new InternalErrorException(ex.Message);
            }
        }
        public void UpdateStateCard(CardValidDto tarjeta)
        {
            try
            {
                var cardSaved = _tarjeta.GetAll().FirstOrDefault(x => x.nro_tarjeta.Equals(tarjeta.nro_tarjeta)) ?? throw new NotFoundException("The entity cannot be found");
                cardSaved.tarjeta_bloqueada = tarjeta.bloquear;
                _tarjeta.Update(cardSaved);
                _tarjeta.Save();
            }
            catch (Exception ex)
            {
                throw new InternalErrorException(ex.Message);
            }
        }

        public void DeleteCard(string nro_tarjeta)
        {
            try
            {
                var cardSaved = _tarjeta.GetAll().FirstOrDefault(x => x.nro_tarjeta.Equals(nro_tarjeta)) ?? throw new NotFoundException("The entity cannot be found");

                _tarjeta.Delete(cardSaved.tarjeta_id);
                _tarjeta.Save();
            }
            catch (Exception ex)
            {
                throw new InternalErrorException(ex.Message);
            }
        }
        public TarjetaDto GetCardWithNumber(string nro_tarjeta)
        {
            try
            {
                var entity = _tarjeta.GetAll().FirstOrDefault(x => x.nro_tarjeta.Equals(nro_tarjeta)) ?? throw new NotFoundException("The entity cannot be not found");
                var response = _mapper.Map<TarjetaDto>(entity);

                return response;
            }
            catch (Exception ex)
            {
                throw new InternalErrorException(ex.Message);
            }
        }
        public void ValidateCVV(ValidateCVVRequestDto request)
        {
            try
            {
                var entity = _tarjeta.GetAll().FirstOrDefault(x => x.nro_tarjeta.Equals(request.nro_tarjeta)) ?? throw new NotFoundException("The entity cannot be not found");
                if (!entity.pin_tarjeta.Equals(request.pin_tarjeta))
                {
                    throw new InternalErrorException("The cvv was no correct.");
                }
            }
            catch (Exception ex)
            {
                throw new InternalErrorException(ex.Message);
            }
        }
        public List<TarjetaDto> GetAll()
        {
            try
            {
                var entity = _tarjeta.GetAll().Include(a => a.Operaciones).Include(s => s.Retiros).ToList();
                if (entity == null || entity.Count == 0)
                {
                    throw new NotFoundException("The entity cannot be not found");
                }
                var response = _mapper.Map<List<TarjetaDto>>(entity);

                return response;
            }
            catch (Exception ex)
            {
                throw new InternalErrorException(ex.Message);
            }
        }
    }
}
