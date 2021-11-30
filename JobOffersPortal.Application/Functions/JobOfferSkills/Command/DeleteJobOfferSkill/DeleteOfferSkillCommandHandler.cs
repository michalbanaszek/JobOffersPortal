using Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace JobOffersPortal.Application.Functions.JobOfferSkills.Command.DeleteJobOfferSkill
{
    public class DeleteOfferSkillCommandHandler : IRequestHandler<DeleteOfferSkillCommand, Unit>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteOfferSkillCommandHandler> _logger;
        private readonly IApplicationDbContext _context;

        public DeleteOfferSkillCommandHandler(IMapper mapper, ILogger<DeleteOfferSkillCommandHandler> logger, IApplicationDbContext context, ICurrentUserService currentUserService)
        {
            _mapper = mapper;
            _logger = logger;
            _context = context;
        }

        public async Task<Unit> Handle(DeleteOfferSkillCommand request, CancellationToken cancellationToken)
        {
            var jobOfferSkill = await _context.JobOfferSkills.FindAsync(request.Id);

            _context.JobOfferSkills.Remove(jobOfferSkill);

            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Deleted JobOfferSkill Id: {0}", request.Id);

            return Unit.Value;
        }
    }
}
