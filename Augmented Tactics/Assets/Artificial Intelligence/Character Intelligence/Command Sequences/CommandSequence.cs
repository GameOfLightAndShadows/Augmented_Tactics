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

	public bool IsFacingCharacter(CharacterObservable self, CharacterObservable target)
	{
		return Math.Abs (self.Direction - target.Direction) == 1;
	}

	public void RotateTowardsCharacter(CharacterObservable character)
	{
		throw new NotImplementedException ();
	}
	#endregion Implemented Methods

	#region Abstract Methods
	public abstract List<Action> MakeCommandSequence();
	public abstract void MoveTowardsCharacter(CharacterObservable character);
	public abstract void EvaluateCommandSequence();
	#endregion Abstract Methods

}
