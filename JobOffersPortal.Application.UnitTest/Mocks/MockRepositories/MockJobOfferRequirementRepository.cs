using JobOffersPortal.Application.Common.Interfaces.Persistance;
using JobOffersPortal.Domain.Entities;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace JobOffersPortal.Application.UnitTest.Mocks.MockRepositories
{
    public static class MockJobOfferRequirementRepository
    {
        public static Mock<IJobOfferRequirementRepository> GetJobOfferRequirementRepository()
        {
            var jobOfferRequirementList = GetJobOfferRequirementList();

            var mockJobOffeRequirementRepository = new Mock<IJobOfferRequirementRepository>();

            mockJobOffeRequirementRepository.Setup(repo => repo.GetAllAsync())
                                            .ReturnsAsync(jobOfferRequirementList);

            mockJobOffeRequirementRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<string>()))
                                            .ReturnsAsync((string id) =>
                                            {
                                                return jobOfferRequirementList.FirstOrDefault(x => x.Id == id);
                                            });

            mockJobOffeRequirementRepository.Setup(repo => repo.AddAsync(It.IsAny<JobOfferRequirement>())).ReturnsAsync((JobOfferRequirement entity) =>
            {
                jobOfferRequirementList.Add(new JobOfferRequirement((jobOfferRequirementList.Count + 1).ToString(), "NewContent", entity.JobOfferId));
                return entity;
            });

            mockJobOffeRequirementRepository.Setup(repo => repo.UpdateAsync(It.IsAny<JobOfferRequirement>())).Callback<JobOfferRequirement>((entity) =>
            {
                jobOfferRequirementList.Remove(entity);
                jobOfferRequirementList.Add(entity);
            });

            mockJobOffeRequirementRepository.Setup(repo => repo.DeleteAsync(It.IsAny<JobOfferRequirement>())).Callback<JobOfferRequirement>((entity) =>
            {
                jobOfferRequirementList.Remove(entity);
            });

            return mockJobOffeRequirementRepository;
        }

        private static List<JobOfferRequirement> GetJobOfferRequirementList()
        {
            return new List<JobOfferRequirement>()
            {
                new JobOfferRequirement("1", "JobOfferRequirement1", "1"),
                new JobOfferRequirement("2", "JobOfferRequirement2", "1") 
            };
        }
    }
}
