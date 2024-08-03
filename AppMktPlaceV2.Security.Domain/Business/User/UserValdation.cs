#region ATRIBUTTES
using AppMktPlaceV2.Security.Domain.Interfaces.Services.User;
using AppMktPlaceV2.Security.Application.Helper.Static.Generic;
using AppMktPlaceV2.Security.Application.Models.Dto.User;
using AppMktPlaceV2.Security.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using AppMktPlaceV2.Security.Application.Dtos.User.Request;
#endregion

namespace AppMktPlaceV2.Security.Business.Business.User
{
    public static class UserValdation
    {
        #region INSERT
        public static async Task<string> ValidInsert(this UserDto model, IUserService _service)
        {
            var _allUsers = await _service.GetAllAsync();

            string validation = "";

            if (!string.IsNullOrEmpty(model.Nome))
            {
                model.Nome = model.Nome.RemoveInjections();
                if (model.Nome.Length < 3)
                {
                    validation += "Nome contem menos de três caracteres\n";
                }
            }

            if (!string.IsNullOrEmpty(model.Email))
            {
                if (model.Email.Length < 3)
                {
                    validation += "Email contem menos de três caracteres\n";
                }

                if (!model.Email.Contains("@"))
                {
                    validation += "Email nao contem @\n";
                }
            }

            if (model.UsuarioInclusaoId == Guid.Empty)
            {
                validation += "Identificação do usuario que incluiu e invalido\n";
            }

            if (_allUsers.Any(c => c.Email.ToUpper().Trim().Equals(model.Email.ToUpper().Trim())))
            {
                validation += "Ja existe este usuario com este email cadastrado na base de dados.\n";
            }

            if (_allUsers.Any(c => c.NmrDocumento.ToUpper().Trim().Equals(model.NmrDocumento.ToUpper().Trim())))
            {
                validation += "Ja existe este usuario com este numero de documento cadastrado na base de dados.\n";
            }

            return validation;
        }
        #endregion

        #region UPDATE
        public static string ValidUpdate(this UserUpdateRequest model)
        {
            string validation = "";

            if (model.Identifier == Guid.Empty)
            {
                validation += "Identificação do avaliação invalido\n";
            }

            if (!string.IsNullOrEmpty(model.Nome))
            {
                if (model.Nome.Length < 3)
                {
                    validation += "Nome contem menos de três caracteres\n";
                }
            }

            return validation;
        }
        #endregion

        #region DELETE
        public static string ValidDelete(this Usuario model)
        {
            try
            {
                string validation = "";

                if (model.UsuarioId == Guid.Empty)
                {
                    validation += "Identificação do avaliação invalido\n";
                }

                return validation;
            }
            catch (ValidationException ex)
            {
                throw new ValidationException(ex.Message);
            }
        }
        #endregion
    }
}
