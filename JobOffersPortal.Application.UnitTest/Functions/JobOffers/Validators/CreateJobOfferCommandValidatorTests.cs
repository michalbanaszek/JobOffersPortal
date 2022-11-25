using FluentValidation.TestHelper;
using JobOffersPortal.Application.Common.Interfaces.Persistance;
using JobOffersPortal.Application.Functions.JobOffers.Commands.CreateJobOffer;
using JobOffersPortal.Application.UnitTest.Mocks.MockRepositories;
using Moq;
using Xunit;

namespace JobOffersPortal.Application.UnitTest.Functions.JobOffers.Validators
{
    public class CreateJobOfferCommandValidatorTests
    {
        private CreateJobOfferCommandValidator _validator;
        private Mock<IJobOfferRepository> _jobOfferrepositoryMock;

        public CreateJobOfferCommandValidatorTests()
        {
            _jobOfferrepositoryMock = MockJobOfferRepository.GetJobOffersRepository();

            _validator = new CreateJobOfferCommandValidator(_jobOfferrepositoryMock.Object);
        }

        [Fact]
        public void Should_Have_Error_When_Position_Is_Empty()
        {
            //Arrange 
            var command = new CreateJobOfferCommand() { Position = string.Empty };

            //Act
            var result = _validator.TestValidate(command);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Position);
        }

        [Fact]
        public void Should_Have_Error_When_Position_Is_Less_Than_2_Characters()
        {
            //Arrange 
            var command = new CreateJobOfferCommand() { Position = new string('T', 1) };

            //Act
            var result = _validator.TestValidate(command);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Position);
        }

        [Fact]
        public void Should_Have_Error_When_Position_Is_Greater_Than_30_Characters()
        {
            //Arrange 
            var command = new CreateJobOfferCommand() { Position = new string('T', 31) };

            //Act
            var result = _validator.TestValidate(command);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Position);
        }

        [Fact]
        public void Should_Have_Error_When_Position_Is_Invalid_Format()
        {
            //Arrange 
            var command = new CreateJobOfferCommand() { Position = "Test/" };

            //Act
            var result = _validator.TestValidate(command);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Position);
        }

        [Fact]
        public void Should_Have_Error_When_CompanyId_Is_Empty()
        {
            //Arrange
            var command = new CreateJobOfferCommand() { CompanyId = string.Empty };

            //Act
            var result = _validator.TestValidate(command);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.CompanyId);
        }

        [Fact]
        public void Should_Have_Error_When_CompanyId_Is_Null()
        {
            //Arrange
            var command = new CreateJobOfferCommand() { CompanyId = null };

            //Act
            var result = _validator.TestValidate(command);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.CompanyId);
        }

        [Fact]
        public void Should_Have_Error_When_Requirements_Is_Null()
        {
            //Arrange
            var command = new CreateJobOfferCommand() { Requirements = null };

            //Act
            var result = _validator.TestValidate(command);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Requirements);
        }

        [Fact]
        public void Should_Have_Error_When_Skills_Is_Null()
        {
            //Arrange
            var command = new CreateJobOfferCommand() { Skills = null };

            //Act
            var result = _validator.TestValidate(command);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Skills);
        }

        [Fact]
        public void Should_Have_Error_When_Propositions_Is_Null()
        {
            //Arrange
            var command = new CreateJobOfferCommand() { Propositions = null };

            //Act
            var result = _validator.TestValidate(command);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Propositions);
        }
    }
}
