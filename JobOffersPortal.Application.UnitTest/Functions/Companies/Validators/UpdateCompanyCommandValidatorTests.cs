using FluentValidation.TestHelper;
using JobOffersPortal.Application.Common.Interfaces.Persistance;
using JobOffersPortal.Application.Functions.Companies.Commands.UpdateCompany;
using JobOffersPortal.Application.UnitTest.Mocks.MockRepositories;
using Moq;
using Xunit;

namespace JobOffersPortal.Application.UnitTest.Functions.Companies.Validators
{
    public class UpdateCompanyCommandValidatorTests
    {
        private UpdateCompanyCommandValidator _validator;
        private Mock<ICompanyRepository> _companyRepositoryMock;

        public UpdateCompanyCommandValidatorTests()
        {
            _companyRepositoryMock = MockCompanyRepository.GetCompanyRepository();

            _validator = new UpdateCompanyCommandValidator(_companyRepositoryMock.Object);
        }

        [Fact]
        public void Should_Have_Error_When_Name_Is_Empty()
        {
            //Arrange 
            var command = new UpdateCompanyCommand() { Name = string.Empty };

            //Act
            var result = _validator.TestValidate(command);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void Should_Have_Error_When_Name_Is_Less_Than_2_Characters()
        {
            //Arrange 
            var command = new UpdateCompanyCommand() { Name = new string('T', 1) };

            //Act
            var result = _validator.TestValidate(command);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void Should_Have_Error_When_Name_Is_Greater_Than_30_Characters()
        {
            //Arrange 
            var command = new UpdateCompanyCommand() { Name = new string('T', 31) };

            //Act
            var result = _validator.TestValidate(command);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void Should_Have_Error_When_Name_Is_Invalid_Format()
        {
            //Arrange 
            var command = new UpdateCompanyCommand() { Name = "Test/" };

            //Act
            var result = _validator.TestValidate(command);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void Should_Have_Error_When_Name_Is_Already_Exists()
        {
            //Arrange 
            var command = new UpdateCompanyCommand() { Name = "CompanyName1" };

            //Act
            var result = _validator.TestValidate(command);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }
    }
}
