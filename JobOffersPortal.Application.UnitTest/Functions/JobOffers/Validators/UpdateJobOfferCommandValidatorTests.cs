using FluentValidation.TestHelper;
using JobOffersPortal.Application.Common.Interfaces.Persistance;
using JobOffersPortal.Application.Functions.JobOffers.Commands.UpdateJobOffer;
using JobOffersPortal.Application.UnitTest.Mocks.MockRepositories;
using Moq;
using Xunit;

namespace JobOffersPortal.Application.UnitTest.Functions.JobOffers.Validators
{
    public class UpdateJobOfferCommandValidatorTests
    {
        private UpdateJobOfferCommandValidator _validator;
        private Mock<IJobOfferRepository> _jobOfferrepositoryMock;

        public UpdateJobOfferCommandValidatorTests()
        {
            _jobOfferrepositoryMock = MockJobOfferRepository.GetJobOffersRepository();

            _validator = new UpdateJobOfferCommandValidator(_jobOfferrepositoryMock.Object);
        }

        [Fact]
        public void Should_Have_Error_When_Position_Is_Empty()
        {
            //Arrange 
            var command = new UpdateJobOfferCommand() { Position = string.Empty };

            //Act
            var result = _validator.TestValidate(command);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Position);
        }

        [Fact]
        public void Should_Have_Error_When_Position_Is_Less_Than_2_Characters()
        {
            //Arrange 
            var command = new UpdateJobOfferCommand() { Position = new string('T', 1) };

            //Act
            var result = _validator.TestValidate(command);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Position);
        }

        [Fact]
        public void Should_Have_Error_When_Position_Is_Greater_Than_30_Characters()
        {
            //Arrange 
            var command = new UpdateJobOfferCommand() { Position = new string('T', 31) };

            //Act
            var result = _validator.TestValidate(command);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Position);
        }

        [Fact]
        public void Should_Have_Error_When_Position_Is_Invalid_Format()
        {
            //Arrange 
            var command = new UpdateJobOfferCommand() { Position = "NewCompany/" };

            //Act
            var result = _validator.TestValidate(command);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Position);
        }

        [Fact]
        public void Should_Have_Error_When_Position_Is_Already_Exists()
        {
            //Arrange 
            var command = new UpdateJobOfferCommand() { Position = "Position1" };

            //Act
            var result = _validator.TestValidate(command);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Position);
        }
    }
}
