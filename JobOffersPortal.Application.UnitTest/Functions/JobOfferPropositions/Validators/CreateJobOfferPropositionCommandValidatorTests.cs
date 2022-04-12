using FluentValidation.TestHelper;
using JobOffersPortal.Application.Functions.JobOfferPropositions.Commands.CreateJobOfferProposition;
using Xunit;

namespace JobOffersPortal.Application.UnitTest.Functions.JobOfferPropositions.Validators
{
    public class CreateJobOfferPropositionCommandValidatorTests
    {
        private CreateJobOfferPropositionCommandValidator _validator;

        public CreateJobOfferPropositionCommandValidatorTests()
        {
            _validator = new CreateJobOfferPropositionCommandValidator();
        }

        [Fact]
        public void Should_Have_Error_When_Content_Is_Null()
        {
            //Arrange 
            var command = new CreateJobOfferPropositionCommand() { Content = null };

            //Act
            var result = _validator.TestValidate(command);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Content);
        }

        [Fact]
        public void Should_Have_Error_When_Content_Is_Empty()
        {
            //Arrange 
            var command = new CreateJobOfferPropositionCommand() { Content = string.Empty };

            //Act
            var result = _validator.TestValidate(command);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Content);
        }

        [Fact]
        public void Should_Have_Error_When_Content_Is_Less_Than_2_Characters()
        {
            //Arrange 
            var command = new CreateJobOfferPropositionCommand() { Content = new string('T', 1) };

            //Act
            var result = _validator.TestValidate(command);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Content);
        }

        [Fact]
        public void Should_Have_Error_When_Content_Is_Greater_Than_50_Characters()
        {
            //Arrange 
            var command = new CreateJobOfferPropositionCommand() { Content = new string('T', 51) };

            //Act
            var result = _validator.TestValidate(command);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Content);
        }

        [Fact]
        public void Should_Have_Error_When_Content_Is_Invalid_Format()
        {
            //Arrange 
            var command = new CreateJobOfferPropositionCommand() { Content = "Test/" };

            //Act
            var result = _validator.TestValidate(command);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Content);
        }
    }
}
