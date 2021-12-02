using MediatR;
using System;

namespace JobOffersPortal.Application.Functions.Companies.Commands.CreateCompany
{
    public class CreateCompanyCommand : IRequest<CreateCompanyResponse>
    {
        public string Name { get; set; }
    }
}
