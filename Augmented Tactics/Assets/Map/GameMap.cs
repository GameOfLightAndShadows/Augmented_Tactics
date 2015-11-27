﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
public class GameMap : MonoBehaviour {
	public int MapSize = 20;
	private static GameMap gm;
	private static readonly object padlock = new object();
	public List<List<Cell>> CellGameMap;
	public GameObject CellPrefab;
	public static int Instances = 0;



	private void CreateMap()
	{
		CellGameMap = new List<List<Cell>>();
		for (var i = 0; i < 32; i++)
			for (var j = 0; j < 32; j++)
		{
			var cell = Instantiate(
				CellPrefab,
				new Vector3(j, i, 0),
				Quaternion.identity) as GameObject;
			if (cell == null)
				continue;
			cell.transform.parent = transform;
			var c = cell.GetComponent<Cell>();
			c.Coordinates = new Vector2(i, j);
			CellGameMap[i].Add(c);
		}
	}

	// Use this for initialization
	void Start () {
		CreateMap ();
		if(Instances <2)
			Instances++;
		else 
			throw new Exception("Cannot have more than a map");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
