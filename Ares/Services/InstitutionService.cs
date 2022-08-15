using API.DTOs.Instution;
using API.DTOs.Pagination;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using API.Interfaces.Services;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace API.Services
{
    public class InstitutionService : IInstitutionService
    {
        private readonly IRepository<Institution> _institutionRepository;
        private readonly IMapper _mapper;
        public InstitutionService(IRepository<Institution> institutionRepository, IMapper mapper)
        {
            _institutionRepository = institutionRepository;
            _mapper = mapper;
        }
        public async Task<Response<PagedList<InstitutionDto>>> GetInstitutions(PaginationParams paginationParams, int userId)
        {
            var query = _institutionRepository.GetAll(s => s.AppUsers.Any(q => q.Id == userId), new[] { "AppUsers" }).ProjectTo<InstitutionDto>(_mapper.ConfigurationProvider); ;
            var result = await PagedList<InstitutionDto>.CreateAsync(query, paginationParams.PageNumber, paginationParams.PageSize);
            return new Response<PagedList<InstitutionDto>>(result);
        }
    }
}
