﻿using JobOffersPortal.Domain.Primitives;

namespace JobOffersPortal.Domain.Entities
{
    public sealed class JobOfferRequirement : Entity
    {
        public JobOfferRequirement(string id, string content, string jobOfferId) : base(id)
        {
            Content = content;
            JobOfferId = jobOfferId;
        }

        public string Content { get; private set; }

        public string JobOfferId { get; private set; }

        public JobOffer JobOffer { get; private set; }
    }
}