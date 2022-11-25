using MediatR;

namespace JobOffersPortal.Application.Functions.Companies.Commands.UpdateCompany
{
    public sealed class UpdateCompanyCommand : IRequest<Unit>
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
