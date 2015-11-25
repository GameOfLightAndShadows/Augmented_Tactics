using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public class EnemyBase : CharacterObservable {
	public List<CharacterObservable> HumanPlayers;
	public ChoiceSelector ChoiceSelector;


	//TODO:Removed abstract Notify in the CharacterObservable script and create an implementation directly in the file. (DRY)
	public override void Notify()
	{
		if (isDamage || healthWasRaised)
		{
			//TODO: Type should be HealthManager
			foreach (var o in ObserversList.OfType<HealthManager>())
			{
				o.UpdateObserver(this);
			}
		}
		
		if (Health.IsDead)
		{
			//TODO: Type should be GameManager
			foreach (var o in ObserversList.OfType<GameManager>())
			{
				o.UpdateObserver(this);
			}
		}
	}

	//TODO: After choice has been made, the choices should be reset 
	public void ResetChoiceSelector()
	{
		throw new NotImplementedException();
	}

	//TODO: Will perform the best sequence for current turn hold inside the ChoiceSelector.
	public void PerformChoice()
	{
		throw new NotImplementedException ();
	}
}
