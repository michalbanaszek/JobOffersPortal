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

namespace Application.JobOfferSkills.Command.CreateJobOfferSkill
{
    public class CreateJobOfferSkillCommand : IRequest<CreateDetailsJobOfferSkillViewModel>, IMapFrom<CreateDetailsJobOfferSkillViewModel>
    {
        public string JobOfferId { get; set; }
        public string Content { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateJobOfferSkillCommand, CreateDetailsJobOfferSkillViewModel>();
        }
    }

    public class CreateJobOfferSkillCommandHandler : IRequestHandler<CreateJobOfferSkillCommand, CreateDetailsJobOfferSkillViewModel>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<CreateJobOfferSkillCommandHandler> _logger;
        private readonly IApplicationDbContext _context;

        public CreateJobOfferSkillCommandHandler(IMapper mapper, ILogger<CreateJobOfferSkillCommandHandler> logger, IApplicationDbContext context)
        {
            _mapper = mapper;
            _logger = logger;
            _context = context;
        }

        public async Task<CreateDetailsJobOfferSkillViewModel> Handle(CreateJobOfferSkillCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.JobOffers.Include(x => x.Skills)
                                                  .SingleOrDefaultAsync(x => x.Id == request.JobOfferId);
            if (entity == null)
            {
                throw new NotFoundException();
            }

            JobOfferSkill jobOfferSkill = new JobOfferSkill()
            {
                Id = Guid.NewGuid().ToString(),
                Content = request.Content
            };

            entity.Skills.Add(jobOfferSkill);

            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Created JobOfferRequirement for JobOffer Id: {0}, Name: {1}", entity.Id, entity.Position);

            return _mapper.Map<CreateDetailsJobOfferSkillViewModel>(entity);
        }
    }
}
