using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace JobOffersPortal.Application.Functions.JobOffers.Commands.UpdateJobOffer
{
    public class UpdateJobOfferCommandHandler : IRequestHandler<UpdateJobOfferCommand, string>
    {
        private readonly IJobOfferService _jobOfferService;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateJobOfferCommandHandler> _logger;
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;

        public UpdateJobOfferCommandHandler(IJobOfferService jobOfferService, IMapper mapper, ILogger<UpdateJobOfferCommandHandler> logger, IApplicationDbContext context, ICurrentUserService currentUserService)
        {
            _jobOfferService = jobOfferService;
            _mapper = mapper;
            _logger = logger;
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task<string> Handle(UpdateJobOfferCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.JobOffers
                                       .Include(x => x.Requirements)
                                       .Include(x => x.Skills)
                                       .Include(x => x.Propositions)
                                       .SingleOrDefaultAsync(x => x.Id == request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(JobOffer), request.Id);
            }

            var userOwns = await _jobOfferService.UserOwnsEntityAsync(request.Id, _currentUserService.UserId);

            if (!userOwns)
            {
                _logger.LogWarning("Update company failed - NotFoundUserOwnException, Id: {0}, UserId: {1}", request.Id, _currentUserService.UserId);

                throw new NotFoundUserOwnException(nameof(Company), _currentUserService.UserId);
            }

            _mapper.Map(request, entity);

            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Updated JobOffer Id: {0}", request.Id);

            return entity.Id;
        }
    }
}
