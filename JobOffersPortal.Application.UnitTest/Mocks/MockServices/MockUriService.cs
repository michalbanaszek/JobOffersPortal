using JobOffersPortal.Application.Common.Interfaces;
using Moq;
using System;

namespace JobOffersPortal.Application.UnitTest.Mocks.MockServices
{
    internal class MockUriService
    {
        public static Mock<IUriService> GetUriService()
        {
            var mockUriService = new Mock<IUriService>();

            mockUriService.Setup(serv => serv.Get(It.IsAny<string>(), It.IsAny<string>()))
                .Returns((string id, string controller) =>
                {
                    var uri = $"https://localhost:5001/api/{controller}/{id}";

                    return new Uri(uri);
                });

            mockUriService.Setup(serv => serv.GetAll(It.IsAny<int>(), It.IsAny<int>()))
             .Returns((int pageNumber, string pageSize) =>
             {
                 var uri = $"https://localhost:5001/?pageNumber={pageNumber}?pageSize={pageSize}";

                 return new Uri(uri);
             });

            return mockUriService;
        }
    }
}
