using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestFullPostgre.Dto;
using RestFullPostgre.Message;
using RestFullPostgre.Models;
using RestFullPostgre.Services;

namespace RestFullPostgre.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrancodeManualController : ControllerBase
    {
        private readonly ITrancodeManualService _service;

        public TrancodeManualController(ITrancodeManualService service)
        {
            _service = service;
        }

        [HttpPost("CreateTrancodeManual")]
        public async Task<ActionResult> CreateTrancodeManual([FromBody] TrancodeManual entity)
        {
            var res = await _service.InsertTrancodeManual(entity);

            return Ok(new DefaultMessage { isSuccess = true, statusCode = StatusCodes.Status200OK, message = "Trancode success created" });
        }

        [HttpPost("SearchTrancodeManual")]
        public async Task<ActionResult> SearchTrancodeManual([FromBody] SearchTrancodeManualDto search)
        {
            var res = await _service.SearchTrancodeManualBy(search);

            return Ok(new PayloadMessage { isSuccess = res.isSuccess, statusCode = StatusCodes.Status200OK, message = res.message, payload = res.data });
        }
    }
}
