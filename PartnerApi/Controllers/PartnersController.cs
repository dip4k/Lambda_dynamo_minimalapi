using Microsoft.AspNetCore.Mvc;

using PartnerApi.Sevices;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PartnerApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PartnersController : ControllerBase
    {
        private readonly IAppservice _appservice;

        public PartnersController(IAppservice appservice)
        {
            _appservice = appservice;
        }

        // GET: api/<PartnersController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _appservice.GetAllPartnersAsync();
            return Ok(response);
        }

        // GET api/<PartnersController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

       
    }
}
