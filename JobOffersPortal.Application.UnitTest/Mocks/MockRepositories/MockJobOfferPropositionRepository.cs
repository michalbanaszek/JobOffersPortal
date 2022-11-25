using JobOffersPortal.Application.Common.Interfaces.Persistance;
using JobOffersPortal.Domain.Entities;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace JobOffersPortal.Application.UnitTest.Mocks.MockRepositories
{
    public static class MockJobOfferPropositionRepository
    {
        public static Mock<IJobOfferPropositionRepository> GetJobOfferPropositionRepository()
        {
            var jobOfferPropositionList = GetJobOfferPropositionList();

            var mockJobOfferPropositionRepository = new Mock<IJobOfferPropositionRepository>();

            mockJobOfferPropositionRepository.Setup(repo => repo.GetAllAsync())
                                             .ReturnsAsync(jobOfferPropositionList);

            mockJobOfferPropositionRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<string>()))
                                             .ReturnsAsync((string id) =>
            {
                return jobOfferPropositionList.FirstOrDefault(x => x.Id == id);
            });

            mockJobOfferPropositionRepository.Setup(repo => repo.AddAsync(It.IsAny<JobOfferProposition>()))
                                             .ReturnsAsync((JobOfferProposition entity) =>
                                             {
                                                 jobOfferPropositionList.Add(new JobOfferProposition((jobOfferPropositionList.Count + 1).ToString(), "NewContent", entity.JobOfferId));
                                                 return entity;
                                             });
            mockJobOfferPropositionRepository.Setup(repo => repo.UpdateAsync(It.IsAny<JobOfferProposition>()))
                                             .Callback<JobOfferProposition>((entity) =>
                                             {
                                                 jobOfferPropositionList.Remove(entity);
                                                 jobOfferPropositionList.Add(entity);
                                             });
            mockJobOfferPropositionRepository.Setup(repo => repo.DeleteAsync(It.IsAny<JobOfferProposition>()))
                                             .Callback<JobOfferProposition>((entity) =>
            {
                jobOfferPropositionList.Remove(entity);
            });

            return mockJobOfferPropositionRepository;
        }

        private static List<JobOfferProposition> GetJobOfferPropositionList()
        {
            return new List<JobOfferProposition>()
            {
                new JobOfferProposition("1", "Proposition1", "1"),
                new JobOfferProposition("2", "Proposition2", "1")
            };
        }
    }
}
