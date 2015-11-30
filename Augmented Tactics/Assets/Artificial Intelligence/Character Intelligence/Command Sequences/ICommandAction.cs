using System;
using System.Collections.Generic;

public interface ICommandAction
{
    bool CanSelfMove();

    void EvaluateCommandSequence();

    object FindCharacter(); //The return type should the prefab element composing the map. Will be updated on a lete

    bool IsFacingCharacter(CharacterObservable self, CharacterObservable target);

    List<Action> MakeCommandSequence();

    void MoveTowardsCharacter(CharacterObservable character);

    void RotateTowardsCharacter(CharacterObservable character);
}