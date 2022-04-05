using JobOffersPortal.Application.Common.Interfaces;
using Moq;

namespace JobOffersPortal.Application.UnitTest.Mocks.MockServices
{
    internal class MockCurrentUserService
    {
        public static Mock<ICurrentUserService> GetCurrentUserService()
        {
            var mockCurrentUserService = new Mock<ICurrentUserService>();

            mockCurrentUserService.SetupGet(x => x.UserId).Returns("user1");

            return mockCurrentUserService;
        }
    }
}
