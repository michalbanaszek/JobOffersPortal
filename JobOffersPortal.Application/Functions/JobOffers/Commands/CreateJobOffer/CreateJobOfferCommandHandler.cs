using Application.Common.Interfaces;
using Application.JobOffers.Commands.CreateJobOffer;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace JobOffersPortal.Application.Functions.JobOffers.Commands.CreateJobOffer
{
    public class CreateJobOfferCommandHandler : IRequestHandler<CreateJobOfferCommand, (Uri, CreateJobOfferResponse)>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateJobOfferCommandHandler> _logger;
        private readonly IUriJobOfferService _uriJobOfferService;

        public CreateJobOfferCommandHandler(IMapper mapper, ILogger<CreateJobOfferCommandHandler> logger, IApplicationDbContext context, IUriJobOfferService uriJobOfferService)
        {
            _mapper = mapper;
            _logger = logger;
            _context = context;
            _uriJobOfferService = uriJobOfferService;
        }

        public async Task<(Uri, CreateJobOfferResponse)> Handle(CreateJobOfferCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<JobOffer>(request);

            _context.JobOffers.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Created JobOffer Id: {0}", entity.Id);

            var uri = _uriJobOfferService.GetJobOfferUri(entity.Id);

            return (uri, new CreateJobOfferResponse() { Id = entity.Id });
        }
    }
}
