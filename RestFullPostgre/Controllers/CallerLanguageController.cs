using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestFullPostgre.Message;
using RestFullPostgre.Models;
using RestFullPostgre.Services;

namespace RestFullPostgre.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CallerLanguageController : ControllerBase
    {
        private readonly ICallerLanguageService _service;

        public CallerLanguageController(ICallerLanguageService service)
        {
            _service = service;
        }

        [HttpPost("CreateCallerLanguageParam")]
        public async Task<ActionResult> CreateCallerLanguage([FromBody] CallerLanguage entity)
        {
            var res = await _service.CreateCallerLanguage(entity);

            return Ok(new DefaultMessage { isSuccess = true, statusCode = StatusCodes.Status200OK, message = "Caller language created" });
        }

        [HttpPost("GetAllCallerLanguageParam")]
        public async Task<ActionResult> GetAllCallerLanguage()
        {
            var res = await _service.GetAllCallerLanguage();

            return Ok(new PayloadMessage { isSuccess = true, statusCode = StatusCodes.Status200OK, message = "Caller language found", payload = res.data });
        }

    }
}
