using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
public abstract class CommandSequence : ICommandAction {
	#region Implemented Methods
	public bool CanSelfMove()
	{
		//Will be based on coordinate of self in the game map.
		//Implementation will have to wait. 
		throw new NotImplementedException ();
	}
	
	public object FindCharacter()
	{
		//Will be based on coordinate of self in the game map.
		//Implementation will have to wait.
		throw new NotImplementedException ();
	}

	public bool IsFacingCharacter(ICharacter self, ICharacter target)
	{
		return Math.Abs (self.Direction - target.Direction) == 1;
	}

	public void RotateTowardsCharacter(ICharacter character)
	{
		throw new NotImplementedException ();
	}
	#endregion Implemented Methods

	#region Abstract Methods
	public abstract List<Action> MakeCommandSequence();
	public abstract void MoveTowardsCharacter(ICharacter character);
	public abstract void EvaluateCommandSequence();
	#endregion Abstract Methods

}
