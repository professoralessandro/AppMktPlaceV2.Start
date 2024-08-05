#region IMPORTS
using AppMktPlaceV2.Start.Application.Dtos.Group;
using AppMktPlaceV2.Start.Application.Dtos.GroupResource;
using AppMktPlaceV2.Start.Application.Dtos.Resource;
using AppMktPlaceV2.Start.Application.Dtos.User.Response;
using AppMktPlaceV2.Start.Application.Models.Dto.User;
using AppMktPlaceV2.Start.Domain.Entities;
using AutoMapper;
#endregion

namespace AppMktPlaceV2.Start.Infrastructure.Mappers
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
