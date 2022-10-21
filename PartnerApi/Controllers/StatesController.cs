using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PartnerApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StatesController : ControllerBase
    {
        // GET: api/<StatesController>
        [HttpGet]
        public IActionResult Get()
        {
            var stateMap = UsStateList.stateList.Select(x=>new {value=x,label=x}).ToList();
            return new JsonResult(stateMap) { StatusCode= StatusCodes.Status200OK };
        }

       
    }
}
