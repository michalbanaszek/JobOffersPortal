using JobOffersPortal.Application.Common.Interfaces.Persistance;
using JobOffersPortal.Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using JobOffersPortal.Application.Functions.Companies.Queries.GetCompanyList;

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

                jobOfferList.Add(new JobOffer((jobOfferList.Count + 1).ToString(), jobOffer.CompanyId, jobOffer.Position, jobOffer.Salary, DateTime.Now, true));
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

            mockJobOffersRepository.Setup(repo => repo.IsPositionAlreadyExistAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync((string position, string companyId) =>
            {
                if (string.IsNullOrEmpty(position) || string.IsNullOrEmpty(companyId))
                {
                    return false;
                }

                var entity = jobOfferList.Where(x => x.CompanyId == companyId).FirstOrDefault(x => x.Position.ToLower() == position.ToLower());

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
                new Company("1", "CompanyName1") { CreatedBy = "user1" },
                new Company("2", "CompanyName2") {  CreatedBy = "user2" },
                new Company("3", "CompanyName3") { CreatedBy = "user3"  }
            };

            List<JobOffer> jobsLists = new List<JobOffer>();

            var jobOffer1 = new JobOffer("1", companies[0].Id, "position1", "1000", DateTime.Now, true) { CreatedBy = "user1", Created = DateTime.Now };
            jobOffer1.AddProposition(propositions[0], "1");
            jobOffer1.AddProposition(propositions[1], "1");
            jobOffer1.AddRequirement(requirements[0], "1");
            jobOffer1.AddRequirement(requirements[1], "1");
            jobOffer1.AddSkill(skills[0], "1");
            jobOffer1.AddSkill(skills[1], "1");

            jobsLists.Add(jobOffer1);

            return jobsLists;
        }
    }
}