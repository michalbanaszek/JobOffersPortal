using FluentAssertions;
using NetArchTest.Rules;

namespace JobOffersPortal.Architecture.Tests
{
    public class ArchitectureTests
    {
        private const string DomainNamespace = "JobOffersPortal.Domain";
        private const string ApplicationNamespace = "JobOffersPortal.Application";
        private const string ApplicationSecurityNamespace = "JobOffersPortal.Application.Security";
        private const string PersistanceNamespace = "JobOffersPortal.Persistance.EF";
        private const string InfrastructureSecurityNamespace = "JobOffersPortal.Infrastructure.Security";
        private const string APINamespace = "JobOffersPortal.API";
        private const string UINamespace = "JobOffersPortal.UI";

        [Fact]
        public void Domain_Should_Not_HaveDependencyOnOtherProjects()
        {
            // Arrange
            var assembly = typeof(JobOffersPortal.Domain.AssemblyReference).Assembly;

            var otherProjects = new[]
            {
                ApplicationNamespace,
                ApplicationSecurityNamespace,
                PersistanceNamespace,
                InfrastructureSecurityNamespace,
                APINamespace,
                UINamespace
            };

            // Act
            var testResult = Types
                .InAssembly(assembly)
                .ShouldNot()
                .HaveDependencyOnAll(otherProjects)
                .GetResult();

            // Assert
            testResult.IsSuccessful.Should().BeTrue();
        }

        [Fact]
        public void Application_Should_Not_HaveDependencyOnOtherProjects()
        {
            // Arrange
            var assembly = typeof(JobOffersPortal.Application.AssemblyReference).Assembly;

            var otherProjects = new[]
            {
                ApplicationSecurityNamespace,
                PersistanceNamespace,
                InfrastructureSecurityNamespace,
                APINamespace,
                UINamespace
            };

            // Act
            var testResult = Types
                .InAssembly(assembly)
                .ShouldNot()
                .HaveDependencyOnAll(otherProjects)
                .GetResult();

            // Assert
            testResult.IsSuccessful.Should().BeTrue();
        }

        [Fact]
        public void Handlers_Should_Have_DependencyOnDomain()
        {
            // Arrange
            var assembly = typeof(JobOffersPortal.Application.AssemblyReference).Assembly;
            
            // Act
            var testResult = Types
                .InAssembly(assembly)
                .That()
                .HaveNameEndingWith("Handler")
                .Should()
                .HaveDependencyOn(DomainNamespace)
                .GetResult();
            
            // Assert
            testResult.IsSuccessful.Should().BeTrue();
        }

        [Fact]
        public void ApplicationSecurity_Should_Not_HaveDependencyOnOtherProjects()
        {
            // Arrange
            var assembly = typeof(JobOffersPortal.Application.Security.AssemblyReference).Assembly;

            var otherProjects = new[]
            {
                PersistanceNamespace,
                InfrastructureSecurityNamespace,
                APINamespace,
                UINamespace
            };

            // Act
            var testResult = Types
                .InAssembly(assembly)
                .ShouldNot()
                .HaveDependencyOnAll(otherProjects)
                .GetResult();

            // Assert
            testResult.IsSuccessful.Should().BeTrue();
        }

        [Fact]
        public void PersistanceEF_Should_Not_HaveDependencyOnOtherProjects()
        {
            // Arrange
            var assembly = typeof(JobOffersPortal.Persistance.EF.AssemblyReference).Assembly;

            var otherProjects = new[]
            {
                InfrastructureSecurityNamespace,
                APINamespace,
                UINamespace
            };

            // Act
            var testResult = Types
                .InAssembly(assembly)
                .ShouldNot()
                .HaveDependencyOnAll(otherProjects)
                .GetResult();

            // Assert
            testResult.IsSuccessful.Should().BeTrue();
        }

        [Fact]
        public void InfranstructureSecurity_Should_Not_HaveDependencyOnOtherProjects()
        {
            // Arrange
            var assembly = typeof(JobOffersPortal.Infrastructure.Security.AssemblyReference).Assembly;

            var otherProjects = new[]
            {
                ApplicationNamespace,
                APINamespace,
                UINamespace
            };

            // Act
            var testResult = Types
                .InAssembly(assembly)
                .ShouldNot()
                .HaveDependencyOnAll(otherProjects)
                .GetResult();

            // Assert
            testResult.IsSuccessful.Should().BeTrue();
        }

        [Fact]
        public void API_Should_Not_HaveDependencyOnOtherProjects()
        {
            // Arrange
            var assembly = typeof(JobOffersPortal.API.AssemblyReference).Assembly;

            var otherProjects = new[]
            {
                ApplicationSecurityNamespace,
                UINamespace
            };

            // Act
            var testResult = Types
                .InAssembly(assembly)
                .ShouldNot()
                .HaveDependencyOnAll(otherProjects)
                .GetResult();

            // Assert
            testResult.IsSuccessful.Should().BeTrue();
        }

        [Fact]
        public void UI_Should_Not_HaveDependencyOnOtherProjects()
        {
            // Arrange
            var assembly = typeof(JobOffersPortal.API.AssemblyReference).Assembly;

            var otherProjects = new[]
            {
                DomainNamespace,
                ApplicationNamespace,
                ApplicationSecurityNamespace,
                PersistanceNamespace,
                InfrastructureSecurityNamespace,
                APINamespace,
                UINamespace
            };

            // Act
            var testResult = Types
                .InAssembly(assembly)
                .ShouldNot()
                .HaveDependencyOnAll(otherProjects)
                .GetResult();

            // Assert
            testResult.IsSuccessful.Should().BeTrue();
        }

        [Fact]
        public void Controllers_Should_HaveDependencyOnMediatR()
        {
            // Arrange
            var assembly = typeof(JobOffersPortal.API.AssemblyReference).Assembly;

            // Act
            var testResult = Types
                .InAssembly(assembly)
                .That()
                .HaveNameEndingWith("Controller")
                .Should()
                .HaveDependencyOn("MediatR")
                .GetResult();

            // Assert
            testResult.IsSuccessful.Should().BeTrue();
        }
    }
}