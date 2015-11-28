using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public interface ICommandAction {
	bool CanSelfMove();
	void EvaluateCommandSequence();
	object FindCharacter(); //The return type should the prefab element composing the map. Will be updated on a lete
	bool IsFacingCharacter(ICharacter self, ICharacter target);
	List<Action> MakeCommandSequence();
	void MoveTowardsCharacter(ICharacter character);
	void RotateTowardsCharacter(ICharacter character);
}
