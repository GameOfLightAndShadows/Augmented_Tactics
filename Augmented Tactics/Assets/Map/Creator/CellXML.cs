using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Map.Creator
{
    public class CellXML
    {
        [XmlAttribute("id")]
        public int id;

        [XmlAttribute("locX")]
        public int locX;

        [XmlAttribute("locY")]
        public int locY;
    }
}
