using UnityEngine;
using System.Collections;

public class Node
{
	public int X;
	public int Y;
	public float G;
	public float H;
	public float MoveCost;
	public Node Parent;
	public Cell Cell;

	public Node(int x, int y, float g, float moveCost, float h, Node parent, Cell c)
	{
		X = x;
		Y = y;
		G = g;
		H = h;
		MoveCost = moveCost;
		Parent = parent;
		Cell = c;
	}

}
