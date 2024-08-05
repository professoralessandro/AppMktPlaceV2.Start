#region REFERENCES
using AutoMapper;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AppMktPlaceV2.Start.Application.Helper.Static.Settings.Jtw;
using AppMktPlaceV2.Start.Domain.Entities;
using AppMktPlaceV2.Start.Domain.Interfaces.Services.User;
using AppMktPlaceV2.Start.Domain.Interfaces.Repository.User;
using AppMktPlaceV2.Start.Application.Models.Dto.User;
using AppMktPlaceV2.Start.Business.Business.User;
using AppMktPlaceV2.Start.Application.Helper.Static.Generic;
using AppMktPlaceV2.Start.Application.Enums;
using AppMktPlaceV2.Start.Application.Dtos.User.Request;
using AppMktPlaceV2.Start.Application.Models.Dtos;
using AppMktPlaceV2.Start.Application.Helper.Static.Hasher;
using AppMktPlaceV2.Start.Application.Models.Dto.Autenticate;
#endregion REFERENCES

namespace AppMktPlaceV2.Securityt.Domain.Servies.User
{
    public class UserService : IUserService
    {
        #region ATRIBUTTES
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        #endregion

        #region CONTRUCTORS
        public UserService(IUserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        #endregion

        #region FIND BY ID
        public async Task<UserDto> GetByIdAsync(Guid userId)
        {
            try
            {
                var user = await _repository.ReturnListWithParametersPaginated<UserDto>(id: userId, pageNumber: 1, rowspPage: 1);

                return user.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception("Houve um erro ao buscar o registro desejado!" + ex.Message);
            }
        }
        #endregion

        #region FIND USERS TO SELECT RETURN KEY AND VALUE
        public async Task<IEnumerable<object>> ReturnUsersToSelectAsync(string? param = null, int? pageNumber = null, int? rowspPage = null)
        {
            try
            {
                return await _repository.ReturnUsersToSelectAsync<object>(param, pageNumber, rowspPage);
            }
            catch (Exception ex)
            {
                throw new Exception("Houve um erro ao buscar o registro desejado!" + ex.Message);
            }
        }
        #endregion

        #region RETURN LIST WITH PARAMETERS PAGINATED
        public async Task<IEnumerable<UserDto>> ReturnListWithParametersPaginated(Guid? userId = null, Guid? id = null, string? userName = null, string? nome = null, string? nmrDocumento = null, string? email = null, bool? ativo = null, int? pageNumber = null, int? rowspPage = null)
        {
            try
            {
                return await _repository.ReturnListWithParametersPaginated<UserDto>(userId, id, userName, nome, nmrDocumento, email, ativo, pageNumber, rowspPage);
            }
            catch (Exception ex)
            {
                throw new Exception("Houve um erro ao buscar o registro desejado!" + ex.Message);
            }
        }
        #endregion

        #region GETALL
        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            try
            {
                var msgLog = "\n======================================================================================";
                msgLog += "\nBuscando por usuarios na base de dados";
                msgLog += "\n======================================================================================";
                Serilog.Log.Information(msgLog);

                var result = await _repository.GetAllAsync();

                return _mapper.Map<IEnumerable<Usuario>, IEnumerable<UserDto>>(result);
            }
            catch (Exception ex)
            {
                var msgLog = "\n======================================================================================";
                msgLog += "\nERROR: Não foi possível realizar a busca por registros: \n" + ex.Message;
                msgLog += "\n======================================================================================";
                Serilog.Log.Error(msgLog);
                throw new Exception("Não foi possível realizar a busca por registros: " + ex.Message);
            }
        }
        #endregion

        #region INSERT
        public async Task<UserDto> InsertAsync(UserDto model)
        {
            try
            {
                string validation = await model.ValidInsert(this);

                if (validation != string.Empty) throw new ValidationException("Erro na validacao do registro : " + validation);

                string email = model.Email;

                var resultDto = _mapper.Map<UserDto, Usuario>(model.TrasnformObjectPropValueToUpper());

                resultDto.Email = email.RemoveAllSpecialCaracterFromEmail();

                resultDto.GrupoId = GroupDbEnum.User;

                await _repository.AddAsync(resultDto);     

                return model;
            }
            catch (ValidationException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Houve um erro ao incluir registro: " + ex.Message);
            }
        }
        #endregion

