using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface ICompanyService
    {      
        Task<bool> UserOwnsEntityAsync(string id, string userId);
    }
}
