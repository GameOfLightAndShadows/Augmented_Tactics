using System.Collections.Generic;
using Assets.Map.Creator;
using UnityEngine;
using Debug = System.Diagnostics.Debug;

namespace Assets.Managers
{
    public class TilesManager : MonoBehaviour
    {
        private int mapSize = 32;
        private Transform mapTransform;

        public List<List<Tile>> map = new List<List<Tile>>();
        void generateMap()
        {
            loadMapFromXml();

            //		map = new List<List<Tile>>();
            //		for (int i = 0; i < mapSize; i++) {
            //			List <Tile> row = new List<Tile>();
            //			for (int j = 0; j < mapSize; j++) {
            //				Tile tile = ((GameObject)Instantiate(TilePrefab, new Vector3(i - Mathf.Floor(mapSize/2),0, -j + Mathf.Floor(mapSize/2)), Quaternion.Euler(new Vector3()))).GetComponent<Tile>();
            //				tile.gridPosition = new Vector2(i, j);
            //				row.Add (tile);
            //			}
            //			map.Add(row);
            //		}
        }

        void loadMapFromXml()
        {
            MapXmlContainer container = MapSaveLoad.Load("map.xml");

            mapSize = container.size;

            //initially remove all children
            for (int i = 0; i < mapTransform.childCount; i++)
            {
                Destroy(mapTransform.GetChild(i).gameObject);
            }

            map = new List<List<Tile>>();
            for (int i = 0; i < mapSize; i++)
            {
                List<Tile> row = new List<Tile>();
                for (int j = 0; j < mapSize; j++)
                {
                    Tile tile = ((GameObject)Instantiate(PrefabHolder.instance.BASE_TILE_PREFAB, new Vector3(i - Mathf.Floor(mapSize / 2), 0, -j + Mathf.Floor(mapSize / 2)), Quaternion.Euler(new Vector3()))).GetComponent<Tile>();
                    tile.transform.parent = mapTransform;
                    tile.gridPosition = new Vector2(i, j);
                    tile.setType((TileType)container.tiles.Where(x => x.locX == i && x.locY == j).First().id);
                    row.Add(tile);
                }
                map.Add(row);
            }
        }

        public void removeTileHighlights()
        {
            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    if (!map[i][j].impassible) map[i][j].visual.transform.renderer.materials[0].color = Color.white;
                }
            }
        }

        public void moveCurrentPlayer(Tile destTile)
        {
            var destTileRenderer = destTile.visual.transform.GetComponent<Renderer>();
            if (destTileRenderer.materials[0].color != Color.white && !destTile.impassible && players[currentPlayerIndex].positionQueue.Count == 0)
            {
                removeTileHighlights();
                players[currentPlayerIndex].moving = false;
                foreach (Tile t in TilePathFinder.FindPath(map[(int)players[currentPlayerIndex].gridPosition.x][(int)players[currentPlayerIndex].gridPosition.y], destTile, players.Where(x => x.gridPosition != destTile.gridPosition && x.gridPosition != players[currentPlayerIndex].gridPosition).Select(x => x.gridPosition).ToArray()))
                {
                    players[currentPlayerIndex].positionQueue.Add(map[(int)t.gridPosition.x][(int)t.gridPosition.y].transform.position + 1.5f * Vector3.up);
                    Debug.Log("(" + players[currentPlayerIndex].positionQueue[players[currentPlayerIndex].positionQueue.Count - 1].x + "," + players[currentPlayerIndex].positionQueue[players[currentPlayerIndex].positionQueue.Count - 1].y + ")");
                }
                players[currentPlayerIndex].gridPosition = destTile.gridPosition;
            }
            else
            {
                Debug.Log("destination invalid");
            }
        }
    }
}