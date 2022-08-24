using API.DTOs.Pagination;
using API.DTOs.Vehicle;
using API.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace API.Interfaces.Services
{
    public interface IVehicleService
    {
        Task<Response<bool>> SetVehicleStatus(int vehicleId,bool status);

        Task<Response<bool>> UpdateVehicle(int vehicleId, AddVehicle model);
        Task<Response<PagedList<VehicleDto>>> GetDriverVehicles(PaginationParams paginationParams, int appUserId);
        Task<Response<bool>> CreateVehicle(CreateVehicleDto createVehicleDto,int enterpriseId);

        public Task<Response<PagedList<VehicleDto>>> GetVehicles(PaginationParams paginationParams, int userId);
    }
}
