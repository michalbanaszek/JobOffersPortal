using AutoMapper;
using JobOffersPortal.Application.Common.Interfaces;
using JobOffersPortal.Application.Common.Interfaces.Persistance;
using JobOffersPortal.Application.Common.Mappings;
using JobOffersPortal.Application.UnitTest.Mocks.MockRepositories;
using Moq;

namespace JobOffersPortal.Application.UnitTest.Companies.Commands
{
    public abstract class BaseCompanyInitialization
    {
        protected readonly Mock<ICompanyRepository> _mockCompanyRepository;
        protected readonly Mock<ICurrentUserService> _currentUserServiceMock;
        protected readonly ICurrentUserService _currentUserService;
        protected readonly IMapper _mapper;

        protected BaseCompanyInitialization()
        {
            _mockCompanyRepository = MockCompanyRepository.GetCompanyRepository();

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
