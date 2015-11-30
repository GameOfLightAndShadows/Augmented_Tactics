using Assets.Map;
using Assets.Map.Creator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
namespace Assets.Managers
{
    public class MapCreatorManager : MonoBehaviour
    {
        public static MapCreatorManager instance;
        public int mapSize;
        public List<List<Cell>> map = new List<List<Cell>>();
        Transform mapTransform;
        public CellType palletSelection = CellType.Normal;


        void Awake()
        {
            instance = this;

            mapTransform = transform.FindChild("Map");

            generateBlankMap(32);
        }

    void generateBlankMap(int mSize) {
		mapSize = mSize;

		//initially remove all children
		for(int i = 0; i < mapTransform.childCount; i++) {
			Destroy (mapTransform.GetChild(i).gameObject);
		}

		map = new List<List<Cell>>();
		for (int i = 0; i < mapSize; i++) {
			List <Cell> row = new List<Cell>();
			for (int j = 0; j < mapSize; j++) {
                Cell cell = ((GameObject)Instantiate(PrefabHolder.instance.BASE_TILE_PREFAB, new Vector3(i - Mathf.Floor(mapSize / 2), 0, -j + Mathf.Floor(mapSize / 2)), Quaternion.Euler(new Vector3()))).GetComponent<Cell>();
				cell.transform.parent = mapTransform;
				cell.gridPosition = new Vector2(i, j);
				cell.type = (CellType.Normal);
				row.Add (cell);
			}
			map.Add(row);
		}
	}

	void loadMapFromXml() {
		CellMapXMLContainer container = MapSaveLoad.Load("map.xml");

		mapSize = container.size;
		
		//initially remove all children
		for(int i = 0; i < mapTransform.childCount; i++) {
			Destroy (mapTransform.GetChild(i).gameObject);
		}

		map = new List<List<Cell>>();
		for (int i = 0; i < mapSize; i++) {
			List <Cell> row = new List<Cell>();
			for (int j = 0; j < mapSize; j++) {
                Cell cell = ((GameObject)Instantiate(PrefabHolder.instance.BASE_TILE_PREFAB, new Vector3(i - Mathf.Floor(mapSize/2),0, -j + Mathf.Floor(mapSize/2)), Quaternion.Euler(new Vector3()))).GetComponent<Cell>();
				cell.transform.parent = mapTransform;
				cell.gridPosition = new Vector2(i, j);
				cell.type = (CellType)(container.cells.Where(x => x.locX == i && x.locY == j).First().id);
				row.Add (cell);
			}
			map.Add(row);
		}
	}

	void saveMapToXml() {
		MapSaveLoad.Save(MapSaveLoad.CreateMapContainer(map), "map.xml");
        }

    }
}
