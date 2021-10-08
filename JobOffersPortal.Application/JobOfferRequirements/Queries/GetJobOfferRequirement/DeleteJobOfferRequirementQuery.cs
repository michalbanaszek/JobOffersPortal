using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Application.JobOfferRequirements.Queries.GetJobOfferRequirement
{
    public class DeleteJobOfferRequirementQuery : IRequest<JobOfferRequirementViewModel>
    {
        public string Id { get; set; }
    }

    public class DeleteJobOfferRequirementQueryHandler : IRequestHandler<DeleteJobOfferRequirementQuery, JobOfferRequirementViewModel>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteJobOfferRequirementQueryHandler> _logger;
        private readonly IApplicationDbContext _context;

        public DeleteJobOfferRequirementQueryHandler(IMapper mapper, ILogger<DeleteJobOfferRequirementQueryHandler> logger, IApplicationDbContext context)
        {
            _mapper = mapper;
            _logger = logger;
            _context = context;
        }

        public async Task<JobOfferRequirementViewModel> Handle(DeleteJobOfferRequirementQuery request, CancellationToken cancellationToken)
        {

            if (request.Id == null)
            {
                throw new NotFoundException();
            }

            var entity = await _context.JobOfferRequirements
                                       .FirstOrDefaultAsync(m => m.Id == request.Id);

            if (entity == null)
            {
                throw new NotFoundException();
            }

            return _mapper.Map<JobOfferRequirementViewModel>(entity);
        }
    }
}
