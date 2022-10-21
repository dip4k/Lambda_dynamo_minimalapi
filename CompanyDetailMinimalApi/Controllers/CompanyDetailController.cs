using CompanyDetailMinimalApi.Contracts.Requests;
using CompanyDetailMinimalApi.Services;

using Microsoft.AspNetCore.Mvc;

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
            return new JsonResult(response)
            {
                StatusCode = StatusCodes.Status201Created // Status code here 
            };
        }

        [HttpGet("{id:guid}")]

        public async Task<IActionResult> GetCompany(Guid id)
        {
            var response = await _appService.GetCompanyAsync(id);
            if (response == null) return NotFound("No Element Found");
            return new JsonResult(response)
            {
                StatusCode = StatusCodes.Status200OK // Status code here 
            };
        }
    }
}
