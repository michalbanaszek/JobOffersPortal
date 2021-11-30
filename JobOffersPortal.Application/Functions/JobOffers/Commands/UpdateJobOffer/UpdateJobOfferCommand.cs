using MediatR;
using System;

namespace JobOffersPortal.Application.Functions.JobOffers.Commands.UpdateJobOffer
{
    public class UpdateJobOfferCommand : IRequest<string>
    {
        public string Id { get; set; }
        public string Position { get; set; }
        public string Salary { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
