﻿using AutoMapper;
using JobOffersPortal.Application.Common.Exceptions;
using JobOffersPortal.Application.Common.Interfaces;
using JobOffersPortal.Application.Common.Interfaces.Persistance;
using JobOffersPortal.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace JobOffersPortal.Application.Functions.Companies.Commands.CreateCompany
{
    public class CreateCompanyCommandHandler : IRequestHandler<CreateCompanyCommand, CreateCompanyCommandResponse>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateCompanyCommandHandler> _logger;
        private readonly IUriService _uriService;

        public CreateCompanyCommandHandler(ICompanyRepository companyRepository, IMapper mapper, ILogger<CreateCompanyCommandHandler> logger, IUriService uriService)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
            _logger = logger;
            _uriService = uriService;
        }

        public async Task<CreateCompanyCommandResponse> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Company>(request);

            await _companyRepository.AddAsync(entity);

            _logger.LogInformation("Created company Id: {0}, Name: {1}", entity.Id, entity.Name);

            var uri = _uriService.Get(entity.Id, nameof(Company));

            return new CreateCompanyCommandResponse(uri);
        }
    }
}