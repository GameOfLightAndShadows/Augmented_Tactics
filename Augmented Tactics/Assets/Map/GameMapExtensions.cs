﻿using System;
using System.Collections.Generic;
using System.Linq;

public static class GameMapExtensions
{
    public static bool CanMoveAway(this GameMap gm, CharacterObservable chObs)
    {
        return GetAvailableMoveActions(gm, chObs).Count(c => c.UseByCharacter) < 4;
    }

    public static IEnumerable<Cell> GetAvailableMoveActions(this GameMap gm, CharacterObservable self)
    {
        var coor = self.CurrentCoordinates.gridPosition;
        return
            gm.CellGameMap.SelectMany(
                row => row.Where(cell => Math.Abs(cell.gridPosition.x - (coor.x + 0x1)) < double.Epsilon ||
                             Math.Abs(cell.gridPosition.x - (coor.x - 0x1)) < double.Epsilon ||
                             Math.Abs(cell.gridPosition.y - (coor.y + 0x1)) < double.Epsilon ||
                             Math.Abs(cell.gridPosition.y - (coor.y - 0x1)) < double.Epsilon));
    }

    public static int GetNumberCharactersBlockingMovement(this GameMap gm, CharacterObservable character)
    {
        return GetAvailableMoveActions(gm, character).Count(c => c.UseByCharacter);
    }

    public static List<Cell> PathToTarget(this GameMap gm, Cell origin, Cell goal)
    {
        var pathFinder = new AStarPathfinder();
        pathFinder.FindPath(origin, goal, gm.CellGameMap, false);
        return pathFinder.CellsFromPath();
    }

    public static Cell FindPlayerCoordinates(this GameMap gm, CharacterObservable player)
    {
        var cells = gm.CellGameMap.Cast<Cell>().ToList();
        return cells.FirstOrDefault(c => c.gridPosition == player.CurrentCoordinates.gridPosition);
    }

    public static bool InRange(this GameMap gm, CharacterObservable self, CharacterObservable target)
    {
        var mp = self.MovementPoints;
        var currentPos = self.CurrentCoordinates;
        var row = gm.CellGameMap.ElementAt((int)currentPos.gridPosition.x);
        var start = gm.CellGameMap.IndexOf(row) - mp;
        if (start <= 0)
            start = 0;
        for (; start < gm.CellGameMap.IndexOf(row) + mp; start++)
        {
            var counter = start;
            for (; counter < 2 * mp + start; counter++)
            {
                if (row.ElementAt(counter).gridPosition == target.CurrentCoordinates.gridPosition)
                    return true;
            }
        }
        return false;
    }

    public static IEnumerable<CharacterObservable> PlayersInRange(this GameMap gm, CharacterObservable self,
                                                            params CharacterObservable[] players)
    {
        return players.Where(player => InRange(gm, self, player));
    }

    public static bool IsInRange(this GameMap gm, CharacterObservable self, CharacterObservable target)
    {
        var mp = self.MovementPoints;
        var currentPos = self.CurrentCoordinates;//Pos in x, y
        var row = gm.CellGameMap.ElementAt((int)currentPos.gridPosition.x);
        var start = gm.CellGameMap.IndexOf(row) - mp;
        if (start <= 0)
            start = 0;
        for (; start < gm.CellGameMap.IndexOf(row) + mp; start++)
        {
            var counter = start;
            for (; counter < 2 * mp + start; counter++)
            {
                if (row.ElementAt(counter).gridPosition == target.CurrentCoordinates.gridPosition)
                    return true;
            }
        }
        return false;
    }

    public static IEnumerable<bool> AreInRange(this GameMap gm, CharacterObservable self, params CharacterObservable[] characters)
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
                //if (!gm.CellGameMap[i][j].impassible) gm.CellGameMap[i][j].visual.transform.GetComponent<Renderer>().materials[0].color = Color.white;
            }
        }
    }

    private static List<Cell> GetValidCardinalCells(this GameMap gm, Cell myPos)
    {
        var curX = (int)myPos.gridPosition.x;
        var curY = (int)myPos.gridPosition.y;
        var cardinalCells = new List<Cell>();
        if (curX - 1 > 0)
        {
            cardinalCells.Add(gm.CellGameMap[curX - 1].ElementAt(curY));
        }

        if (curX + 1 < gm.MapSize)
        {
            cardinalCells.Add(gm.CellGameMap[curX + 1].ElementAt(curY));
        }

        if (curY - 1 > 0)
        {
            cardinalCells.Add(gm.CellGameMap[curX].ElementAt(curY - 1));
        }

        if (curY + 1 > gm.MapSize)
        {
            cardinalCells.Add(gm.CellGameMap[curX].ElementAt(curY + 1));
        }
        return cardinalCells;
    }

    public static bool IsTargetPositionAdjacentToSelf(this GameMap gm, Cell currentPos, Cell targetCell)
    {
        return GetValidCardinalCells(gm, currentPos).Any((
            cc => cc.gridPosition.x == targetCell.gridPosition.x && cc.gridPosition.y == targetCell.gridPosition.y));
    }

    public static void ClearCharacterFromMap(this GameMap gm, CharacterObservable obs)
    {
        var firstOrDefault = gm.CellGameMap.SelectMany(x => x).FirstOrDefault(x => x.Equals(obs.CurrentCoordinates));
        if (firstOrDefault != null)
            firstOrDefault.UseByCharacter =
                false;
    }
}