using Application.Common.Interfaces;
using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.JobOffers.Commands.CreateJobOffer
{
    public class CreateJobOfferCommand : IRequest<(Uri, string)>, IMapFrom<JobOffer>
    {
        public string CompanyId { get; set; }

        public string Position { get; set; }

        public string Salary { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;
        public bool IsAvailable { get; set; } = false;

        public IEnumerable<string> Requirements { get; set; } = new List<string>();

        public IEnumerable<string> Skills { get; set; } = new List<string>();

        public IEnumerable<string> Propositions { get; set; } = new List<string>();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateJobOfferCommand, JobOffer>()
                   .ForMember(dest => dest.Requirements,
                      opt => opt.MapFrom(src => src.Requirements.Select(x => 
                      new JobOfferRequirement() { Id = Guid.NewGuid().ToString(), Content = x })))
                   .ForMember(dest => dest.Skills,
                      opt => opt.MapFrom(src => src.Skills.Select(y => 
                      new JobOfferSkill() { Id = Guid.NewGuid().ToString(), Content = y })))
                   .ForMember(dest => dest.Propositions,
                      opt => opt.MapFrom(src => src.Propositions.Select(z =>
                      new JobOfferProposition() { Id = Guid.NewGuid().ToString(), Content = z })));
        }
    }

    public class CreateJobOfferCommandHandler : IRequestHandler<CreateJobOfferCommand, (Uri, string)>
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

        public async Task<(Uri,string)> Handle(CreateJobOfferCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<JobOffer>(request);

            _context.JobOffers.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Created JobOffer Id: {0}", entity.Id);

            var uri = _uriJobOfferService.GetJobOfferUri(entity.Id);

            return (uri, entity.Id);
        }
    }
}
