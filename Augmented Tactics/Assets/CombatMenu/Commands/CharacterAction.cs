using UnityEngine;
using System.Collections;

public abstract class CharacterAction : ICharacterActionCommand {
	public IReceiver Receiver { get; set; }
	public bool IsExecuted { get; set; }
	
	protected CharacterAction(IReceiver receiver)
	{
		Receiver = receiver;
		IsExecuted = false;
	}
	
	public virtual void Execute(CharacterObservable caller, CharacterObservable chracterToInteractWith)
	{
	}
	
	public virtual void Execute(CharacterObservable caller)
	{
	}
	
	public virtual void Execute(CharacterObservable caller, int raiseDefense)
	{
	}
}
