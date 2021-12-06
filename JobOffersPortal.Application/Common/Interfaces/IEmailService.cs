﻿using JobOffersPortal.Application.Common.Models;
using System.Threading.Tasks;

namespace JobOffersPortal.Application.Common.Interfaces
{
    public interface IEmailService
    {
        public Task SendEmailAsync(SendEmailRequest request);
    }
}
