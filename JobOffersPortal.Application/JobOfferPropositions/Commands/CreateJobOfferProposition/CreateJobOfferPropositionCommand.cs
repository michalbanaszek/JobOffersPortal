﻿using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.JobOfferPropositions.Commands.CreateJobOfferProposition
{
    public class CreateJobOfferPropositionCommand : IRequest<CreateDetailsJobOfferPropositionViewModel>, IMapFrom<CreateDetailsJobOfferPropositionViewModel>
    {
        public string JobOfferId { get; set; }
        public string Content { get; set; }       

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateJobOfferPropositionCommand, CreateDetailsJobOfferPropositionViewModel>();
        }
    }

    public class CreateJobOfferPropositionCommandHandler : IRequestHandler<CreateJobOfferPropositionCommand, CreateDetailsJobOfferPropositionViewModel>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<CreateJobOfferPropositionCommandHandler> _logger;
        private readonly IApplicationDbContext _context;    

        public CreateJobOfferPropositionCommandHandler(IMapper mapper, ILogger<CreateJobOfferPropositionCommandHandler> logger, IApplicationDbContext context)
        {
            _mapper = mapper;
            _logger = logger;
            _context = context;      
        }

        public async Task<CreateDetailsJobOfferPropositionViewModel> Handle(CreateJobOfferPropositionCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.JobOffers.Include(x => x.Propositions)
                                                  .SingleOrDefaultAsync(x => x.Id == request.JobOfferId);
            if (entity == null)
            {
                throw new NotFoundException();
            }

            JobOfferProposition jobOfferProposition = new JobOfferProposition()
            {
                Id = Guid.NewGuid().ToString(),
                Content = request.Content
            };
                       
            entity.Propositions.Add(jobOfferProposition);

            await _context.SaveChangesAsync(new CancellationToken());

            _logger.LogInformation("Created JobOfferProposition for JobOffer Id: {0}, Name: {1}", entity.Id, entity.Position);

            return _mapper.Map<CreateDetailsJobOfferPropositionViewModel>(entity);
        }
    }
}