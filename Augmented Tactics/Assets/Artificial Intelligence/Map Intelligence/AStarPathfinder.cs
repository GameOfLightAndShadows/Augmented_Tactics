using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
public class AStarPathfinder
{
	private List<Node> _openList;
	private List<Node> _closeList;
	private List<Node> _neighbours;
	public List<Node> FinalPath;
	private Node _start;
	private Node end;
	
	private Cell[][] _map;
	private int _mapWidth;
	private int _mapHeight;
	private bool _isTesting;
	
	public AStarPathfinder()
	{
		_openList = new List<Node>();
		_closeList = new List<Node>();
		_neighbours = new List<Node>();
		FinalPath = new List<Node>();
	}
	
	public void FindPath(Cell startCell, Cell goalCell, List<List<Cell>> map, bool targetCellMustBeFree)
	{
		_map = map.Select(a => a.ToArray()).ToArray(); ;
		_mapWidth = _map.GetLength(0);
		_mapHeight = _map.GetLength(1);
		
		_start = new Node((int)startCell.Coordinates.x, (int)startCell.Coordinates.y, 0, 0, 0, null, startCell);
		end = new Node((int)goalCell.Coordinates.x, (int)goalCell.Coordinates.y, 0, 0, 0, null, goalCell);
		_openList.Add(_start);
		bool keepSearching = true;
		bool pathExists = true;
		
		while ((keepSearching) && (pathExists))
		{
			Node currentNode = ExtractBestNodeFromOpenList();
			if (currentNode == null)
			{
				pathExists = false;
				break;
			}
			_closeList.Add(currentNode);
			if (NodeIsGoal(currentNode))
				keepSearching = false;
			else
			{
				if (targetCellMustBeFree)
					FindValidFourNeighbours(currentNode);
				else
					FindValidFourNeighboursIgnoreTargetCell(currentNode);
				
				foreach (Node neighbour in _neighbours)
				{
					if (FindInCloseList(neighbour) != null)
						continue;
					Node inOpenList = FindInOpenList(neighbour);
					if (inOpenList == null)
					{
						_openList.Add(neighbour);
					}
					else
					{
						if (neighbour.G < inOpenList.G)
						{
							inOpenList.G = neighbour.G;
							inOpenList.MoveCost = inOpenList.G + inOpenList.H;
							inOpenList.Parent = currentNode;
						}
					}
				}
			}
		}
		
		if (pathExists)
		{
			Node n = FindInCloseList(end);
			while (n != null)
			{
				FinalPath.Add(n);
				n = n.Parent;
			}
		}
	}
	
	public List<int> PointsFromPath()
	{
		List<int> points = new List<int>();
		foreach (Node n in FinalPath)
		{
			points.Add(n.X);
			points.Add(n.Y);
		}
		return points;
	}
	
	public List<Cell> CellsFromPath()
	{
		List<Cell> path = FinalPath.Select(n => n.Cell).ToList();
		if (path.Count == 0) return path;
		path.Reverse();
		path.RemoveAt(0);
		return path;
	}
	
	public List<Cell> CellsFromPath(int size)
	{
		return FinalPath.Select(node => node.Cell).Take(size).ToList();
	}
	
	private Node ExtractBestNodeFromOpenList()
	{
		float minF = float.MaxValue;
		Node bestOne = null;
		foreach (Node n in _openList)
		{
			if (n.MoveCost < minF)
			{
				minF = n.MoveCost;
				bestOne = n;
			}
		}
		if (bestOne != null)
			_openList.Remove(bestOne);
		return bestOne;
	}
	
	private bool NodeIsGoal(Node node)
	{
		return ((node.X == end.X) && (node.Y == end.Y));
	}
	
	private void FindValidFourNeighbours(Node n)
	{
		_neighbours.Clear();
		
		if ((n.Y - 1 >= 0) && ((_map[n.X][n.Y - 1].IsWalkable())))
		{
			Node vn = PrepareNewNodeFrom(n, 0, -1);
			_neighbours.Add(vn);
		}
		if ((n.Y + 1 <= _mapHeight - 1) && ((_map[n.X][n.Y + 1].IsWalkable())))
		{
			Node vn = PrepareNewNodeFrom(n, 0, +1);
			_neighbours.Add(vn);
		}
		if ((n.X - 1 >= 0) && ((_map[n.X - 1][n.Y].IsWalkable())))
		{
			Node vn = PrepareNewNodeFrom(n, -1, 0);
			_neighbours.Add(vn);
		}
		if ((n.X + 1 <= _mapWidth - 1) && ((_map[n.X + 1][n.Y].IsWalkable())))
		{
			Node vn = PrepareNewNodeFrom(n, 1, 0);
			_neighbours.Add(vn);
		}
	}
	
	/* Last cell does not need to be walkable in the farm game */
	
	private void FindValidFourNeighboursIgnoreTargetCell(Node n)
	{
		_neighbours.Clear();
		if ((n.Y - 1 >= 0) && ((_map[n.X][n.Y - 1].IsWalkable()) || _map[n.X][n.Y - 1] == end.Cell))
		{
			Node vn = PrepareNewNodeFrom(n, 0, -1);
			_neighbours.Add(vn);
		}
		if ((n.Y + 1 <= _mapHeight - 1) && ((_map[n.X][n.Y + 1].IsWalkable()) || _map[n.X][n.Y + 1] == end.Cell))
		{
			Node vn = PrepareNewNodeFrom(n, 0, +1);
			_neighbours.Add(vn);
		}
		if ((n.X - 1 >= 0) && ((_map[n.X - 1][n.Y].IsWalkable()) || _map[n.X - 1][n.Y] == end.Cell))
		{
			Node vn = PrepareNewNodeFrom(n, -1, 0);
			_neighbours.Add(vn);
		}
		if ((n.X + 1 <= _mapWidth - 1) && ((_map[n.X + 1][n.Y].IsWalkable()) || _map[n.X + 1][n.Y] == end.Cell))
		{
			Node vn = PrepareNewNodeFrom(n, 1, 0);
			_neighbours.Add(vn);
		}
	}
	
	
	private Node PrepareNewNodeFrom(Node n, int x, int y)
	{
		Node newNode = new Node(n.X + x, n.Y + y, 0, 0, 0, n, _map[n.X + x][n.Y + y]);
		newNode.G = n.G + MovementCost(n, newNode);
		newNode.H = Heuristic(newNode);
		newNode.MoveCost = newNode.G + newNode.H;
		newNode.Parent = n;
		return newNode;
	}
	
	private float Heuristic(Node n)
	{
		return Mathf.Sqrt((n.X - end.X) * (n.X - end.X) + (n.Y - end.Y) * (n.Y - end.Y));
	}
	
	public float MovementCost(Node a, Node b)
	{
		return _map[b.X][b.Y].MovementCost();
	}
	
	private Node FindInCloseList(Node n)
	{
		return _closeList.FirstOrDefault(nestedNode => (nestedNode.X == n.X) && (nestedNode.Y == n.Y));
	}
	
	private Node FindInOpenList(Node n)
	{
		return _openList.FirstOrDefault(nestedNode => (nestedNode.X == n.X) && (nestedNode.Y == n.Y));
	}
}
