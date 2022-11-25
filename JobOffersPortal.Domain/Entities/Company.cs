using System;
using JobOffersPortal.Domain.Primitives;
using System.Collections.Generic;

namespace JobOffersPortal.Domain.Entities
{
    public sealed class Company : Entity
    {
        private readonly List<JobOffer> _jobOffers = new();

        public Company(string id, string name) : base(id)
        {
            Name = name;
        }

        public string Name { get; private set; }

        public IReadOnlyCollection<JobOffer> JobOffers => _jobOffers;

        public static Company Create(string name)
        {
            var company = new Company(Guid.NewGuid().ToString(), name);

            return company;
        }

        public JobOffer AddJobOffer(string companyId, string position, string salary, DateTime date, bool isAvailable)
        {
            var jobOffer = new JobOffer(Guid.NewGuid().ToString(), companyId, position, salary, date, isAvailable);

            _jobOffers.Add(jobOffer);

            return jobOffer;
        }
    }
}
