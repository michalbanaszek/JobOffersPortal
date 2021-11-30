using JobOffersPortal.Application.Functions.JobOffers.Commands.CreateJobOffer;
using MediatR;
using System;
using System.Collections.Generic;

namespace Application.JobOffers.Commands.CreateJobOffer
{
    public class CreateJobOfferCommand : IRequest<(Uri, CreateJobOfferResponse)>
    {
        public string CompanyId { get; set; }
        public string Position { get; set; }
        public string Salary { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public bool IsAvailable { get; set; } = false;
        public IEnumerable<string> Requirements { get; set; } = new List<string>();
        public IEnumerable<string> Skills { get; set; } = new List<string>();
        public IEnumerable<string> Propositions { get; set; } = new List<string>();    
    }
}
