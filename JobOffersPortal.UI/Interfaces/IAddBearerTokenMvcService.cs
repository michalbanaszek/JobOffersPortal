using System.Threading.Tasks;
using WebApp.Services;

namespace WebApp.Interfaces
{
    public interface IAddBearerTokenMvcService
    {
        void AddBearerToken(IClient client);
    }
}
