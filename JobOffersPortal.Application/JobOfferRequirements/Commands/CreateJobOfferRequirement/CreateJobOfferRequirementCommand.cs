using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.JobOfferRequirements.Commands.CreateJobOfferRequirement
{
    public class CreateJobOfferRequirementCommand : IRequest<CreateDetailsJobOfferRequirementViewModel>, IMapFrom<CreateDetailsJobOfferRequirementViewModel>
    {
        public string JobOfferId { get; set; }
        public string Content { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateJobOfferRequirementCommand, CreateDetailsJobOfferRequirementViewModel>();
        }
    }

    public class CreateJobOfferRequirementCommandHandler : IRequestHandler<CreateJobOfferRequirementCommand, CreateDetailsJobOfferRequirementViewModel>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<CreateJobOfferRequirementCommandHandler> _logger;
        private readonly IApplicationDbContext _context;

        public CreateJobOfferRequirementCommandHandler(IMapper mapper, ILogger<CreateJobOfferRequirementCommandHandler> logger, IApplicationDbContext context)
        {
            _mapper = mapper;
            _logger = logger;
            _context = context;
        }

        public async Task<CreateDetailsJobOfferRequirementViewModel> Handle(CreateJobOfferRequirementCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.JobOffers.Include(x => x.Requirements)
                                                  .SingleOrDefaultAsync(x => x.Id == request.JobOfferId);
            if (entity == null)
            {
                throw new NotFoundException();
            }

            JobOfferRequirement jobOfferRequirement = new JobOfferRequirement()
            {
                Id = Guid.NewGuid().ToString(),
                Content = request.Content
            };

            entity.Requirements.Add(jobOfferRequirement);

            await _context.SaveChangesAsync(new CancellationToken());

            _logger.LogInformation("Created JobOfferRequirement for JobOffer Id: {0}, Name: {1}", entity.Id, entity.Position);

            return _mapper.Map<CreateDetailsJobOfferRequirementViewModel>(entity);
        }
    }
}
