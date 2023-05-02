using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestFullPostgre.Message;
using RestFullPostgre.Models;
using RestFullPostgre.Services;

namespace RestFullPostgre.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SquadRelatedController : ControllerBase
    {
        private readonly ISquadRelatedService _service;

        public SquadRelatedController(ISquadRelatedService service)
        {
            _service = service;
        }

        [HttpPost("CreateSquadRelatedParam")]
        public async Task<ActionResult> CreateSquadRelated([FromBody] SquadRelated entity)
        {
            var res = await _service.CreateSquadRelated(entity);

            return Ok(new DefaultMessage { isSuccess = true, statusCode = StatusCodes.Status200OK, message = "Squad related created" });
        }

        [HttpPost("GetAllSquadRelatedParam")]
        public async Task<ActionResult> GetAllSquadRelated()
        {
            var res = await _service.GetAllSquadRelated();

            return Ok(new PayloadMessage { isSuccess = true, statusCode = StatusCodes.Status200OK, message = "Squad related found", payload = res.data });
        }
    }
}
