using UnityEngine;
using System.Collections;

public abstract class CharacterAction : ICharacterActionCommand {
	public IReceiver Receiver { get; set; }
	public bool IsExecuted { get; set; }
	
	protected CharacterActions(IReceiver receiver)
	{
		Receiver = receiver;
		IsExecuted = false;
	}
	
	public virtual void Execute(ICharacter caller, ICharacter chracterToInteractWith)
	{
	}
	
	public virtual void Execute(ICharacter caller)
	{
	}
	
	public virtual void Execute(ICharacter caller, int raiseDefense)
	{
	}
}
