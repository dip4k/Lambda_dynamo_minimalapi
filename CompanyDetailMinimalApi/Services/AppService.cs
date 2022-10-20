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
            company.Contacts = string.Join(",", contactList.Select(x => x.Id));
            var res1 = await _companyDetailRepository.CreateAsync(company);
            var res2 = await _contactRepository.BatchWriteAsync(contactList);
            if(res1 == true && res2 == true)
            {
                return company.FromCompanyDto(contactList);
            }
            return null;
        }

        public async Task<bool> DeleteCompanyAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteContactAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<CompanyDetailResponse> GetCompanyAsync(Guid id)
        {
            var res = await _companyDetailRepository.GetAsync(id);
            if (res == null) return null;
            var contactIds = res.Contacts.Split(",").Select(x=>Guid.Parse(x)).ToList();
            var contactList = await _contactRepository.BatchGetAsync(id, contactIds);
            
            return res.FromCompanyDto(contactList);
        }

        public async Task<ContactDetailResponse> GetContactAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateCompanyAsync(CompanyDetailCreateRequest company)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateContactAsync(ContactDetailRequest contact)
        {
            throw new NotImplementedException();
        }
    }
}
