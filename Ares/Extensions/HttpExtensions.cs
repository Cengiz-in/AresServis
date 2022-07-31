using System;
using System.Text.Json;
using API.Helpers;
using Microsoft.AspNetCore.Http;

namespace API.Extensions
{
    public static class HttpExtensions
    {
        public static void AddPaginationHeader(this HttpResponse response,int currentPage,int itemsPerPage,
            int totalItems,int totalPages)
        {
            var paginationHeader = new PaginationHeader(currentPage,itemsPerPage,totalItems,totalPages);
            var options =  new JsonSerializerOptions{
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            response.Headers.Add("Pagination", JsonSerializer.Serialize(paginationHeader,options));
            response.Headers.Add("Access-Control-Expose-Headers","Pagination");
        }
        
        public static string ipAddress(this HttpRequest request, HttpContext context)
        {
            if (request.Headers.ContainsKey("X-Forwarded-For"))
                return request.Headers["X-Forwarded-For"];
            else
                return context.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }
    }
}