        #region UPDATE
        public async Task<UserUpdateRequest> UpdateAsync(UserUpdateRequest model, Guid userUpdationgId)
        {
            try
            {
                if (model.Role.ToLower().Contains("Master"))
                {
                    var userUpdating = await _repository.GetByIdAsync(userUpdationgId);

                    if (userUpdating == null) throw new Exception("User requested not found");

                    if(userUpdating.GrupoId != GroupDbEnum.Master) throw new Exception("Only admin can add this especific role");
                }

                string validation = model.ValidUpdate();

                if (validation != string.Empty) throw new ValidationException("Erro na validacao do registro : " + validation);

                var userFromDB = await _repository.GetByIdAsync(model.Identifier.Value);

                model = await MergeAndPersistUser(userFromDB, model, userUpdationgId, model.Role.ToLower().Contains("Master") ? GroupDbEnum.Master : null);

                return model;
            }
            catch (ValidationException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Houve um erro ao tentar editar o registro: " + ex.Message);
            }
        }
        #endregion        

        #region DELETE SERVIÇO DE DELETE
        public async Task<UserDto> DeleteAsync(Guid UsuarioId)
        {
            try
            {
                var model = await _repository.GetByIdAsync(UsuarioId);

                string validation = model.ValidDelete();

                if (validation != string.Empty) throw new ValidationException("Erro na validacao do registro : " + validation);

                model.Ativo = false;
                await _repository.UpdateAsync(model);
                return _mapper.Map<Usuario, UserDto>(model);
            }
            catch (ValidationException ex)
            {
                throw new ValidationException("Houve um erro ao validar o registro: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Houve um erro ao tentar deletar o registro: " + ex.Message);
            }
        }
        #endregion

        #region VALIDATE IF THE USER IS A ADMIN
        public async Task<bool> UserServiceValidateAdminUser(Guid userId)
        {
            try
            {
                var parameters = new DynamicParameters();

                parameters.Add("@UserId", userId);

                var storedProcedure = $@"[seg].[ReturnUsersIsASystemAdmin] @UserId";

                var result = await _repository.ReturnObjectSingleFromQueryAsync<bool>(storedProcedure, parameters);

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Houve um erro ao buscar o registro desejado!" + ex.Message);
            }
        }
        #endregion

