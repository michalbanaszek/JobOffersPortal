using JobOffersPortal.WebUI.Data;
using JobOffersPortal.WebUI.Domain;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace JobOffersPortal.WebUI.Services
{
    public class JobOfferService : Service<JobOffer>, IJobOfferService
    {
        public JobOfferService(DataContext context) : base(context)
        {
        }
    }
}
