using System.Net;
using System.Text.Json;
using API.Helpers;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using API.Models.Exceptions;

namespace API.Middleware
{
    public class GlobalErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                // _logger.LogError(exception: error, message: error.Message);

                var responseModel = new Response<string>() { Succeeded = false };

                switch (error)
                {
                    case CustomException:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        responseModel.Message = error.Message;
                        break;
                    case IdentityException e:
                        responseModel.Errors = e.Errors.Select(s => s.Description).ToList();
                        break;
                    default:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        responseModel.Message = error.Message;
                        break;
                }


                var result = JsonSerializer.Serialize(responseModel, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

                await response.WriteAsync(result);
            }
        }
    }
}