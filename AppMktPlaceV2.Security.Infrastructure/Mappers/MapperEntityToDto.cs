#region IMPORTS
using AppMktPlaceV2.Security.Application.Dtos.Group;
using AppMktPlaceV2.Security.Application.Dtos.GroupResource;
using AppMktPlaceV2.Security.Application.Dtos.Resource;
using AppMktPlaceV2.Security.Application.Dtos.User.Response;
using AppMktPlaceV2.Security.Application.Models.Dto.User;
using AppMktPlaceV2.Security.Domain.Entities;
using AutoMapper;
#endregion

namespace AppMktPlaceV2.Security.Infrastructure.Mappers
{
    public class MapperEntityToDto : Profile
    {
        public MapperEntityToDto()
        {
            #region USER
            CreateMap<Usuario, UserDto>()
            .ForMember(memb => memb.Identifier, m => m.MapFrom(a => a.UsuarioId))
            .ForMember(memb => memb.Password, m => m.MapFrom(a => a.Senha))
            .ForMember(memb => memb.UserName, m => m.MapFrom(a => a.Login))
            .ForMember(memb => memb.TipoDocumento, m => m.MapFrom(a => a.TipoDocumentoId));

            CreateMap<Usuario, UserResponseDto>()
            .ForMember(memb => memb.Identifier, m => m.MapFrom(a => a.UsuarioId));

            CreateMap<UserDto, UserResponseDto>();
            #endregion USER
        }
    }
}
