using AutoMapper;
using ProEventos.Aula.Domain.Models;
using ProEventos.Aula.Application.Dtos;
using ProEventos.Aula.Domain.Identity;
using ProEventos.Aula.Persistence.Models;

namespace ProEventos.Aula.Application.Helpers
{
    public class ProEventosProfile: Profile
    {
        public ProEventosProfile()
        {
            CreateMap<Evento, EventoDto>().ReverseMap();
            CreateMap<Lote, LoteDto>().ReverseMap();
            CreateMap<RedeSocial, RedeSocialDto>().ReverseMap();

            CreateMap<Palestrante, PalestranteDto>().ReverseMap();
            CreateMap<Palestrante, PalestranteAddDto>().ReverseMap();
            CreateMap<Palestrante, PalestranteUpdateDto>().ReverseMap();

            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, UserLoginDto>().ReverseMap();
            CreateMap<User, UserUpdateDto>().ReverseMap();

            CreateMap<PageParams, PageParamsDto>().ReverseMap();
            CreateMap<PageList<Evento>, PageListDto<EventoDto>>().ReverseMap();
        }
    }
}
