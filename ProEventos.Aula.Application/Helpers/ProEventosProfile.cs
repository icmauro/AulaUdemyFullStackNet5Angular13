using AutoMapper;
using ProEventos.Aula.Domain.Models;
using ProEventos.Aula.Application.Dtos;

namespace ProEventos.Aula.Application.Helpers
{
    public class ProEventosProfile: Profile
    {
        public ProEventosProfile()
        {
            CreateMap<Evento, EventoDto>().ReverseMap();
            CreateMap<Lote, LoteDto>().ReverseMap();
            CreateMap<Palestrante, PalestranteDto>().ReverseMap();
            CreateMap<RedeSocial, RedeSocialDto>().ReverseMap();
        }
    }
}
