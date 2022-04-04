using MediatR;

namespace JobOffersPortal.Application.Functions.Companies.Commands.DeleteCompany
{
    public class DeleteCompanyCommand : IRequest<Unit>
    {
        public string Id { get; set; }
    }
}
