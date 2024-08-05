#region REFERENCES
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using Serilog;
using System.ComponentModel.DataAnnotations;
using AppMktPlaceV2.Start.Application.Models.Dto.User;
using AppMktPlaceV2.Start.Application.Models.Dtos;
using AppMktPlaceV2.Start.Application.Models.Dto.Autenticate;
using AppMktPlaceV2.Start.Api.Controllers.Common;
using AppMktPlaceV2.Start.Domain.Interfaces.Services.User;
using AppMktPlaceV2.Start.Application.Helper.Static.Generic;
using AppMktPlaceV2.Start.Domain.Interfaces.Services.Log;
using AppMktPlaceV2.Start.Domain.Entities;
using Microsoft.AspNetCore.OutputCaching;
using AppMktPlaceV2.Start.Application.Dtos.User.Response;
using AppMktPlaceV2.Start.Application.Dtos.User.Request;
#endregion REFERENCES

namespace AppMktPlaceV2.Start.Api.Controllers
{
    public class UserController : CommonController
    {
        #region ATRIBUTTES
        private readonly IMapper _mapper;
        private readonly IUserService _service;
        private readonly ILogService _log;
        #endregion ATRIBUTTES

        #region CONTRUCTORS
        public UserController(IUserService service, ILogService log, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
            _log = log;
        }
        #endregion CONTRUCTORS

        #region AUTHENTICATE
        [HttpPost("authenticate"), AllowAnonymous]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateRequest userObj)
        {
            try
            {
                var result = await _service.Authenticate(userObj);

                if (userObj == null)
                    return BadRequest();

                return Ok(result);
            }
            catch (Exception ex)
            {
                Serilog.Log.Error(ex, UtilHelper.FormatLogInformationMessage(message: "ERROR => Host terminated unexpectedly", userId: Guid.Parse("d2a833de-5bb4-4931-a3c2-133c8994072a")));
                return BadRequest(ex.Message);
            }
        }
        #endregion AUTHENTICATE

        #region REGISTER
        [HttpPost("register"), AllowAnonymous]
        public async Task<IActionResult> Registeruser([FromBody] UserDto userObj)
        {
            try
            {
                await _service.RegisterUser(userObj);

                return Ok(new
                {
                    Message = "User Registered"
                });
            }
            catch (Exception ex)
            {
                Serilog.Log.Error(ex, UtilHelper.FormatLogInformationMessage(message: "ERROR => Host terminated unexpectedly", userId: Guid.Parse("d2a833de-5bb4-4931-a3c2-133c8994072a")));
                return BadRequest(ex.Message);
            }
        }
        #endregion REGISTER

        #region GET BY ID
        [HttpGet, Route("GetById"), OutputCache]
        public async Task<ActionResult<UserDto>> GetById(
            [FromQuery] Guid UserId
            )
        {
            try
            {
                var result = await _service.GetByIdAsync(UserId);

                if (result == null) { return NotFound($"register: {UserId} not found"); }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return UnprocessableEntity(ex.Message);
            }
        }
        #endregion

        #region GET ALL USERS
        [HttpGet]
        public async Task<ActionResult<UserResponseDto>> GetAllUsersByParameter(
                [FromQuery] Guid? id,
                [FromQuery] string? userName,
                [FromQuery] string? nome,
                [FromQuery] string? nmrDocumento,
                [FromQuery] string? email,
                [FromQuery] bool? ativo,
                [FromQuery] int? pageNumber,
                [FromQuery] int? rowspPage
            )
        {
            try
            {
                if (!Guid.TryParse(this.Request.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.PrimarySid)?.Value, out Guid userId)) throw new ValidationException("Error to validate user");

                var result = await _service.ReturnListWithParametersPaginated(id, userId, userName, nome, nmrDocumento, email, ativo, pageNumber, rowspPage);

                var users = _mapper.Map<IEnumerable<UserDto>, IEnumerable<UserResponseDto>>(result);

                return Ok(users);
            }
            catch (Exception ex)
            {
                Serilog.Log.Error(ex, UtilHelper.FormatLogInformationMessage(message: "ERROR => Host terminated unexpectedly", userId: Guid.Parse("d2a833de-5bb4-4931-a3c2-133c8994072a")));
                return BadRequest(ex.Message);
            }
        }
        #endregion GET ALL USERS

        #region FIND USERS TO SELECT RETURN KEY AND VALUE
        [HttpGet, Route("ReturnUsersToSelect"), OutputCache]
        public async Task<ActionResult<IEnumerable<object>>> ReturnUsersToSelectAsync(
                [FromQuery] string? param,
                [FromQuery] int? pageNumber,
                [FromQuery] int? rowspPage
            )
        {
            try
            {
                var result = await _service.ReturnUsersToSelectAsync(param, pageNumber, rowspPage);

                // if (!result.Any()) { return NotFound($"register: {BlockTypeEnum} not found"); }

                return Ok(result);
            }
            catch (Exception ex)
            {
                this.Response.StatusCode = 422;
                await this._log.Create(this.Request, this.Response, ex.Message, null);
                Serilog.Log.Error(ex, ex.Message);
                return UnprocessableEntity(ex.Message);
            }
        }
        #endregion

        #region REFRESH TOKEN
        [HttpPost("refresh"), AllowAnonymous]
        public async Task<IActionResult> Refresh(TokenApiDto tokenApiDto)
        {
            try
            {
                var result = this._service.RefreshToken(tokenApiDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Serilog.Log.Error(ex, UtilHelper.FormatLogInformationMessage(message: "ERROR => Host terminated unexpectedly", userId: Guid.Parse("d2a833de-5bb4-4931-a3c2-133c8994072a")));
                return BadRequest(ex.Message);
            }
        }
        #endregion REFRESH TOKEN

        #region UPDATE
        [HttpPut]
        public async Task<ActionResult<UserDto>> Update(UserUpdateRequest model, Guid userUpdationgId)
        {
            try
            {
                if (!Guid.TryParse(this.Request.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.PrimarySid)?.Value, out Guid userId)) throw new ValidationException("Error to validate user");

                var response = await _service.UpdateAsync(model, userId);
                await this._log.Create(this.Request, this.Response, this.Response.StatusCode.ToString(), null);
                Serilog.Log.Information("Operation completed successfully");
                return Ok(response);
            }
            catch (Exception ex)
            {
                this.Response.StatusCode = 422;
                await this._log.Create(this.Request, this.Response, ex.Message, null);
                Serilog.Log.Error(ex, ex.Message);
                return UnprocessableEntity(ex.Message);
            }
        }
        #endregion

        #region DELETE
        [HttpDelete]
        public async Task<ActionResult> Delete(Guid Id)
        {
            try
            {
                await _service.DeleteAsync(Id);
                await this._log.Create(this.Request, this.Response, this.Response.StatusCode.ToString(), null);
                Serilog.Log.Information("Operation completed successfully");
                return Ok();
            }
            catch (Exception ex)
            {
                this.Response.StatusCode = 422;
                await this._log.Create(this.Request, this.Response, ex.Message, null);
                Serilog.Log.Error(ex, ex.Message);
                return UnprocessableEntity(ex.Message);
            }
        }
        #endregion
    }
}

