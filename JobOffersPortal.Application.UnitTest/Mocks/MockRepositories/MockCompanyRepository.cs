﻿using JobOffersPortal.Application.Common.Interfaces.Persistance;
using JobOffersPortal.Domain.Entities;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace JobOffersPortal.Application.UnitTest.Mocks.MockRepositories
{
    public static class MockCompanyRepository
    {
        public static Mock<ICompanyRepository> GetCompanyRepository()
        {
            var companies = GetCompanies();

            var mockCompanyRepository = new Mock<ICompanyRepository>();

            mockCompanyRepository.Setup(repo => repo.GetAllAsync())
                                                    .ReturnsAsync(companies);

            mockCompanyRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<string>()))
                                                    .ReturnsAsync((string id) =>
            {
                return companies.FirstOrDefault(x => x.Id == id);
            });

            mockCompanyRepository.Setup(repo => repo.GetByIdIncludeEntitiesAsync(It.IsAny<string>())).ReturnsAsync((string id) =>
            {
                return companies.FirstOrDefault(x => x.Id == id);
            });

            mockCompanyRepository.Setup(repo => repo.AddAsync(It.IsAny<Company>()))
                                                    .ReturnsAsync((Company company) =>
            {
                company.Id = (companies.Count + 1).ToString();
                companies.Add(company);
                return company;
            });

            mockCompanyRepository.Setup(repo => repo.DeleteAsync(It.IsAny<Company>()))
                                                    .Callback<Company>((entity) => companies.Remove(entity));

            mockCompanyRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Company>())).Callback<Company>((entity) =>
            {
                companies.Remove(entity);
                companies.Add(entity);
            });

            mockCompanyRepository.Setup(repo => repo.UserOwnsEntityAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync((string id, string userId) =>
            {
                var entity = companies.FirstOrDefault(x => x.Id == id);

                if (entity == null)
                {
                    return false;
                }

                return entity.CreatedBy != userId ? false : true;
            });

            mockCompanyRepository.Setup(repo => repo.IsNameAlreadyExistAsync(It.IsAny<string>())).ReturnsAsync((string name) =>
            {
                return companies.Any(x => x.Name.ToLower() == name.ToLower());
            });

            return mockCompanyRepository;
        }

        private static List<Company> GetCompanies()
        {
            return new List<Company>()
            {
                new Company() { Id = "1", Name = "CompanyName1", CreatedBy = "user1" },
                new Company() { Id = "2", Name = "CompanyName2", CreatedBy = "user2" },
                new Company() { Id = "3", Name = "CompanyName3", CreatedBy = "user3"  },
             };
        }
    }
}