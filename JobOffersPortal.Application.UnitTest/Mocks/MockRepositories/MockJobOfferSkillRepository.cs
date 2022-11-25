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
            var jobOfferSkillList = GetJobOfferSkillList();

            var mockJobOfferSkillRepository = new Mock<IJobOfferSkillRepository>();

            mockJobOfferSkillRepository.Setup(repo => repo.GetAllAsync())
                                            .ReturnsAsync(jobOfferSkillList);

            mockJobOfferSkillRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<string>()))
                                            .ReturnsAsync((string id) =>
                                            {
                                                return jobOfferSkillList.FirstOrDefault(x => x.Id == id);
                                            });

            mockJobOfferSkillRepository.Setup(repo => repo.AddAsync(It.IsAny<JobOfferSkill>())).ReturnsAsync((JobOfferSkill entity) =>
            {
                jobOfferSkillList.Add(new JobOfferSkill((jobOfferSkillList.Count + 1).ToString(), "NewContent", entity.JobOfferId));
                return entity;
            });

            mockJobOfferSkillRepository.Setup(repo => repo.UpdateAsync(It.IsAny<JobOfferSkill>())).Callback<JobOfferSkill>((entity) =>
            {
                jobOfferSkillList.Remove(entity);
                jobOfferSkillList.Add(entity);
            });

            mockJobOfferSkillRepository.Setup(repo => repo.DeleteAsync(It.IsAny<JobOfferSkill>())).Callback<JobOfferSkill>((entity) =>
            {
                jobOfferSkillList.Remove(entity);
            });

            return mockJobOfferSkillRepository;
        }

        private static List<JobOfferSkill> GetJobOfferSkillList()
        {
            return new List<JobOfferSkill>()
            {
                new JobOfferSkill("1", "JobOfferSkill1", "1"),
                new JobOfferSkill("2", "JobOfferSkill2", "1")
            };
        }
    }
}
