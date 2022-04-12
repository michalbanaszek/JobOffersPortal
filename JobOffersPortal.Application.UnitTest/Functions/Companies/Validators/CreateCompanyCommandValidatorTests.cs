using FluentValidation.TestHelper;
using JobOffersPortal.Application.Common.Interfaces.Persistance;
using JobOffersPortal.Application.Functions.Companies.Commands.CreateCompany;
using JobOffersPortal.Application.UnitTest.Mocks.MockRepositories;
using Moq;
using Xunit;

namespace JobOffersPortal.Application.UnitTest.Functions.Companies.Validators
{
    public class CreateCompanyCommandValidatorTests
    {
        private CreateCompanyCommandValidator _validator;
        private Mock<ICompanyRepository> _companyRepositoryMock;

        public CreateCompanyCommandValidatorTests()
        {
            _companyRepositoryMock = MockCompanyRepository.GetCompanyRepository();

            _validator = new CreateCompanyCommandValidator(_companyRepositoryMock.Object);
        }

        [Fact]
        public void Should_Have_Error_When_Name_Is_Empty()
        {
            //Arrange 
            var command = new CreateCompanyCommand() { Name = string.Empty };

            //Act
            var result = _validator.TestValidate(command);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void Should_Have_Error_When_Name_Is_Less_Than_2_Characters()
        {
            //Arrange 
            var command = new CreateCompanyCommand() { Name = new string('T', 1) };

            //Act
            var result = _validator.TestValidate(command);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void Should_Have_Error_When_Name_Is_Greater_Than_30_Characters()
        {
            //Arrange 
            var command = new CreateCompanyCommand() { Name = new string('T', 31) };

            //Act
            var result = _validator.TestValidate(command);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void Should_Have_Error_When_Name_Is_Invalid_Format()
        {
            //Arrange 
            var command = new CreateCompanyCommand() { Name = "Test/" };

            //Act
            var result = _validator.TestValidate(command);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void Should_Have_Error_When_Name_Is_Already_Exists()
        {
            //Arrange 
            var command = new CreateCompanyCommand() { Name = "CompanyName1" };

            //Act
            var result = _validator.TestValidate(command);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }
    }
}
