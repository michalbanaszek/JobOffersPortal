using AutoMapper;
using JobOffersPortal.Application.Common.Exceptions;
using JobOffersPortal.Application.Common.Interfaces.Persistance;
using JobOffersPortal.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace JobOffersPortal.Application.Functions.Companies.Queries.GetCompanyDetail
{
    public class GetCompanyDetailQueryHandler : IRequestHandler<GetCompanyDetailQuery, CompanyDetailViewModel>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<GetCompanyDetailQueryHandler> _logger;
        private readonly ICompanyRepository _companyRepository;

        public GetCompanyDetailQueryHandler(ICompanyRepository companyRepository, IMapper mapper, ILogger<GetCompanyDetailQueryHandler> logger)
        {
            _mapper = mapper;
            _logger = logger;
            _companyRepository = companyRepository;
        }

        public async Task<CompanyDetailViewModel> Handle(GetCompanyDetailQuery request, CancellationToken cancellationToken)
        {
            var entity = await _companyRepository.GetByIdIncludeEntitiesAsync(request.Id);

            if (entity == null)
            {
                _logger.LogWarning("Entity not found from database. Request ID: {0}", request.Id);

                throw new NotFoundException(nameof(Company), request.Id);
            }

            return _mapper.Map<CompanyDetailViewModel>(entity);
        }
    }
}
