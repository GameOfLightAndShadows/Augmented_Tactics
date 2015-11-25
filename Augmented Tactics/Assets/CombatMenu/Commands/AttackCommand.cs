using UnityEngine;
using System.Collections;
using System;
public class AttackCommand : CharacterAction {
	public CharacterObservable Target;
	
	public AttackCommand(IReceiver receiver) : base(receiver)
	{
	}

	
	public new void Execute(ICharacter caller, ICharacter characterToAttack)
	{
		if (caller == null)
			throw new ArgumentException();
		if (characterToAttack == null)
			throw new ArgumentException();
		//On Later commit

		//if (!(caller is BaseEnemy) && !(characterToAttack is BaseEnemy))
		//	throw new Exception("Cannot attack team member");
		//if (caller is Healer && characterToAttack is BaseEnemy)
		//	throw new Exception("Cannot heal enemies");
		//if (caller is BaseEnemy && characterToAttack is BaseEnemy
		//    && !(caller is NormalHealer || caller is HardHealer))
		//	throw new Exception("No friendly-fire amongst AI");
		if (caller.Equals(characterToAttack))//To be tested November 20th 2015
			throw new Exception("Cannot harm self");
		
		Target = characterToAttack as CharacterObservable;
		if (Target == null)
			return;
		//if (caller is NormalHealer || caller is HardHealer && Target is BaseEnemy)
		//{
		//	Target.Health.RestoreHealth((int)caller.BaseStats.Power);
		//}
		else
		{
			var attackStrenght = caller.Stats.GetAttackStrenght(caller, Target);
			Target.Health.TakeDamageFromCharacter(attackStrenght);
		}
		Target.Notify(); // Single cal of notify here will either restore health, reduce health or make so that the manager call Death animation and destroy game object
		IsExecuted = true;
	}
}
