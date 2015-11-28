using System;
using System.Collections.Generic;
using System.Linq;

public class ClosestTargetCommandSequence : AttackCommandSequence
{
    private List<Action> _closestEnemyToAttackSequence;
    private int _closestEnemyToAttackRating;
    private EnemyBase _selfAsEnemy;
    private bool _isMoveNeeded;
    private bool _isFacingCharacter;
    private bool _isTargetInAttackRange;

    public ClosestTargetCommandSequence(params CharacterObservable[] humans)
    {
        _selfAsEnemy = (EnemyBase)Observable;
        SelectTarget(humans);
    }

    public override List<Action> MakeCommandSequence()
    {
        _closestEnemyToAttackSequence = new List<Action>();
        if (!_isMoveNeeded)
        {
            _closestEnemyToAttackSequence.Add(() => MoveTowardsCharacter(_selfAsEnemy.Target));
        }

        if (_isTargetInAttackRange)
        {
            _closestEnemyToAttackSequence.Add(() => AttackPlayer(_selfAsEnemy.Target));
        }

        return _closestEnemyToAttackSequence;
    }


    public void AttackPlayer(CharacterObservable bs)
    {
        if (_selfAsEnemy.IsOfTypeWizard() && IsInsideAttackRange(bs))
        {
            HarmCharacter(bs);
        }

        if (_selfAsEnemy.IsOfTypeWizard() && !IsInsideAttackRange(bs))
        {
            MoveTowardsCharacter(bs);
            if (IsInsideAttackRange(bs))
                HarmCharacter(bs);
        }
        if (CanAttackFromCloseRange(bs))
        {
            HarmCharacter(bs);
        }

        if (!IsMoveNeeded(bs) && !IsFacingPlayer(bs) && !(_self is NormalMage))
        {
            LookAtHuman(bs);
            HarmCharacter(bs);
        }

        if (!IsMoveNeeded(bs)) return;
        MoveTowardsCharacter(bs);
        if (CanAttackFromCloseRange(bs))
            HarmCharacter(bs);
    }

    public override void MoveTowardsCharacter(CharacterObservable character)
    {
        throw new NotImplementedException();
    }

    private bool IsMoveNeeded(CharacterObservable human)
    {
        var directionMoves = _selfAsEnemy.Map.GetAvailableMoveActions(_selfAsEnemy);
        return directionMoves.Any(t => t.Coordinates == human.CurrentCoordinates.Coordinates);
    }

    public override void EvaluateCommandSequence()
    {
        _isMoveNeeded = IsMoveNeeded(_selfAsEnemy.Target);
        if (!_isMoveNeeded)
            _closestEnemyToAttackRating += 50;

        _isFacingCharacter = IsFacingPlayer(_selfAsEnemy.Target);
        if (_isFacingCharacter)
            _closestEnemyToAttackRating += 20;

        if (!_selfAsEnemy.Target.Stats.DefenseBonusActivated)
            _closestEnemyToAttackRating += 25;
    }

    public sealed override void SelectTarget(CharacterObservable[] humans)
    {
        var observableAsEnemy = (EnemyBase)Observable;
        var pathfinder = observableAsEnemy.PathFinder;
        var closestCharacter = humans[0];
        pathfinder.FindPath(Observable.CurrentCoordinates, humans[0].CurrentCoordinates, observableAsEnemy.Map.CellGameMap, false);
        var minCost = pathfinder.FinalPath.Count;
        var targets = humans.ToList().Skip(1);
        foreach (var target in targets)
        {
            pathfinder.FindPath(observableAsEnemy.CurrentCoordinates, target.CurrentCoordinates, observableAsEnemy.Map.CellGameMap, false);
            if (pathfinder.FinalPath.Count < minCost)
            {
                closestCharacter = target;
            }
        }
        _selfAsEnemy.Target = (CharacterBase)closestCharacter;
    }
}