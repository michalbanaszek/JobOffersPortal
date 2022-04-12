using Application.JobOffers.Commands.CreateJobOffer;
using AutoMapper;
using JobOffersPortal.Application.Common.Exceptions;
using JobOffersPortal.Application.Common.Interfaces;
using JobOffersPortal.Application.Common.Interfaces.Persistance;
using JobOffersPortal.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace JobOffersPortal.Application.Functions.JobOffers.Commands.CreateJobOffer
{
    public class CreateJobOfferCommandHandler : IRequestHandler<CreateJobOfferCommand, CreateJobOfferCommandResponse>
    {
        private readonly IJobOfferRepository _jobOfferRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateJobOfferCommandHandler> _logger;
        private readonly IUriService _uriService;

        public CreateJobOfferCommandHandler(IMapper mapper, ILogger<CreateJobOfferCommandHandler> logger, IJobOfferRepository jobOfferRepository, ICompanyRepository companyRepository, IUriService uriJobOfferService)
        {
            _mapper = mapper;
            _logger = logger;
            _jobOfferRepository = jobOfferRepository;
            _uriService = uriJobOfferService;
            _companyRepository = companyRepository;
        }

        public async Task<CreateJobOfferCommandResponse> Handle(CreateJobOfferCommand request, CancellationToken cancellationToken)
        {
            var company = await _companyRepository.GetByIdAsync(request.CompanyId);

            if (company == null)
            {
                _logger.LogWarning("Entity not found from database. Request ID: {0}", request.CompanyId);

                throw new NotFoundException(nameof(Company), request.CompanyId);
            }

            var entity = _mapper.Map<JobOffer>(request);

            await _jobOfferRepository.AddAsync(entity);

            _logger.LogInformation("Created JobOffer Id: {0}", entity.Id);

            var uri = _uriService.Get(entity.Id, nameof(JobOffer));

            return new CreateJobOfferCommandResponse(uri);
        }
    }
}