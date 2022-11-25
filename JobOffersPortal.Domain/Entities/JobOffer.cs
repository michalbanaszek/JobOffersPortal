using JobOffersPortal.Domain.Primitives;
using System;
using System.Collections.Generic;

namespace JobOffersPortal.Domain.Entities
{
    public sealed class JobOffer : Entity
    {
        private readonly List<JobOfferRequirement> _requirements = new();
        private readonly List<JobOfferSkill> _skills = new();
        private readonly List<JobOfferProposition> _propositions = new();

        public JobOffer(string id, string companyId, string position, string salary, DateTime date, bool isAvailable) : base(id)
        {
            Position = position;
            Salary = salary;
            Date = date;
            IsAvailable = isAvailable;
            CompanyId = companyId;
        }

        public string Position { get; private set; }

        public string Salary { get; private set; }

        public DateTime Date { get; private set; }

        public bool IsAvailable { get; private set; }

        public string CompanyId { get; private set; }

        public Company Company { get; private set; }

        public IReadOnlyCollection<JobOfferRequirement> Requirements => _requirements;

        public IReadOnlyCollection<JobOfferSkill> Skills => _skills;

        public IReadOnlyCollection<JobOfferProposition> Propositions => _propositions;

        public JobOfferSkill AddSkill(string content, string jobOfferId)
        {
            var skill = new JobOfferSkill(Guid.NewGuid().ToString(), content, jobOfferId);

            _skills.Add(skill);

            return skill;
        }

        public JobOfferRequirement AddRequirement(string content, string jobOfferId)
        {
            var requirement = new JobOfferRequirement(Guid.NewGuid().ToString(), content, jobOfferId);

            _requirements.Add(requirement);

            return requirement;
        }

        public JobOfferProposition AddProposition(string content, string jobOfferId)
        {
            var proposition = new JobOfferProposition(Guid.NewGuid().ToString(), content, jobOfferId);

            _propositions.Add(proposition);

            return proposition;
        }
    }
}
