using FluentValidation.TestHelper;
using JobOffersPortal.Application.Functions.JobOfferRequirements.Commands.CreateJobOfferRequirement;
using Xunit;

namespace JobOffersPortal.Application.UnitTest.Functions.JobOfferRequirements.Validators
{
    public class CreateJobOfferRequirementCommandValidatorTests
    {
        private CreateJobOfferRequirementCommandValidator _validator;

        public CreateJobOfferRequirementCommandValidatorTests()
        {
            _validator = new CreateJobOfferRequirementCommandValidator();
        }

        [Fact]
        public void Should_Have_Error_When_Content_Is_Null()
        {
            //Arrange 
            var command = new CreateJobOfferRequirementCommand() { Content = null };

            //Act
            var result = _validator.TestValidate(command);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Content);
        }

        [Fact]
        public void Should_Have_Error_When_Content_Is_Empty()
        {
            //Arrange 
            var command = new CreateJobOfferRequirementCommand() { Content = string.Empty };

            //Act
            var result = _validator.TestValidate(command);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Content);
        }

        [Fact]
        public void Should_Have_Error_When_Content_Is_Less_Than_2_Characters()
        {
            //Arrange 
            var command = new CreateJobOfferRequirementCommand() { Content = new string('T', 1) };

            //Act
            var result = _validator.TestValidate(command);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Content);
        }

        [Fact]
        public void Should_Have_Error_When_Content_Is_Greater_Than_50_Characters()
        {
            //Arrange 
            var command = new CreateJobOfferRequirementCommand() { Content = new string('T', 51) };

            //Act
            var result = _validator.TestValidate(command);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Content);
        }

        [Fact]
        public void Should_Have_Error_When_Content_Is_Invalid_Format()
        {
            //Arrange 
            var command = new CreateJobOfferRequirementCommand() { Content = "Test/" };

            //Act
            var result = _validator.TestValidate(command);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Content);
        }
    }
}