using AutoMapper;
using ChallengeNET.Application.Dto;
using ChallengeNET.DataAccess.Entitys;

namespace ChallengeNET.WebApi
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Tarjeta, TarjetaDto>().ReverseMap();
            CreateMap<CreateCardDto, Tarjeta>();
            CreateMap<Balance, BalanceDto>()
                .ForMember(x => x.Tarjeta, s => s
                .MapFrom(d => d.Tarjeta)).ReverseMap().PreserveReferences();
            CreateMap<BalanceRequestDto, Balance>();
            CreateMap<Retiro, RetiroDto>()
                .ForMember(x => x.Tarjeta, d=> d.MapFrom(s => s.Tarjeta))
                .ForMember(x => x.Operacion, d=> d.MapFrom(s => s.Operacion)).ReverseMap();
            CreateMap<Operacion, OperacionDto>().ReverseMap()
                .ForMember(x => x.Tarjeta, s => s.MapFrom(d => d.Tarjeta))
                .PreserveReferences();
            CreateMap<OperacionRequestDto, Operacion>()
                .ForMember(x => x.cod_operacion, d => d
                .MapFrom(s => s.cod_operacion.ToString())).ReverseMap();
            CreateMap<RetiroRequestDto, Retiro>()
                .ForMember(x => x.monto_retiro, d => d
                .MapFrom(s => s.monto)).ReverseMap();
        }
    }
}
