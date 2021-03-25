using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IJobOfferService
    {
        Task<bool> UserOwnsEntityAsync(string id, string userId);
    }
}
