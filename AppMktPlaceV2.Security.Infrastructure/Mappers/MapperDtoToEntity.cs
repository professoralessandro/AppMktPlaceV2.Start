#region IMPORT
using AppMktPlaceV2.Security.Application.Dtos.Group;
using AppMktPlaceV2.Security.Application.Dtos.GroupResource;
using AppMktPlaceV2.Security.Application.Dtos.Log;
using AppMktPlaceV2.Security.Application.Dtos.Resource;
using AppMktPlaceV2.Security.Application.Dtos.User.Response;
using AppMktPlaceV2.Security.Application.Models.Dto.User;
using AppMktPlaceV2.Security.Domain.Entities;
using AutoMapper;
#endregion

namespace AppMktPlaceV2.Security.Infrastructure.Mappers
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
