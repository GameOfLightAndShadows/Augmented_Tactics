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
        EvaluateCommandSequence();
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
        if (!CanSelfMove()) return;

        if (_selfAsEnemy.IsOfTypeWizard())
        {
            _closestEnemyToAttackSequence.Add(() => WizardAttackPattern(bs));
        }
        else
        {
            if (CanAttackWithoutMoving(bs))
                _closestEnemyToAttackSequence.Add(HarmTarget);
            else
            {
                _closestEnemyToAttackSequence.Add(() => MoveTowardsCharacter(bs));
                if (!_isFacingCharacter)
                    _closestEnemyToAttackSequence.Add(() => LookAtHuman(bs));

                if (CanPerformCloseRangeAttack())
                    _closestEnemyToAttackSequence.Add(HarmTarget);
            }
        }

        if (!IsMoveNeeded(bs) && !IsFacingPlayer(bs) && !_selfAsEnemy.IsOfTypeWizard())
        {
            LookAtHuman(bs);
            HarmTarget();
        }

        if (!IsMoveNeeded(bs)) return;
        MoveTowardsCharacter(bs);
        if (CanAttackWithoutMoving(bs))
            HarmTarget();
    }

    private bool CanPerformCloseRangeAttack()
    {
        throw new NotImplementedException();
    }

    private void WizardAttackPattern(CharacterObservable obs)
    {
        if (!_isMoveNeeded) return;
        _closestEnemyToAttackSequence.Add(() => MoveTowardsCharacter(obs));
        if (!IsInsideAttackRange(obs)) return;
        _closestEnemyToAttackSequence.Add(HarmTarget);
    }

    private bool CanAttackWithoutMoving(CharacterObservable target)
    {
        return !IsMoveNeeded(target) && IsFacingPlayer(target);
    }

    private void HarmTarget()
    {
        var isSelfWizardType = _selfAsEnemy.IsOfTypeWizard();
        var target = _selfAsEnemy.Target;
        var isDefenseBonusActivated = target.Stats.DefenseBonusActivated;
        _selfAsEnemy.Animator.SetTrigger(isSelfWizardType ? "Attack" : "Conjuring");
        var attackPower = isSelfWizardType
            ? _selfAsEnemy.Stats.Power + _selfAsEnemy.Stats.MagicPower - CalculateDamageReduction(target)
            : _selfAsEnemy.Stats.Power - CalculateDamageReduction(target);
        target.Health.CurrentHealth -= (int)(isDefenseBonusActivated ? (attackPower - target.Stats.TemporaryDefenseBonusValue) : attackPower);
        target.Animator.SetTrigger(target.Stats.DefenseBonusActivated ? "Hurt" : "Defense");
        target.Notify();
    }

    private void HealTeamMember()
    {
    }

    private void LookAtHuman(CharacterObservable target)
    {
        //Invoke RotateCommand
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

    public sealed override void EvaluateCommandSequence()
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

    public new void SelectTarget(CharacterObservable[] humans)
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