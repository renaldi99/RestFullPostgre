using DotNetEnv;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RestFullPostgre.Controllers
{
    [ApiController]
    public class ReadyController : ControllerBase
    {

        [HttpGet("[action]")]
        public ActionResult Ready()
        {
            Env.Load();
            Env.TraversePath().Load();

            var check = System.Environment.GetEnvironmentVariable("LOAD_STRING");

            return Ok(check);
        }
    }
}
