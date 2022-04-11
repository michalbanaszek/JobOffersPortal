using JobOffersPortal.Application.Common.Interfaces.Persistance;
using JobOffersPortal.Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JobOffersPortal.Application.UnitTest.Mocks.MockRepositories
{
    public static class MockJobOfferRepository
    {
        public static Mock<IJobOfferRepository> GetJobOffersRepository()
        {
            var jobOfferList = GetJobOffersWithCompany();

            var mockJobOffersRepository = new Mock<IJobOfferRepository>();

            mockJobOffersRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(jobOfferList);

            mockJobOffersRepository.Setup(repo => repo.GetAllByCompany(It.IsAny<string>())).Returns((string companyId) =>
            {
                return jobOfferList.Where(repo => repo.CompanyId == companyId).AsQueryable();                
            });

            mockJobOffersRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<string>())).ReturnsAsync((string id) =>
            {
                return jobOfferList.FirstOrDefault(x => x.Id == id);
            });

            mockJobOffersRepository.Setup(repo => repo.GetAllByCompany(It.IsAny<string>())).Returns((string categoryId) =>
            {
                return jobOfferList.Where(x => x.CompanyId == categoryId).AsQueryable();
            });

            mockJobOffersRepository.Setup(repo => repo.GetByIdIncludeAllEntities(It.IsAny<string>())).ReturnsAsync((string id) =>
            {
                return jobOfferList.FirstOrDefault(x => x.Id == id);
            });

            mockJobOffersRepository.Setup(repo => repo.AddAsync(It.IsAny<JobOffer>())).ReturnsAsync((JobOffer jobOffer) =>
            {
                jobOffer.Id = (jobOfferList.Count + 1).ToString();
                jobOfferList.Add(jobOffer);
                return jobOffer;
            });

            mockJobOffersRepository.Setup(repo => repo.UpdateAsync(It.IsAny<JobOffer>())).Callback((JobOffer jobOffer) =>
            {
                jobOfferList.Remove(jobOffer);
                jobOfferList.Add(jobOffer);
            });

            mockJobOffersRepository.Setup(repo => repo.DeleteAsync(It.IsAny<JobOffer>())).Callback((JobOffer jobOffer) =>
            {
                jobOfferList.Remove(jobOffer);
            });

            mockJobOffersRepository.Setup(repo => repo.UserOwnsEntityAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync((string id, string userId) =>
            {
                var entity = jobOfferList.FirstOrDefault(x => x.Id == id);

                if (entity == null)
                {
                    return false;
                }

                return entity.CreatedBy != userId ? false : true;
            });

            mockJobOffersRepository.Setup(repo => repo.IsPositionAlreadyExistAsync(It.IsAny<string>())).ReturnsAsync((string position) =>
            {
                var entity = jobOfferList.FirstOrDefault(x => x.Position.ToLower() == position.ToLower());

                return entity == null ? false : true;
            });

            return mockJobOffersRepository;
        }

        private static List<JobOffer> GetJobOffersWithCompany()
        {
            var skills = new[]
            {
                "skill1",
                "skill2",
                "skill3"
            };

            var requirements = new[]
            {
                "requirement1",
                "requirement2",
                "requirement3"
            };

            var propositions = new[]
            {
                "proposition1",
                "proposition2",
                "proposition3"
            };

            List<Company> companies = new List<Company>()
            {
                new Company() { Id = "1", Name = "CompanyName1", CreatedBy = "user1" },
                new Company() { Id = "2", Name = "CompanyName2", CreatedBy = "user2" },
                new Company() { Id = "3", Name = "CompanyName3", CreatedBy = "user3"  }
            };

            List<JobOffer> jobs = new List<JobOffer>()
            {
                 new JobOffer()
                 {
                    Id = "1",
                    CompanyId = companies[0].Id,
                    Date = DateTime.Now,
                    IsAvailable = true,
                    Propositions = new List<JobOfferProposition>()
                    {
                        new JobOfferProposition() { Id = "1", Content = propositions[0] },
                        new JobOfferProposition() { Id = "2", Content = propositions[1] },
                    },
                    Requirements = new List<JobOfferRequirement>()
                    {
                        new JobOfferRequirement() { Id = "1", Content = requirements[0] },
                        new JobOfferRequirement() { Id = "2", Content = requirements[1] },
                    },
                    Skills = new List<JobOfferSkill>()
                    {
                        new JobOfferSkill() { Id = "1", Content = skills[0] },
                        new JobOfferSkill() { Id = "2", Content = skills[1] },
                    },
                    Salary = "1000",
                    Position = "Position1",
                    CreatedBy = "user1",
                    Created = DateTime.Now
                 },
                 new JobOffer()
                 {
                    Id = "2",
                    CompanyId = companies[1].Id,
                    Date = DateTime.Now,
                    IsAvailable = true,
                    Propositions = new List<JobOfferProposition>()
                    {
                        new JobOfferProposition() { Id = "1", Content = propositions[0] },
                        new JobOfferProposition() { Id = "2", Content = propositions[1] },
                    },
                    Requirements = new List<JobOfferRequirement>()
                    {
                        new JobOfferRequirement() { Id = "1", Content = requirements[0] },
                        new JobOfferRequirement() { Id = "2", Content = requirements[1] },
                    },
                    Skills = new List<JobOfferSkill>()
                    {
                        new JobOfferSkill() { Id = "1", Content = skills[0] },
                        new JobOfferSkill() { Id = "2", Content = skills[1] },
                    },
                    Salary = "2000",
                    Position = "Position2",
                    CreatedBy = "user1",
                    Created = DateTime.Now
                 },
                 new JobOffer()
                 {
                    Id = "3",
                    CompanyId = companies[2].Id,
                    Date = DateTime.Now,
                    IsAvailable = true,
                    Propositions = new List<JobOfferProposition>()
                    {
                        new JobOfferProposition() { Id = "2", Content = propositions[1] },
                        new JobOfferProposition() { Id = "3", Content = propositions[2] },
                    },
                    Requirements = new List<JobOfferRequirement>()
                    {
                        new JobOfferRequirement() { Id = "2", Content = requirements[1] },
                        new JobOfferRequirement() { Id = "3", Content = requirements[2] },
                    },
                    Skills = new List<JobOfferSkill>()
                    {
                        new JobOfferSkill() { Id = "2", Content = skills[1] },
                        new JobOfferSkill() { Id = "3", Content = skills[2] },
                    },
                    Salary = "3000",
                    Position = "Position3",
                    CreatedBy = "user1",
                    Created = DateTime.Now
                 }
            };

            return jobs;
        }
    }
}