using System;

namespace Application.Common.Interfaces
{
    public interface IUriCompanyService : IUriService
    {
        Uri GetCompanyUri(string companyId);
    }
}
