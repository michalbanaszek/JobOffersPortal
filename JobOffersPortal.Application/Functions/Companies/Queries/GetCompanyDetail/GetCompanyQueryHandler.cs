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
    public class GetCompanyQueryHandler : IRequestHandler<GetCompanyQuery, CompanyDetailViewModel>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<GetCompanyQueryHandler> _logger;      
        private readonly ICompanyRepository _companyRepository;

        public GetCompanyQueryHandler(ICompanyRepository companyRepository ,IMapper mapper, ILogger<GetCompanyQueryHandler> logger)
        {
            _mapper = mapper;
            _logger = logger;
            _companyRepository = companyRepository;
        }

        public async Task<CompanyDetailViewModel> Handle(GetCompanyQuery request, CancellationToken cancellationToken)
        {
            var entity = await _companyRepository.GetByIdIncludeEntitiesAsync(request.Id);            

            if (entity == null)
            {
                _logger.LogWarning("Get JobOffer failed - NotFoundException, Id: {0}", request.Id);

                throw new NotFoundException(nameof(Company), request.Id);
            }

            return _mapper.Map<CompanyDetailViewModel>(entity);
        }
    }
}
