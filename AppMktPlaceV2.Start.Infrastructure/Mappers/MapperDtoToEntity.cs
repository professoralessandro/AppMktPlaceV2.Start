#region IMPORT
using AppMktPlaceV2.Start.Application.Dtos.Group;
using AppMktPlaceV2.Start.Application.Dtos.GroupResource;
using AppMktPlaceV2.Start.Application.Dtos.Log;
using AppMktPlaceV2.Start.Application.Dtos.Resource;
using AppMktPlaceV2.Start.Application.Dtos.User.Response;
using AppMktPlaceV2.Start.Application.Models.Dto.User;
using AppMktPlaceV2.Start.Domain.Entities;
using AutoMapper;
#endregion

namespace AppMktPlaceV2.Start.Infrastructure.Mappers
{
    public class MapperDtoToEntity : Profile
    {
        public MapperDtoToEntity()
        {
            #region USER
            CreateMap<UserDto, Usuario>()
            .ForMember(memb => memb.UsuarioId, m => m.MapFrom(a => a.Identifier))
            .ForMember(memb => memb.Senha, m => m.MapFrom(a => a.Password))
            .ForMember(memb => memb.Login, m => m.MapFrom(a => a.UserName))
            .ForMember(memb => memb.TipoDocumentoId, m => m.MapFrom(a => a.TipoDocumento))
            .ReverseMap();

            CreateMap<UserResponseDto, Usuario>()
            .ForMember(memb => memb.UsuarioId, m => m.MapFrom(a => a.Identifier))
            .ReverseMap();

            CreateMap<UserResponseDto, UserDto>()
            .ReverseMap();
            #endregion USER
        }
    }
}
