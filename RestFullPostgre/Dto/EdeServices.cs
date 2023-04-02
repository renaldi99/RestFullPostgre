using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace RestFullPostgre.Dto
{
    [Serializable]
    [XmlRoot("edeservices")]
    public class EdeServices
    {
        [XmlAttribute("name")]
        public string? name { get; set; }
        [XmlElement("servicegroups")]
        public ServiceGroups? servicegroups { get; set; }
    }

    public class ServiceGroups
    {
        [XmlElement("servicegroup")]
        public ServiceGroup? servicegroup { get; set; }
    }

    public class ServiceGroup
    {
        [XmlElement("services")]
        public Services? services { get; set; }
    }

    public class Services
    {
        [XmlElement("service")]
        public List<Service>? service { get; set; }
    }

    public class Service
    {
        [XmlAttribute("name")]
        public string? name { get; set; }
    }
}