        #region REFRESH TOKEN
        public async Task<TokenApiDto> RefreshToken(TokenApiDto tokenApiDto)
        {
            try
            {
                if (tokenApiDto is null)
                    throw new Exception("invalid client Request");
                string accessToken = tokenApiDto.AccessToken;
                string refreshtoken = tokenApiDto.RefreshToken;
                var principal = GetPrincipleFromExpiredToken(accessToken);
                var username = principal.Identity.Name;
                var users = await _repository.ReturnListWithParametersPaginated<Usuario>(userName: username, pageNumber: 1, rowspPage: 1);
                var user = users.FirstOrDefault();

                if (user is null || user.RefreshToken != refreshtoken || user.RefreshTokenExpiryTime <= DateTime.Now)
                    throw new Exception("invalid request");
                var newAccessToken = CreateJwt(user);
                var newRefreshToken = await CreateRefreshToken(user.UsuarioId.Value);
                user.RefreshToken = newRefreshToken;
                await _repository.UpdateAsync(user);
                return new TokenApiDto()
                {
                    AccessToken = newAccessToken,
                    RefreshToken = newRefreshToken
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Houve um erro no metodo => RefreshToken: " + ex.Message);
            }
        }
        #endregion REFRESH TOKEN

        #region AUTHENTICATE
        public async Task<TokenApiDto> Authenticate([FromBody] AuthenticateRequest userObj)
        {
            try
            {
                if (userObj == null)
                    throw new Exception("invalid client Request");

                var user = _repository.ReturnListWithParametersPaginated<Usuario>(email: userObj.Email, pageNumber: 1, rowspPage: 1).Result.FirstOrDefault();

                if (user == null)
                    throw new Exception("User Not Found!");

                if (!PasswordHasher.VerifyPasword(userObj.Password, user.Senha))
                {
                    throw new Exception("Password is incorrect !");
                }

                user.Token = CreateJwt(user);
                var newAccessToken = user.Token;
                var newRefreshToken = await CreateRefreshToken(user.UsuarioId.Value);
                user.RefreshToken =  newRefreshToken;
                user.RefreshTokenExpiryTime = DateTime.Now.AddDays(JwtRuntimeConfig.RefreshTokenExpiryTimeInDay);
                await _repository.UpdateAsync(user);
                return new TokenApiDto()
                {
                    AccessToken = newAccessToken,
                    RefreshToken = newRefreshToken
                };
            }
            catch (Exception ex)
            {
                Serilog.Log.Error(ex, UtilHelper.FormatLogInformationMessage(message: "ERROR => Host terminated unexpectedly", userId: Guid.Parse("d2a833de-5bb4-4931-a3c2-133c8994072a")));
                throw new Exception(ex.Message);
            }
        }
        #endregion AUTHENTICATE

        #region REGISTER USER
        public async Task RegisterUser([FromBody] UserDto userObj)
        {
            #region VALIDATION
            if (userObj == null)
                throw new Exception("invalid client Request");

            if (userObj.NmrTelefone == null || string.IsNullOrEmpty(userObj.NmrTelefone))
            {
                throw new Exception("The request should has a fone number");
            }
            else
            {
                if (userObj.NmrTelefone.Length > 14 || userObj.NmrTelefone.Length < 10)
                    throw new Exception("The phone number should have betwen 10 and 14 numbers");
            }

            //check username
            if (await CheckUserNameExistAsync(userObj.UserName))
                throw new Exception("UserName Already Exist !");

            //check Email
            if (await CheckEmailExistAsync(userObj.Email))
                throw new Exception("Email Already Exist !");
            #endregion VALIDATION

            userObj.Password = PasswordHasher.HashPassword(userObj.Password);
            userObj.Token = "";

            var user = await InsertUser(userObj);
        }
        #endregion REGISTER USER

        #region PRIVATE ATRIBUTTES
        private async Task<bool> CheckEmailExistAsync(string Email)
            => _repository.ReturnListWithParametersPaginated<Usuario>(email: Email, pageNumber: 1, rowspPage: 1).Result.FirstOrDefault() != null;

        private async Task<bool> CheckUserNameExistAsync(string username)
            => _repository.ReturnListWithParametersPaginated<Usuario>(userName: username, pageNumber: 1, rowspPage: 1).Result.FirstOrDefault() != null;


        private string CreateJwt(Usuario user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(JwtRuntimeConfig.Secret);
            DateTime expiresAt = DateTime.Now.AddHours(JwtRuntimeConfig.ExpiresInHour);
            var identity = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.PrimarySid,$"{user.UsuarioId}"),
                new Claim(ClaimTypes.Email,$"{user.Email}"),
                new Claim(ClaimTypes.Expiration,$"{expiresAt}")
            });

            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                Expires = expiresAt,
                SigningCredentials = credentials
            };
            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            return jwtTokenHandler.WriteToken(token);
        }
        private async Task<string> CreateRefreshToken(Guid userId)
        {
            var tokenBytes = RandomNumberGenerator.GetBytes(64);
            var refreshToken = Convert.ToBase64String(tokenBytes);

            var users = await _repository.ReturnListWithParametersPaginated<UserDto>(userId: userId);

            var tokenInUser = users.Any(a => a.RefreshToken == refreshToken);

            if (tokenInUser)
            {
                return await CreateRefreshToken(userId);
            }
            return refreshToken;
        }
        private ClaimsPrincipal GetPrincipleFromExpiredToken(string token)
        {
            var key = Encoding.ASCII.GetBytes(JwtRuntimeConfig.Secret);
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateLifetime = false
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("this is invalid Token");
            return principal;
        }

        private async Task<Usuario> InsertUser(UserDto userDto)
        {
            try
            {
                string email = userDto.Email;
                string password = userDto.Password;

                var user = _mapper.Map<UserDto, Usuario>(userDto.TrasnformObjectPropValueToUpper());

                user.Email = email.RemoveAllSpecialCaracterFromEmail();
                user.Senha = password;
                user.GrupoId = GroupDbEnum.User;
                await _repository.AddAsync(user);

                return user;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private async Task<UserUpdateRequest> MergeAndPersistUser(Usuario userFromDB, UserUpdateRequest userInformationToUpdate, Guid userUpdationgId, Guid? userGroupId = null)
        {
            userFromDB.EstadoCivil = userInformationToUpdate.EstadoCivil;
            userFromDB.Nome = userInformationToUpdate.Nome;
            userFromDB.Sexo = userInformationToUpdate.Sexo;
            userFromDB.NmrTelefone = userInformationToUpdate.NmrTelefone;
            userFromDB.DataUltimaAlteracao = DateTime.Now;
            userFromDB.UsuarioUltimaAlteracaoId = userUpdationgId;

            if (userGroupId.HasValue) userFromDB.GrupoId = userGroupId;

            await _repository.UpdateAsync(userFromDB);

            return userInformationToUpdate;
        }
        #endregion PRIVATE ATRIBUTTES
    }
}
