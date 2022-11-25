using MediatR;

namespace JobOffersPortal.Application.Functions.Companies.Commands.DeleteCompany
{
    public sealed class DeleteCompanyCommand : IRequest<Unit>
    {
        public string Id { get; set; }
    }
}
