using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

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
                    cells.Add(CreateCellXml(map[i][j]));
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
                id = (int)cell.type,
                locX = (int)cell.gridPosition.x,
                locY = (int)cell.gridPosition.y
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