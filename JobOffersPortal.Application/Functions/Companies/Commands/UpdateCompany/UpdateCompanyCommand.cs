using MediatR;

namespace JobOffersPortal.Application.Functions.Companies.Commands.UpdateCompany
{
    public class UpdateCompanyCommand : IRequest<string>
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
