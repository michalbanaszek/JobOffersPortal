using AutoMapper;
using JobOffersPortal.Application.Common.Interfaces;
using JobOffersPortal.Application.Common.Interfaces.Persistance;
using JobOffersPortal.Application.Common.Mappings;
using JobOffersPortal.Application.UnitTest.Mocks.MockRepositories;
using Moq;

namespace JobOffersPortal.Application.UnitTest.JobOfferPropositions
{
    public abstract class BaseJobOfferPropositionInitialization
    {
        protected readonly Mock<IJobOfferPropositionRepository> _mockJobOfferPropositionRepository;
        protected readonly Mock<IJobOfferRepository> _mockJobOfferRepository;
        protected readonly Mock<ICurrentUserService> _currentUserServiceMock;
        protected readonly ICurrentUserService _currentUserService;
        protected readonly IMapper _mapper;

        protected BaseJobOfferPropositionInitialization()
        {
            _mockJobOfferPropositionRepository = MockJobOfferPropositionRepository.GetJobOfferPropositionRepository();

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
