using APIEscolaAuth1.Models;
using AutoMapper;

namespace APIEscolaAuth1.DTOs;

public class EntityToDTOMappingProfile : Profile
{
    public EntityToDTOMappingProfile()
    {
        CreateMap<AlunoDTO, Aluno>().ReverseMap();
        CreateMap<SalaDTO, Sala>().ReverseMap();
        CreateMap<TurmaDTO, Turma>().ReverseMap();
        CreateMap<UserDTO, User>().ReverseMap();
    }
}
