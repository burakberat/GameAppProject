﻿using GameApp.Infrastructure.Models.Dtos;
using GameApp.Infrastructure.Models.Entities;
using GameApp.Infrastructure.Models.Enums;
using GameApp.Infrastructure.Service.Abstracts;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Transactions;

namespace GameApp.Infrastructure.API.Middlewares
{
    public class LogMiddleware
    {
        string requestBody = "";
        string responseText = "";
        ILogService _logService;
        DateTime stratTime;
        Stream originalBodyStream;

        private readonly RequestDelegate _next;
        public LogMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, ILogService logService)
        {
            _logService = logService;
            var transctionOptions = new TransactionOptions
            {
                IsolationLevel = IsolationLevel.ReadUncommitted
            };
            stratTime = DateTime.UtcNow;

            originalBodyStream = context.Response.Body;
            // Akışı kapatmadan işlemi yapabilmek için yeni bir MemoryStream oluşturun
            var memoryStreamRequestBody = new MemoryStream();
            var memoryStreamResponseBody = new MemoryStream();
            context.Response.Body = memoryStreamResponseBody;
            try
            {
                using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    // İstek gövdesini MemoryStream'e kopyalayın
                    await context.Request.Body.CopyToAsync(memoryStreamRequestBody);

                    // Başlangıca dönerek tekrar okuma için sıfırlayın
                    memoryStreamRequestBody.Position = 0;
                    context.Request.Body = memoryStreamRequestBody;

                    // İsteğin gövdesini bir kere okuyup loglayabilirsiniz
                    requestBody = await new StreamReader(memoryStreamRequestBody).ReadToEndAsync();
                    //Console.WriteLine($"Request Body: {requestBody}");

                    // Okuma işlemi için tekrar başa döndürün
                    memoryStreamRequestBody.Position = 0;

                    await _next.Invoke(context);
                    // StatusCode 401 ise özel bir mesaj döndür
                    if (context.Response.StatusCode >= 300)
                    {
                        throw new UnauthorizedAccessException();
                    }
                    int userId = Convert.ToInt32(context.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value);

                    memoryStreamResponseBody.Position = 0;
                    responseText = await new StreamReader(memoryStreamResponseBody).ReadToEndAsync();

                    LogTable log = new LogTable()
                    {
                        StatusId = 1,
                        UserId = userId,
                        RequestPath = context.Request.Path,
                        RequestBody = requestBody,
                        ResponseBody = responseText,
                        ApplicationId = 1,
                        IpAddress = context.Connection.RemoteIpAddress.ToString(),
                        LastTransactionDate = DateTime.Now,

                    };
                    await logService.AddLogAsync(log);
                    transactionScope.Complete();

                    memoryStreamResponseBody.Position = 0;
                    await memoryStreamResponseBody.CopyToAsync(originalBodyStream);

                }

            }
            catch (Exception ex)
            {
                await this.HandleExceptionAsync(context, ex);
            }
            finally
            {
                context.Response.Body = originalBodyStream;
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            int userId = Convert.ToInt32(context.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value);
            string erorMessage = exception.Message;
            string clientMesaj = "";

            switch (exception)
            {
                case UnauthorizedAccessException:
                    clientMesaj = Messages.UnauthorizedAccess;
                    break;

                //case Saat20SonrasiException:
                //    clientMesaj = "Saat 20:00'dan sonra işlem gerçekleştiremezsiniz.";
                //    //bu hataya özel işlem yapacaksam burada yapmalıyım
                //    break;

                default:
                    clientMesaj = Messages.UnexpectedError;
                    break;
            }

            ErrorLogTable errorLogTable = new()
            {
                StatusId = 0,
                UserId = userId,
                RequestPath = context.Request.Path,
                RequestBody = requestBody,
                ResponseBody = JsonSerializer.Serialize(ResultDto<NoContent>.Error($"Message : {clientMesaj}", null)),
                ApplicationId = 1,
                LogMessage = erorMessage,
                ErrorCode = context.Response.StatusCode,
                IpAddress = context.Connection.RemoteIpAddress.ToString(),
                LastTransactionDate = DateTime.Now,
                ProcessTime = Convert.ToInt64((DateTime.UtcNow - stratTime).TotalMilliseconds)
            };

            await _logService.AddErrorLogAsync(errorLogTable);

            context.Response.Body = originalBodyStream;

            await context.Response.WriteAsync(JsonSerializer.Serialize(ResultDto<NoContent>.Error($"Error Code : {errorLogTable.Id} Message : {clientMesaj}", null)));
        }
    }
}
