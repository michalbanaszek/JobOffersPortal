using System;

namespace Application.Common.Interfaces
{
    public interface IUriService
    {
        Uri GetAllUri(int pageNumber, int pageSize);
    }
}
