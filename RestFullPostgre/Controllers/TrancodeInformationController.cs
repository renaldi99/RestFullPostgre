using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestFullPostgre.Dto;
using RestFullPostgre.Helpers;
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

        [HttpPost("CreateTrancodeInformationv2")]
        public async Task<ActionResult> CreateTrancodeInformationv2()
        {
            string filePath = "services.xml";

            XmlDocument xmlDoc = new XmlDocument();
            var read = System.IO.File.ReadAllText(filePath);
            var encode = Utility.EncodeAmpersand(read);
            xmlDoc.LoadXml(encode);

            string xpathExpression = "//edeservices/servicegroups/servicegroup/services/service";

            XmlNodeList nodes = xmlDoc.SelectNodes(xpathExpression);

            List<string> listTrancodeService = new List<string>();

            foreach (XmlNode node in nodes)
            {
                if (node.Attributes != null)
                {
                    XmlAttribute attribute = node.Attributes["name"];
                    if (attribute != null)
                    {
                        string attributeValue = attribute.Value;
                        listTrancodeService.Add(attributeValue);
                    }
                }
            }

            return Ok(new DefaultMessage { isSuccess = true, statusCode = StatusCodes.Status200OK, message = "Trancode success created" });
        }

        [HttpPost("GetAllTrancodeName")]
        public async Task<ActionResult> GetAllTrancodeName()
        {
            var result = await _service.GetAllTrancodeName();

            return Ok(new PayloadMessage { isSuccess = result.isSuccess, statusCode = StatusCodes.Status200OK, message = result.message, payload = result.data });
        }

        [HttpPost("SearchTrancodeInformationJoined")]
        public async Task<ActionResult> SearchTrancodeInformation([FromBody] SearchTrancodeAttributesDto entity)
        {
            var result = await _service.SearchTrancodeInformation(entity);

            return Ok(new PayloadMessage { isSuccess = true, statusCode = StatusCodes.Status200OK, message = "Trancode found", payload = result });
        }

        [HttpPost("SearchTrancodeInformation")]
        public async Task<ActionResult> SearchTrancodeInformation([FromBody] SearchTrancodeInformationDto entity)
        {
            var result = await _service.SearchTrancodeInformation(entity);

            return Ok(new PayloadMessage { isSuccess = true, statusCode = StatusCodes.Status200OK, message = "Trancode found", payload = result });
        }
    }
}
