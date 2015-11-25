using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using Random = System.Random;
using System.Linq;

public class CharacterBase : CharacterObservable {
	public CharacterStats BaseStats { get; set; }
	public Health Health { get; set; }
	public List<ICharacter> CurrentEnnemies { get; set; }
	public List<ICharacter> TeamMembers { get; set; }
	public int MovementPoints { get; set; }
	public PlayerDirection Direction { get; set; }
//	public Cell CurrentCoordinates { get; set; }
//	public Cell OldCoordinates { get; set; }
	public int[] Position { get; set; } // [0]=x, [1]=y
	public Animator Animator { get; set; }


	public bool CanDoExtraDamage()
	{
		if (BaseStats.ChanceForCriticalStrike * BaseStats.Luck < 50) return false;
		BaseStats.CriticalStrikeCounter--;
		BaseStats.ChanceForCriticalStrike = new Random().Next(0, BaseStats.CriticalStrikeCounter);
		BaseStats.AjustCriticalStrikeChances();
		return true;
	}

	public override bool Equals(object obj)
	{
		var that = (CharacterBase)obj;
		return  ReferenceEquals(this,that) &&
			    GetType() == that.GetType() &&
				Health.CurrentHealth == that.Health.CurrentHealth &&
				Health.MaxHealth == that.Health.MaxHealth &&
				BaseStats == that.BaseStats;
	}

	public override void Notify()
	{
		if (isDamage || healthWasRaised)
		{
			//TODO: Type should be HealthManager
			foreach (var o in ObserversList.OfType<ICharacterObserver>())
			{
				o.UpdateObserver(this);
			}
		}
		
		if (Health.IsDead)
		{
			//TODO: Type should be GameManager
			foreach (var o in ObserversList.OfType<ICharacterObserver>())
			{
				o.UpdateObserver(this);
			}
		}
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
