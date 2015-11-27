using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

public static class GameMapExtensions : MonoBehaviour {

	public static bool CanMoveAway(this GameMap gm, CharacterObservable chObs)
	{
		return GetAvailableMoveActions(gm, chObs).Count(c => c.UseByCharacter) < 4;
	}
	
	public static IEnumerable<Cell> GetAvailableMoveActions(this GameMap gm, CharacterObservable self)
	{
		var coor = self.CurrentCoordinates.Coordinates;
		return
			gm.CellGameMap.SelectMany(
				row => row.Where(cell => Math.Abs(cell.Coordinates.x - (coor.x + 0x1)) < double.Epsilon ||
			                 Math.Abs(cell.Coordinates.x - (coor.x - 0x1)) < double.Epsilon ||
			                 Math.Abs(cell.Coordinates.y - (coor.y + 0x1)) < double.Epsilon ||
			                 Math.Abs(cell.Coordinates.y - (coor.y - 0x1)) < double.Epsilon));
	}
	
	public static IEnumerable<MapCell> GetAvailableMoveActions(this GameMap gm, BaseCharacter self, bool testing)
	{
		var coor = self.CurrentCoordinates.Coordinates;
		return gm.MapCell.SelectMany(
			row => row.Where(cell => Math.Abs(cell.x - (coor.x + 0x1)) < double.Epsilon ||
		                 Math.Abs(cell.x - (coor.x - 0x1)) < double.Epsilon ||
		                 Math.Abs(cell.y - (coor.y + 0x1)) < double.Epsilon ||
		                 Math.Abs(cell.y - (coor.y - 0x1)) < double.Epsilon));
	}
	
	public static int GetNumberCharactersBlockingMovement(this GameMap gm, BaseCharacter character)
	{
		return GetAvailableMoveActions(gm, character).Count(c => c.UseByCharacter);
	}
	
	public static List<Cell> PathToTarget(this GameMap gm, Cell origin, Cell goal)
	{
		var pathFinder = new AStarPathfinder();
		pathFinder.FindPath(origin, goal, gm.CellGameMap, false);
		return pathFinder.CellsFromPath();
	}
	
	public static Cell FindPlayerCoordinates(this GameMap gm, BaseCharacter player)
	{
		var cells = gm.CellGameMap.Cast<Cell>().ToList();
		return cells.FirstOrDefault(c => c.Coordinates == player.CurrentCoordinates.Coordinates);
	}
	
	public static bool InRange(this GameMap gm, BaseCharacter self, BaseCharacter target)
	{
		var mp = self.MovementPoints;
		var currentPos = self.CurrentPosition;//Pos in x, y
		var row = gm.CellGameMap.ElementAt((int)currentPos.gridPosition.x);
		var start = gm.CellGameMap.IndexOf(row) - mp;
		if (start <= 0)
			start = 0;
		for (; start < gm.CellGameMap.IndexOf(row) + mp; start++)
		{
			var counter = start;
			for (; counter < 2 * mp + start; counter++)
			{
				if (row.ElementAt(counter).Coordinates == target.CurrentCoordinates.Coordinates)
					return true;
			}
		}
		return false;
	}
	
	public static IEnumerable<BaseCharacter> PlayersInRange(this GameMap gm, BaseCharacter self,
	                                                        params BaseCharacter[] players)
	{
		return players.Where(player => InRange(gm, self, player));
	}
	
	#endregion Current Extensions Usage
	
	public static bool IsInRange(this GameMap gm, CharacterObservable self, CharacterObservable character)
	{
		var mp = self.MovementPoints;
		var currentPos = self.CurrentCoordinates;//Pos in x, y
		var row = gm.TileMap.ElementAt((int)currentPos.Coordinates.x);
		var start = gm.TileMap.IndexOf(row) - mp;
		if (start <= 0)
			start = 0;
		for (; start < gm.TileMap.IndexOf(row) + mp; start++)
		{
			var counter = start;
			for (; counter < 2 * mp + start; counter++)
			{
				if (row.ElementAt(counter).gridPosition == character.CurrentCoordinates.Coordinates)
					return true;
			}
		}
		return false;
	}
	
	public static IEnumerable<bool> AreInRange(this GameMap gm, BaseCharacter self, params BaseCharacter[] characters)
	{
		var inRange = characters.Select(ch => IsInRange(gm, self, ch)).ToList();
		return inRange;
	}
	
	public static void RemoveTileHighlights(this GameMap gm, int mapSize)
	{
		for (var i = 0; i < mapSize; i++)
		{
			for (var j = 0; j < mapSize; j++)
			{
				if (!gm.TileMap[i][j].impassible) gm.TileMap[i][j].visual.transform.GetComponent<Renderer>().materials[0].color = Color.white;
			}
		}
	}
	
	public static Tile RetrieveCharacterOnTile(this GameMap gm, BaseCharacter charac)
	{
		var tilesInUsed = gm.TileMap
			.SelectMany(tiles => tiles
			            .Where(tile => tile.BeingUsed))
				.ToList();
		return tilesInUsed
			.FirstOrDefault(tiu => charac.CurrentPosition.gridPosition == tiu.gridPosition);
	}
	
	public static void ClearCharacterFromMap(this GameMap gm, CharacterObservable obs)
	{
		var firstOrDefault = gm.CellGameMap.SelectMany(x => x).FirstOrDefault(x => x.Equals(obs.CurrentCoordinates));
		if (firstOrDefault != null)
			firstOrDefault.UseByCharacter =
				false;
	}
}
