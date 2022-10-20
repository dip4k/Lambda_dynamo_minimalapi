using CompanyDetailMinimalApi.Contracts.Requests;
using CompanyDetailMinimalApi.Contracts.Responses;
using CompanyDetailMinimalApi.Services;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace CompanyDetailMinimalApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CompanyDetailController : ControllerBase
    {
        private readonly IAppService _appService;
        public CompanyDetailController(IAppService appService)
        {
            _appService = appService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCompany(CompanyDetailCreateRequest request)
        {
            var response = await _appService.CreateAsync(request);
            if (response == null) return NotFound("Write operation failed");
            return Ok(response);
        }
    }
}
