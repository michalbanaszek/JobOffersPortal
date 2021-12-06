﻿using AutoMapper;
using JobOffersPortal.Application.Common.Exceptions;
using JobOffersPortal.Application.Common.Interfaces.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace JobOffersPortal.Application.Functions.JobOfferSkills.Queries.GetJobOfferSkillDetail
{
    public class GetJobOfferSkillDetailQueryHandler : IRequestHandler<GetJobOfferSkillDetailQuery, JobOfferSkillDetailViewModel>
    {
        private readonly IJobOfferSkillRepository _jobOfferSkillRepository;
        private readonly IMapper _mapper;

        public GetJobOfferSkillDetailQueryHandler(IJobOfferSkillRepository jobOfferSkillRepository, IMapper mapper)
        {
            _jobOfferSkillRepository = jobOfferSkillRepository;
            _mapper = mapper;
        }

        public async Task<JobOfferSkillDetailViewModel> Handle(GetJobOfferSkillDetailQuery request, CancellationToken cancellationToken)
        {
            if (request.Id == null)
            {
                throw new NotFoundException();
            }

            var entities = await _jobOfferSkillRepository.GetByIdAsync(request.Id);

            if (entities == null)
            {
                throw new NotFoundException();
            }

            return _mapper.Map<JobOfferSkillDetailViewModel>(entities);
        }
    }
}