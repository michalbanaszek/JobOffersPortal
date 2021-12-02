using JobOffersPortal.Application.Common.Interfaces;
using System;

namespace JobOffersPortal.Persistance.EF.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
