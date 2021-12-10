using JobOffersPortal.Application.Common.Interfaces.Persistance;
using JobOffersPortal.Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JobOffersPortal.Application.UnitTest.Mocks
{
    internal static class MockJobOfferRepository
    {
        public static Mock<IJobOfferRepository> GetJobOffersRepository()
        {
            var jobOffers = GetJobOffersWithCompany();

            var mockJobOffersRepository = new Mock<IJobOfferRepository>();

            mockJobOffersRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(jobOffers);

            mockJobOffersRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<string>())).ReturnsAsync((string id) =>
            {
                return jobOffers.FirstOrDefault(x => x.Id == id);
            });

            mockJobOffersRepository.Setup(repo => repo.GetAllByCategory(It.IsAny<string>())).Returns((string categoryId) =>
            {
                return jobOffers.Where(x => x.CompanyId == categoryId).AsQueryable();
            });


            return mockJobOffersRepository;
        }

        private static List<JobOffer> GetJobOffersWithCompany()
        {
            List<Company> companies = new List<Company>()
            {
                new Company() { Id = "1", Name = "CompanyOne" },
                new Company() { Id = "2", Name = "CompanyTwo" }
            };

            List<JobOffer> jobsOne = new List<JobOffer>()
            {
                    new JobOffer()
                    {
                        Id = "1", Salary = "1000", IsAvailable = true, Date = DateTime.Now, Position = "PostitionOne", CompanyId = companies[0].Id,
                    },
                    new JobOffer()
                    {
                        Id = "2", Salary = "2000", IsAvailable = false, Date = DateTime.Now, Position = "PostitionTwo", CompanyId = companies[0].Id,
                    },
                    new JobOffer()
                    {
                        Id = "3", Salary = "3000", IsAvailable = true, Date = DateTime.Now, Position = "PostitionThree", CompanyId = companies[1].Id,
                    },
            };            

            return jobsOne;
        }
    }
}
