using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


public class EnemyBase : CharacterObservable {
	public List<CharacterObservable> HumanPlayers;
	public ChoiceSelector ChoiceSelector;

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
