using System;

namespace JobOffersPortal.Application.Common.Interfaces
{
    public interface IUriCompanyService : IUriService
    {
        Uri GetCompanyUri(string companyId);
    }
}
