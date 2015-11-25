using UnityEngine;
using System.Collections;
using System;
public class DefendCommand : CharacterAction {
	
	public DefendCommand(IReceiver receiver) : base(receiver)
	{
		
	}

	public new void Execute(CharacterObservable caller)
	{
		if(caller == null)
			throw new ArgumentException();
		if(caller.Stats == null)
			throw new Exception();
		if (caller.Stats.DefenseBonusActivated)
			return;
		Receiver.SetUserAction(GameActions.DefendAction);
		caller.Stats.Defense += caller.Stats.DetermineDefenseBonusForTurn();
		caller.Stats.DefenseBonusActivated = true;
		IsExecuted = true;
	}
}
