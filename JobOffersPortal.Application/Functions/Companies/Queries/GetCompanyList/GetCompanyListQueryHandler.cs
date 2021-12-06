using AutoMapper;
using JobOffersPortal.Application.Common.Interfaces.Persistance;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace JobOffersPortal.Application.Functions.Companies.Queries.GetCompanyList
{
    public class GetCompanyListQueryHandler : IRequestHandler<GetCompanyListQuery, List<CompanyListViewModel>>
    {
        private readonly IMapper _mapper;
        private readonly ICompanyRepository _companyRepository;

        public GetCompanyListQueryHandler(ICompanyRepository companyRepository, IMapper mapper)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
        }

        public async Task<List<CompanyListViewModel>> Handle(GetCompanyListQuery request, CancellationToken cancellationToken)
        {
            var entities = (await _companyRepository.GetAllAsync()).OrderBy(x => x.Name);

            var response = _mapper.Map<List<CompanyListViewModel>>(entities);

            return response;
        }
    }
}
