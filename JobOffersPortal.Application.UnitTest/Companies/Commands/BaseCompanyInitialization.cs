using AutoMapper;
using JobOffersPortal.Application.Common.Interfaces.Persistance;
using JobOffersPortal.Application.Common.Mappings;
using JobOffersPortal.Application.UnitTest.Mocks;
using Moq;

namespace JobOffersPortal.Application.UnitTest.Companies.Commands
{
    public abstract class BaseCompanyInitialization
    {
        protected IMapper _mapper;
        protected readonly Mock<ICompanyRepository> _mockCompanyRepository;

        protected BaseCompanyInitialization()
        {
            _mockCompanyRepository = MockCompanyRepository.GetCompanyRepository();

            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            });

            _mapper = configurationProvider.CreateMapper();
        }
    }
}
