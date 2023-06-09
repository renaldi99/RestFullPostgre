﻿using Microsoft.AspNetCore.Http;
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
        private readonly ILogger<TrancodeManualController> _logger;

        public TrancodeManualController(ITrancodeManualService service, ILogger<TrancodeManualController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpPost("CreateTrancodeManual")]
        public async Task<ActionResult> CreateTrancodeManual([FromBody] TrancodeManual entity)
        {
            var res = await _service.InsertTrancodeManual(entity);

            if (res == 2)
            {
                return Ok(new DefaultMessage { isSuccess = true, statusCode = StatusCodes.Status200OK, message = "Trancode success updated" });
            }

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
