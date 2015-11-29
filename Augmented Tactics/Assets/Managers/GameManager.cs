using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class GameManager :MonoBehaviour, ICharacterObserver {
	private int _indexOfCharacters;
	public List<CharacterObservable> GameCharacters;
	public CharacterObservable ActivePlayer;
	public GameMap GameMap;
	public GameObject CellPrefab;
	public List<List<Cell>> map;
	public int mapSize=32;

	public void GoToNextCharacter()
	{
		_indexOfCharacters = _indexOfCharacters + 1 < GameCharacters.Count ? _indexOfCharacters + 1 : 0;
		ActivePlayer = GameCharacters[_indexOfCharacters];
	}

	public void UpdateObserver(CharacterObservable character)
	{
		throw new NotImplementedException ();
	}


	private void InitializaGameMap()
	{
		map = new List<List<Cell>>();
		for (int i = 0; i < 32; i++) {
			List <Cell> row = new List<Cell>();
			for (int j = 0; j < 32; j++) {
				Cell tile = ((GameObject)Instantiate(PrefabHolder.instance.BASE_TILE_PREFAB, new Vector3(i - Mathf.Floor(mapSize/2),0, -j + Mathf.Floor(mapSize/2)), Quaternion.Euler(new Vector3()))).GetComponent<Cell>();
				tile.Coordinates = new Vector2(i, j);
				row.Add (tile);
			}
			map.Add(row);
		}
	}

	// Use this for initialization
	void Start () {
		InitializaGameMap ();
		//_indexOfCharacters = 0;
		//ActivePlayer = GameCharacters [_indexOfCharacters];
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
