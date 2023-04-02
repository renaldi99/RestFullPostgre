using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestFullPostgre.Dto;
using RestFullPostgre.Message;
using RestFullPostgre.Models;
using RestFullPostgre.Services;
using System.Reflection.PortableExecutable;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace RestFullPostgre.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrancodeInformationController : ControllerBase
    {
        private readonly ITrancodeInformationService _service;

        public TrancodeInformationController(ITrancodeInformationService service)
        {
            _service = service;
        }

        [HttpPost("CreateTrancodeInformation")]
        public async Task<ActionResult> CreateTrancodeInformation()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { isSuccess = false, status_code = StatusCodes.Status400BadRequest, message = "Invaid Request" });
            }

            
            XmlSerializer xml = new XmlSerializer(typeof(EdeServices));

            var xmlContent = System.IO.File.ReadAllText(@"services.xml");

            XDocument xmlDocument = XDocument.Parse(xmlContent.Replace("&", "&amp;"));

            List<string> listTrancodeName = new List<string>();
            List<Service> serviceList = new List<Service>();

            using (var read = xmlDocument.CreateReader())
            {
                var modelEde = (EdeServices)xml.Deserialize(xmlDocument.CreateReader());
                serviceList = modelEde.servicegroups.servicegroup.services.service;
            }

            foreach (var item in serviceList)
            {
                if (!listTrancodeName.Contains(item.name))
                {
                    listTrancodeName.Add(item.name);
                }
            }

            List<TrancodeInformation> trancodeInformation = new List<TrancodeInformation>();
            foreach (var trancodeName in listTrancodeName)
            {
                trancodeInformation.Add(new TrancodeInformation
                {
                    name_trancode = trancodeName,
                    type_trancode = "ede",
                    description = "mbase",
                    environment = "uat"
                });
            }

            var res = await _service.InsertListTrancode(trancodeInformation);

            return Ok(new DefaultMessage { isSuccess = true, statusCode = StatusCodes.Status200OK, message = "Trancode success created" });
        }

        [HttpPost("GetAllTrancodeName")]
        public async Task<ActionResult> GetAllTrancodeName()
        {
            var result = await _service.GetAllTrancodeName();

            return Ok(new PayloadMessage { isSuccess = result.isSuccess, statusCode = StatusCodes.Status200OK, message = result.message, payload = result.data });
        }
    }
}
