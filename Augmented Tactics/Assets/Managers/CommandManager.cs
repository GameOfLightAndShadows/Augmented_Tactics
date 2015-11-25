using UnityEngine;
using System.Collections;
using System;
public class CommandManager : MonoBehaviour {
	public Stack CommandStack { get; set; }
	public ICharacter CurrentCharater { get; set; }
	internal ICharacterActionCommand PreviousAction;
	private CharacterObservable LastCharacterAttacked;
	
	public void UpdateCommandStack(ICharacterActionCommand cmd)
	{
		if (CurrentCharater == null)
			throw new NullReferenceException();
		if(!cmd.IsExecuted)
			throw new Exception("Cannot update stack if command was not executed!");
		CommandStack.Push(cmd);
		if (cmd is AttackCommand)
		{
			LastCharacterAttacked = ((AttackCommand) cmd).Target;
		}
		PreviousAction = cmd;
	}
	
	public void CreateStack()
	{
		CommandStack = new Stack();
	}
	
	public void SetCurrent(ICharacter current)
	{
		CurrentCharater = current;
	}
	
	
	//TODO: Update method's signature
	public void Undo(CharacterObservable caller, CharacterObservable active)
	{
		if(!Equals(caller, active))
			throw new Exception("Cannot Undo on non-active character");
		
		PreviousAction = (ICharacterActionCommand) CommandStack.Pop();
		if (PreviousAction is AttackCommand && LastCharacterAttacked != null)
		{
			var attackStrenght = caller.Stats.GetAttackStrenght(caller, LastCharacterAttacked);
			LastCharacterAttacked.Health.RaiseHealth(caller);
			LastCharacterAttacked.healthWasRaised = true;
			LastCharacterAttacked.Notify();
			LastCharacterAttacked.healthWasRaised = false;
			((AttackCommand)PreviousAction).IsExecuted = false;
			return;
		}
		if (PreviousAction is DefendCommand)
		{
			caller.Stats.DefenseBonusActivated = false;
			caller.Stats.ResetTemporaryBonus();
			((DefendCommand)PreviousAction).IsExecuted = false;
			return;
		}
		//Implementation to come
		if (PreviousAction is MoveCommand)
		{
			//caller.CurrentCoordinates = caller.OldCoordinates;
			//((MoveCommand)PreviousAction).Execute(caller.CurrentCoordinates);
			//((MoveCommand)PreviousAction).IsExecuted = false;
			
		}
		
		if (PreviousAction is RotateCommand)
		{
			
		}
	}
	
	public void Undo(CharacterObservable caller, CharacterObservable personaeToInteracWith, GameManager gm)
	{
		
		PreviousAction = (ICharacterActionCommand)CommandStack.Pop();
		
		if (PreviousAction is AttackCommand && LastCharacterAttacked!= null)
		{
			var attackStrenght = caller.Stats.GetAttackStrenght(caller, LastCharacterAttacked);
			LastCharacterAttacked.Health.RaiseHealth(caller);
			((AttackCommand)PreviousAction).IsExecuted = false;
			return;
		}
		if (PreviousAction is DefendCommand)
		{
			caller.Stats.DefenseBonusActivated = false;
			caller.Stats.ResetTemporaryBonus();
			((DefendCommand)PreviousAction).IsExecuted = false;
			return;
		}
		if (PreviousAction is MoveCommand)
		{
			//caller.CurrentCoordinates = caller.OldCoordinates;
			//((MoveCommand)PreviousAction).Execute(caller.CurrentCoordinates);
			//((MoveCommand)PreviousAction).IsExecuted = false;
			
		}
		PreviousAction.Execute(caller);
	}
}
