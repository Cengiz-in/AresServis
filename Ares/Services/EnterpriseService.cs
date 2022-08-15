using API.DTOs.Enterprise;
using API.DTOs.Pagination;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using API.Interfaces.Services;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace API.Services
{
    public class EnterpriseService : IEnterpriseService
    {
        private readonly IRepository<Enterprise> _enterpriseRepository;
        private readonly IMapper _mapper;

        public EnterpriseService(IRepository<Enterprise> enterpriseRepository,IMapper mapper)
        {
            _enterpriseRepository = enterpriseRepository;
            _mapper = mapper;
        }
        public async Task<Response<PagedList<EnterpriseDto>>> GetEnterprises(PaginationParams paginationParams, int userId)
        {
            var query = _enterpriseRepository.GetAll(s=> s.AppUserEnterprises.Any(q=> q.AppUserId == userId), new[] { "AppUserEnterprises" } ).ProjectTo<EnterpriseDto>(_mapper.ConfigurationProvider); ;
            var result = await PagedList<EnterpriseDto>.CreateAsync(query, paginationParams.PageNumber, paginationParams.PageSize);
            return new Response<PagedList<EnterpriseDto>>(result);
        }
    }
}
