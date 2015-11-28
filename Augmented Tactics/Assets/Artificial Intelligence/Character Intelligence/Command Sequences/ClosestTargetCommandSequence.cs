using System;
using System.Collections.Generic;
using System.Linq;
public class ClosestTargetCommandSequence : AttackCommandSequence {

    private List<Action> _closestEnemyToAttackSequence;
    private int _closestEnemyToAttackRating;

    public override List<Action> MakeCommandSequence()
    {
        throw new NotImplementedException();
    }

    public override void MoveTowardsCharacter(CharacterObservable character)
    {
        throw new NotImplementedException();
    }

    public override void EvaluateCommandSequence()
    {
        throw new NotImplementedException();
    }
}
