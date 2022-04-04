using MediatR;

namespace JobOffersPortal.Application.Functions.Companies.Commands.CreateCompany
{
    public class CreateCompanyCommand : IRequest<CreateCompanyCommandResponse>
    {
        public string Name { get; set; }
    }
}
