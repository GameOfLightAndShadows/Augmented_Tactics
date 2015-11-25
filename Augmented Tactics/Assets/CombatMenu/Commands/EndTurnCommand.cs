using UnityEngine;
using System.Collections;
using System;
public class EndTurnCommand : CharacterAction {

	public EndTurnCommand(IReceiver receiver) : base(receiver)
	{
	}
	
	public void Execute(ICharacter caller, GameManager manager, CommandManager cm)
	{
		if (caller == null)
			throw new ArgumentException();            
		if(manager == null)
			throw new ArgumentException();
		if(cm == null)
			throw  new ArgumentException();
		cm.CreateStack();
		manager.GoToNextCharacter();
		cm.SetCurrent(manager.ActivePlayer);
	}
}
