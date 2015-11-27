using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System; 
using System.Linq;
public abstract class DefensiveCommandSequence : CommandSequence {
	public CharacterObservable Observable { get; set; }
	protected Dictionary<List<Action>, int> CommandSequenceDictionary;
	protected int _moveTowardsHealerRating;
	protected int _moveAwayFromDangerousSituationRating;
	protected System.Random valuesGenerator;
	private int _count = 8;

	public void AddObservable(CharacterObservable obs)
	{
		if (obs != null)
			Observable = obs;
		else
			throw new ArgumentNullException();
	}

	protected void InvokeDefendBonus()
	{
		var nextValue = valuesGenerator.Next(0, _count);
		_count--;
		if (nextValue >= 5) {
			Observable.Stats.DetermineDefenseBonusForTurn ();
			Observable.Animator.SetTrigger("Defense");
		}
	}
}
