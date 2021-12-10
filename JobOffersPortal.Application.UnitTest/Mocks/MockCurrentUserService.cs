using JobOffersPortal.Application.Common.Interfaces;
using Moq;

namespace JobOffersPortal.Application.UnitTest.Mocks
{
    internal static class MockCurrentUserService
    {
        public static Mock<ICurrentUserService> GetCurrentUserService()
        {
            var mockCurrentUserService = new Mock<ICurrentUserService>();
            return mockCurrentUserService;
        }
    }
}
