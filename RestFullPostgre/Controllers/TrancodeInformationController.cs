using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using RestFullPostgre.Dto;
using RestFullPostgre.Helpers;
using RestFullPostgre.Message;
using RestFullPostgre.Models;
using RestFullPostgre.Services;
using Sprache;
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
        private readonly Pageable<TrancodeInformation> _pageable = new Pageable<TrancodeInformation>();
        private readonly ILogger<TrancodeManualController> _logger;
        private readonly IMemoryCache _cache;


        public TrancodeInformationController(ITrancodeInformationService service, ILogger<TrancodeManualController> logger, IMemoryCache cache)
        {
            _service = service;
            _logger = logger;
            _cache = cache;
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
        public ActionResult CreateTrancodeInformationv2()
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

        [HttpPost("[action]")]
        public async Task<ActionResult> GetTrancodeInformation([FromQuery] ParameterPage param)
        {
            var result = await _service.GetTrancodeInformation();
            // pagination with extension method simple
            //var listCurrentData = result.Pagination<TrancodeInformation>(param.page, param.size);

            // create paging
            var page = _pageable.ToPageableList(result, param.page, param.size);


            return Ok(new PayloadMessage { isSuccess = true, statusCode = StatusCodes.Status200OK, message = "Trancode found", payload = page });

        }

        [HttpPost("GetAllTrancodeName")]
        public async Task<ActionResult> GetAllTrancodeName()
        {
            if (_cache.TryGetValue("AllTrancodeName", out List<TrancodeNameDto> listTrancodeName))
            {
                listTrancodeName = (List<TrancodeNameDto>)_cache.Get("AllTrancodeName");

                _logger.Log(LogLevel.Information, "list found in cache.");

                return Ok(new PayloadMessage { isSuccess = true, statusCode = StatusCodes.Status200OK, message = "Trancode name successfully found", payload = listTrancodeName });
            }
            
            var getAllTrancodeName = await _service.GetAllTrancodeName();

            var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(60))
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
                    .SetPriority(CacheItemPriority.Normal)
                    .SetSize(1024);

            _cache.Set("AllTrancodeName", getAllTrancodeName, cacheEntryOptions);

            return Ok(new PayloadMessage { isSuccess = true, statusCode = StatusCodes.Status200OK, message = "Trancode name successfully found", payload = getAllTrancodeName });
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
