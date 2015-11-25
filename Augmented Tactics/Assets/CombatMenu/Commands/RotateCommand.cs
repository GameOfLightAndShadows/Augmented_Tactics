using UnityEngine;
using System.Collections;

public class RotateCommand : CharacterAction {

	public RotateCommand(IReceiver receiver) : base(receiver)
	{
	}
	
	public void Execute(CharacterObservable character, PlayerDirection newDirection)
	{
		Receiver.SetUserAction(GameActions.RotateAction);
		if (character.Direction == newDirection) return;
		character.Direction = newDirection;
		MakeCharacterRotate(character);
	}
	
	
	private void MakeCharacterRotate(CharacterObservable obs)
	{
		var dir = obs.Direction;
		switch (dir)
		{
		case PlayerDirection.Down:
			break;
		case PlayerDirection.Up:
			break;
		case PlayerDirection.Left:
			break;
		case PlayerDirection.Right:
			break;
		}
	}

}
