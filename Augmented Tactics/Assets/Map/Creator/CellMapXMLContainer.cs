using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace Assets.Map.Creator
{
    [XmlRoot("MapCollection")]
    public class CellMapXMLContainer
    {
        [XmlAttribute("size")]
        public int size;

        [XmlArray("Cells")]
        [XmlArrayItem("Cell")]
        public List<CellXML> cells = new List<CellXML>();
    }
}