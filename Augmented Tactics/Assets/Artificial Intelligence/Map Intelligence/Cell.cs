using UnityEngine;
using System.Collections;
using Assets.Map;

public class Cell : MonoBehaviour
{
    public Vector2 Coordinates = Vector2.zero;
    public CellType Type { get; set; }
	
	public bool UseByCharacter;
	
	public virtual bool IsWalkable()
	{
		return true;
	}
	
	public virtual float MovementCost()
	{
		return 0;
	}
}
