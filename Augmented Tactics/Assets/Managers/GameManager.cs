using Assets.Managers;
using Assets.Map;
using Assets.Map.Creator;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour, ICharacterObserver
{
    private int _indexOfCharacters;
    public List<CharacterObservable> GameCharacters;
    public CharacterObservable ActivePlayer;
    public GameMap GameMap;
    public GameObject CellPrefab;
    public List<List<Cell>> map;
    public int mapSize = 32;
    private Transform mapTransform;
    private GameObjectSpawmer spawmer;
    public CommandManager _commandManager;

    public static GameManager instance;

    public void GoToNextCharacter()
    {
        _indexOfCharacters = _indexOfCharacters + 1 < GameCharacters.Count ? _indexOfCharacters + 1 : 0;
        ActivePlayer = GameCharacters[_indexOfCharacters];
    }

    private void GenerateGameCharacter()
    {
        GameCharacters = spawmer.GenerateGameCharacters();
    }

    private bool DidPlayerLose()
    {
        return spawmer.Humans.Count == 0;
    }

    private bool DidPlayerWin()
    {
        return spawmer.Enemies.Count == 0;
    }

    public void UpdateObserver(CharacterObservable character)
    {
        throw new NotImplementedException();
    }

    private void loadMapFromXml()
    {
        CellMapXMLContainer container = MapSaveLoad.Load("map.xml");

        mapSize = container.size;

        //initially remove all children
        for (int i = 0; i < mapTransform.childCount; i++)
        {
            Destroy(mapTransform.GetChild(i).gameObject);
        }

        map = new List<List<Cell>>();
        for (int i = 0; i < mapSize; i++)
        {
            List<Cell> row = new List<Cell>();
            for (int j = 0; j < mapSize; j++)
            {
                Cell tile = ((GameObject)Instantiate(PrefabHolder.instance.BASE_TILE_PREFAB, new Vector3(i - Mathf.Floor(mapSize / 2), 0, -j + Mathf.Floor(mapSize / 2)), Quaternion.Euler(new Vector3()))).GetComponent<Cell>();
                tile.transform.parent = mapTransform;
                tile.gridPosition = new Vector2(i, j);
                tile.setType((CellType)container.cells.Where(x => x.locX == i && x.locY == j).First().id);
                row.Add(tile);
            }
            map.Add(row);
        }
    }

    private void CreateMap()
    {
        CellMapXMLContainer container = MapSaveLoad.Load("map.xml");

        mapSize = container.size;

        //initially remove all children
        // for (int i = 0; i < mapTransform.childCount; i++)
        //{
        //    Destroy(mapTransform.GetChild(i).gameObject);
        //}

        map = new List<List<Cell>>();
        for (int i = 0; i < mapSize; i++)
        {
            List<Cell> row = new List<Cell>();
            for (int j = 0; j < mapSize; j++)
            {
                GameObject cellPrefab;
                cellPrefab = ((GameObject)Instantiate(PrefabHolder.instance.BASE_TILE_PREFAB.gameObject, new Vector3(i - Mathf.Floor(mapSize / 2), 0, -j + Mathf.Floor(mapSize / 2)), Quaternion.Euler(new Vector3())));
                Cell cell = cellPrefab.GetComponent<Cell>();
                cell.transform.parent = mapTransform;
                cell.gridPosition = new Vector2(i, j);
                cell.type = (CellType)(container.cells.Where(x => x.locX == i && x.locY == j).First().id);
                row.Add(cell);
            }
            map.Add(row);
        }
    }

    private void InitializaGameMap()
    {
        map = new List<List<Cell>>();
        for (int i = 0; i < 32; i++)
        {
            List<Cell> row = new List<Cell>();
            for (int j = 0; j < 32; j++)
            {
                Cell tile = ((GameObject)Instantiate(PrefabHolder.instance.BASE_TILE_PREFAB, new Vector3(i - Mathf.Floor(mapSize / 2), 0, -j + Mathf.Floor(mapSize / 2)), Quaternion.Euler(new Vector3()))).GetComponent<Cell>();
                tile.gridPosition = new Vector2(i, j);
                row.Add(tile);
            }
            map.Add(row);
        }
    }

    public void Awake()
    {
        instance = this;

        mapTransform = transform.FindChild("Map");
    }

    // Use this for initialization
    public void Start()
    {
        loadMapFromXml();
        _indexOfCharacters = 0;
        ActivePlayer = GameCharacters[_indexOfCharacters];
    }

    // Update is called once per frame
    private void Update()
    {
        if (DidPlayerLose())
        {
            //Show canvas for lost game.
        }
        if (DidPlayerWin())
        {
            //Show canvas for won game.
        }

        if (HasCharacterEndTurn())
        {
            GoToNextCharacter();
        }
    }

    private bool HasCharacterEndTurn()
    {
        return _commandManager.PreviousAction is EndTurnCommand || _commandManager.PreviousAction is SkipCommand; 
    }
}