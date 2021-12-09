﻿using AutoMapper;
using JobOffersPortal.Application.Common.Interfaces;
using JobOffersPortal.Application.Common.Interfaces.Persistance;
using JobOffersPortal.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace JobOffersPortal.Application.Functions.Companies.Commands.CreateCompany
{
    public class CreateCompanyCommandHandler : IRequestHandler<CreateCompanyCommand, CreateCompanyCommandResponse>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateCompanyCommandHandler> _logger;
        private readonly IUriCompanyService _uriCompanyService;

        public CreateCompanyCommandHandler(ICompanyRepository companyRepository, IMapper mapper, ILogger<CreateCompanyCommandHandler> logger, IUriCompanyService uriCompanyService)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
            _logger = logger;
            _uriCompanyService = uriCompanyService;
        }

        public async Task<CreateCompanyCommandResponse> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
        {            
            try
            {
                var entity = _mapper.Map<Company>(request);

                await _companyRepository.AddAsync(entity);

                _logger.LogInformation("Created company Id: {0}, Name: {1}", entity.Id, entity.Name);

                var uri = _uriCompanyService.GetCompanyUri(entity.Id);

                return new CreateCompanyCommandResponse(entity.Id, uri);
            }
            catch (DbUpdateConcurrencyException dbUpdateConcurrencyException)
            {
                _logger.LogError("DbUpdateConcurrencyException execuded, Message:", dbUpdateConcurrencyException.Message);               

                return new CreateCompanyCommandResponse(false, new string[] { "Cannot add entity to database." });
            }
            catch(Exception exception)
            {
                _logger.LogError("Exception execuded, Message:", exception.Message);

                return new CreateCompanyCommandResponse(false, new string[] { "Cannot add entity to database." });
            }
        }
    }
}