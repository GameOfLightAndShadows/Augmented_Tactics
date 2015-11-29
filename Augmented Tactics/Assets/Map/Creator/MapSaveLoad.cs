using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
namespace Assets.Map.Creator
{
    public static class MapSaveLoad
    {
        public static CellMapXMLContainer CreateMapContainer(List<List<Cell>> map)
        {

            List<CellXML> cells = new List<CellXML>();

            for (int i = 0; i < map.Count; i++)
            {
                for (int j = 0; j < map.Count; j++)
                {
                    cells.Add(MapSaveLoad.CreateCellXml(map[i][j]));
                }
            }

            return new CellMapXMLContainer()
            {
                size = map.Count,
                cells = cells
            };
        }

        public static CellXML CreateCellXml(Cell cell)
        {
            return new CellXML()
            {
               // id = (int)tile.type,
                locX = (int)cell.Coordinates.x,
                locY = (int)cell.Coordinates.y
            };
        }

        public static void Save(CellMapXMLContainer mapContainer, string filename)
        {
            var serializer = new XmlSerializer(typeof(CellMapXMLContainer));
            using (var stream = new FileStream(filename, FileMode.Create))
            {
                serializer.Serialize(stream, mapContainer);
            }
        }

        public static CellMapXMLContainer Load(string filename)
        {
            var serializer = new XmlSerializer(typeof(CellMapXMLContainer));
            using (var stream = new FileStream(filename, FileMode.Open))
            {
                return serializer.Deserialize(stream) as CellMapXMLContainer;
            }
        }
    }
}
