using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Map.Creator
{
    [XmlRoot("MapCollection")]
    public class CellMapXMLContainer
    {
        [XmlAttribute("size")]
        public int size;

        [XmlArray("Cells")]
        [XmlArrayItem("Cell")]
        public List<CellXML> tiles = new List<CellXML>();
    }
}
