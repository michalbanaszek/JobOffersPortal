using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace JobOffersPortal.Application.Functions.JobOfferSkills.Command.UpdateJobOfferSkill
{
    public class UpdateJobOfferSkillCommandHandler : IRequestHandler<UpdateJobOfferSkillCommand, Unit>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateJobOfferSkillCommandHandler> _logger;
        private readonly IApplicationDbContext _context;

        public UpdateJobOfferSkillCommandHandler(IMapper mapper, ILogger<UpdateJobOfferSkillCommandHandler> logger, IApplicationDbContext context)
        {
            _mapper = mapper;
            _logger = logger;
            _context = context;
        }

        public async Task<Unit> Handle(UpdateJobOfferSkillCommand request, CancellationToken cancellationToken)
        {
            try
            {
                JobOfferSkill entity = new JobOfferSkill()
                {
                    Id = request.Id,
                    Content = request.Content
                };

                _context.JobOfferSkills.Update(entity);

                await _context.SaveChangesAsync(cancellationToken);

                _logger.LogInformation("UpdateJobOfferSkillCommand execuded.");

                return Unit.Value;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.JobOfferPropositions.Any(e => e.Id == request.Id))
                {
                    _logger.LogWarning("UpdateJobOfferPropositionCommand - NotFoundException execuded.");

                    throw new NotFoundException();
                }
                else
                {
                    _logger.LogWarning("UpdateJobOfferPropositionCommand - Exception execuded.");

                    throw;
                }
            }
        }
    }
}
