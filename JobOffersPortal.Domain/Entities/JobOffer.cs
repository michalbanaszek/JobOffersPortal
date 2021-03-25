using Domain.Common;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class JobOffer : AuditableEntity
    {
        public string Id { get; set; }

        public Company Company { get; set; }

        public string CompanyId { get; set; }

        public string Position { get; set; }

        public int Salary { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;

        public List<JobOfferRequirement> Requirements { get; set; } = new List<JobOfferRequirement>();

        public List<JobOfferSkill> Skills { get; set; } = new List<JobOfferSkill>();

        public List<JobOfferProposition> Propositions { get; set; } = new List<JobOfferProposition>();
    }
}
