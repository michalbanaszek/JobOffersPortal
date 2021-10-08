using Domain.Common;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Company : AuditableEntity
    {
        public string Id { get; set; }

        public string Name { get; set; }     


        public IList<JobOffer> JobOffers { get; private set; } = new List<JobOffer>();
    }
}
