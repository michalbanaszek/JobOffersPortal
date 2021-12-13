using AutoMapper;
using JobOffersPortal.Application.Common.Interfaces;
using JobOffersPortal.Application.Common.Interfaces.Persistance;
using JobOffersPortal.Application.Common.Mappings;
using JobOffersPortal.Application.UnitTest.Mocks;
using Moq;

namespace JobOffersPortal.Application.UnitTest.JobOffers
{
    public abstract class BaseJobOfferInitialization
    {
        protected readonly Mock<IJobOfferRepository> _mockJobOfferRepository;
        protected readonly Mock<ICurrentUserService> _currentUserServiceMock;
        protected readonly ICurrentUserService _currentUserService;
        protected readonly IMapper _mapper;

        protected BaseJobOfferInitialization()
        {
            _mockJobOfferRepository = MockJobOfferRepository.GetJobOffersRepository();

            _currentUserServiceMock = new Mock<ICurrentUserService>();

            _currentUserService = _currentUserServiceMock.Object;

            _currentUserServiceMock.SetupGet(x => x.UserId).Returns("user1");

            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            });

            _mapper = configurationProvider.CreateMapper();
        }
    }
}
