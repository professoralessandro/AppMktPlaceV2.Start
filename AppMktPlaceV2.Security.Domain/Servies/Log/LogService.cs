#region REFERENCE
using AutoMapper;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using AppMktPlaceV2.Application.Enums.System;
using AppMktPlaceV2.Security.Application.Dtos.Log;
using AppMktPlaceV2.Security.Domain.Entities;
using AppMktPlaceV2.Security.Domain.Interfaces.Services.Log;
using AppMktPlaceV2.Security.Domain.Interfaces.Repository.Log;
using AppMktPlaceV2.Security.Domain.Business.Log;
using AppMktPlaceV2.Security.Application.Helper.Static.Generic;
#endregion

namespace AppMktPlaceV2.Security.Domain.Servies.User
{
    public class LogService : ILogService
    {
        #region ATRIBUTTES
        private readonly ILogRepository _repository;
        private readonly IMapper _mapper;
        #endregion

        #region CONTRUCTORS
        public LogService(ILogRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        #endregion

        #region FIND BY ID
        public async Task<LogDto> GetByIdAsync(Guid adressId)
        {
            try
            {
                var result = await _repository.GetByIdAsync(adressId);

                return _mapper.Map<Log, LogDto>(result);
            }
            catch (Exception ex)
            {
                throw new Exception("Houve um erro ao buscar o registro desejado!" + ex.Message);
            }
        }
        #endregion

        #region GET ALL ASYNC
        public async Task<IEnumerable<LogDto>> GetAllAsync()
        {
            try
            {
                var result = await _repository.GetAllAsync();

                return _mapper.Map<IEnumerable<Log>, IEnumerable<LogDto>>(result);
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível realizar a busca por registros: " + ex.Message);
            }
        }
        #endregion

        #region INSERT
        public async Task<LogDto> InsertAsync(LogDto model)
        {
            try
            {
                string validation = await model.ValidInsert(this);

                if (validation != string.Empty) throw new ValidationException("Erro na validacao do registro : " + validation);

                var resultDto = _mapper.Map<LogDto, Log>(model.TrasnformObjectPropValueToUpper());

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

        #region CREATE
        public async Task<LogDto> Create(HttpRequest request, HttpResponse response, string message, Guid? userId, string payload = null, string previousPayload = null)
        {
            try
            {
                var userAdded = SystemDBObjEnum.SystemUser;

                var model = new LogDto
                {
                    Method = request.Method.ToString(),
                    Request = $"{request.Scheme}://{request.Host.ToString()}{request.Path.ToString()}",
                    Response = response.StatusCode.ToString(),
                    Message = message,
                    UserAdded = userId.HasValue ? userId.Value : userAdded,
                    DateAdded = DateTime.Now.ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss"),
                    PreviousPayload = previousPayload,
                    CurrentPayload = payload,
                };

                var resultDto = _mapper.Map<LogDto, Log>(model);

                await _repository.AddAsync(resultDto);

                return model;
            }
            catch (Exception ex)
            {
                throw new Exception("Houve um erro ao incluir o registro de LOG: " + ex.Message);
            }
        }
        #endregion

        #region UPDATE
        public async Task<LogDto> UpdateAsync(LogDto model)
        {
            try
            {
                string validation = model.ValidUpdate();

                if (validation != string.Empty) throw new ValidationException("Erro na validacao do registro : " + validation);

                var resultDto = _mapper.Map<LogDto, Log>(model);

                await _repository.UpdateAsync(resultDto);

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
        public async Task<LogDto> RemoveAsync(Guid LogId)
        {
            try
            {
                var model = await _repository.GetByIdAsync(LogId);

                string validation = model.ValidDelete();

                if (validation != string.Empty) throw new ValidationException("Erro na validacao do registro : " + validation);

                await _repository.RemoveAsync(model);
                return _mapper.Map<Log, LogDto>(model);
            }
            catch (ValidationException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Houve um erro ao tentar deletar o registro: " + ex.Message);
            }
        }
        #endregion

        #region DOWNLOADFILE BY DATE
        public async Task<byte[]> DownloadFile(DateTime date)
        {
            try
            {
                var msgLog = "\n======================================================================================";
                msgLog += "\nBuscando por registros na base de dados";
                msgLog += "\n======================================================================================";
                Serilog.Log.Information(msgLog);

                if (date.Date >= DateTime.Now.Date) throw new ValidationException("You can only download files from last day");

                string fileName = $@"log{date.ToString("yyyyMMdd")}.txt";

                string dir = @".\SerilogStorage";

                string absolutePath = Path.Combine(dir, fileName);

                if (!File.Exists(absolutePath)) throw new ValidationException($"file {fileName} no found.");

                return File.ReadAllBytes(absolutePath);
            }
            catch (ValidationException ex)
            {
                var msgLog = "\n======================================================================================";
                msgLog += "\nVALIDATION ERROR: Houve um erro ao gerar o arquivo: \n" + ex.Message;
                msgLog += "\n======================================================================================";
                Serilog.Log.Error(msgLog);

                throw new ValidationException(ex.Message);
            }
            catch (Exception ex)
            {
                var msgLog = "\n======================================================================================";
                msgLog += "\nERROR: Houve um erro ao gerar o arquivo: \n" + ex.Message;
                msgLog += "\n======================================================================================";
                Serilog.Log.Error(msgLog);

                throw new Exception(ex.Message);
            }
        }
        #endregion
    }
}
