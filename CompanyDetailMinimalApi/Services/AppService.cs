using CompanyDetailMinimalApi.Contracts.Data;
using CompanyDetailMinimalApi.Contracts.Requests;
using CompanyDetailMinimalApi.Contracts.Responses;
using CompanyDetailMinimalApi.Mappings;
using CompanyDetailMinimalApi.Repositories;

namespace CompanyDetailMinimalApi.Services
{
    public class AppService : IAppService
    {
        private readonly ICompanyDetailRepository _companyDetailRepository;
        private readonly IContactRepository _contactRepository;
        public AppService(ICompanyDetailRepository companyDetailRepository, IContactRepository contactRepository)
        {
            _companyDetailRepository = companyDetailRepository;
            _contactRepository = contactRepository;
        }

        public async Task<CompanyDetailResponse> CreateAsync(CompanyDetailCreateRequest companyDetail)
        {
            var company = companyDetail.ToCompanyDetail();
            var contactList = new List<ContactDetailDto>();
            foreach (var contact in companyDetail.ContactDetails)
            {
                contactList.Add(contact.ToContactDetail(company.Id));
            }
            var res1 = await _companyDetailRepository.CreateAsync(company);
            var res2 = await _contactRepository.BatchWriteAsync(contactList);
            if(res1 == true && res2 == true)
            {
                return company.FromCompanyDto(contactList);
            }
            return null;
        }

        public Task<bool> DeleteCompanyAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteContactAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<CompanyDetailResponse> GetCompanyAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ContactDetailResponse> GetContactAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateCompanyAsync(CompanyDetailCreateRequest company)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateContactAsync(ContactDetailRequest contact)
        {
            throw new NotImplementedException();
        }
    }
}
