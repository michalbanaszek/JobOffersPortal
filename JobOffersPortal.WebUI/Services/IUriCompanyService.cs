using System;

namespace JobOffersPortal.WebUI.Services
{
    public interface IUriCompanyService : IUriService
    {
        Uri GetCompanyUri(string companyId);
    }
}
