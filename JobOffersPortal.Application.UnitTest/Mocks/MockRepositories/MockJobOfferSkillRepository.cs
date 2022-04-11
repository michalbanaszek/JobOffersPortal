using JobOffersPortal.Application.Common.Interfaces.Persistance;
using JobOffersPortal.Domain.Entities;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace JobOffersPortal.Application.UnitTest.Mocks.MockRepositories
{
    public static class MockJobOfferSkillRepository
    {
        public static Mock<IJobOfferSkillRepository> GetJobOfferSkillRepository()
        {
            var jobOfferRequirementList = GetJobOfferSkillList();

            var mockJobOfferSkillRepository = new Mock<IJobOfferSkillRepository>();

            mockJobOfferSkillRepository.Setup(repo => repo.GetAllAsync())
                                            .ReturnsAsync(jobOfferRequirementList);

            mockJobOfferSkillRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<string>()))
                                            .ReturnsAsync((string id) =>
                                            {
                                                return jobOfferRequirementList.FirstOrDefault(x => x.Id == id);
                                            });

            mockJobOfferSkillRepository.Setup(repo => repo.AddAsync(It.IsAny<JobOfferSkill>())).ReturnsAsync((JobOfferSkill entity) =>
            {
                entity.Id = (jobOfferRequirementList.Count + 1).ToString();
                jobOfferRequirementList.Add(entity);
                return entity;
            });

            mockJobOfferSkillRepository.Setup(repo => repo.UpdateAsync(It.IsAny<JobOfferSkill>())).Callback<JobOfferSkill>((entity) =>
            {
                jobOfferRequirementList.Remove(entity);
                jobOfferRequirementList.Add(entity);
            });

            mockJobOfferSkillRepository.Setup(repo => repo.DeleteAsync(It.IsAny<JobOfferSkill>())).Callback<JobOfferSkill>((entity) =>
            {
                jobOfferRequirementList.Remove(entity);
            });

            return mockJobOfferSkillRepository;
        }

        private static List<JobOfferSkill> GetJobOfferSkillList()
        {
            return new List<JobOfferSkill>()
            {
                new JobOfferSkill() { Id = "1", Content = "JobOfferSkill1" },
                new JobOfferSkill() { Id = "2", Content = "JobOfferSkill2" }
            };
        }
    }
}
