using UnityEngine;
using System.Collections;

public interface ICommandAction {
	bool CanSelfMove();
	void EvaluateCommandSequence();
	object FindCharacter(); //The return type should the prefab element composing the map. Will be updated on a lete
	bool IsFacingCharacter(ICharacter character);
	ICommandAction MakeCommandSequence();
	void MoveTowardsCharacter(ICharacter character);
	void RotateTowardsCharacter(ICharacter character);
}
