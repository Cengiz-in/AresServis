using API.DTOs.Instution;
using API.DTOs.Pagination;
using API.Helpers;

namespace API.Interfaces.Services
{
    public interface IInstitutionService
    {
        public Task<Response<PagedList<InstitutionDto>>> GetInstitutions(PaginationParams paginationParams,int userId);
    }
}
