using API.DTOs.Enterprise;
using API.DTOs.Pagination;
using API.Helpers;

namespace API.Interfaces.Services
{
    public interface IEnterpriseService 
    {
        public Task<Response<PagedList<EnterpriseDto>>> GetEnterprises(PaginationParams paginationParams,int userId);
    }
}
