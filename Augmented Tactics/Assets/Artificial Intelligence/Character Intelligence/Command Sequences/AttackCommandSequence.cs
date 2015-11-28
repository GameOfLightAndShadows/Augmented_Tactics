using System;
using System.Collections.Generic;
using System.Linq;

public abstract class AttackCommandSequence : CommandSequence
{
    public CharacterObservable Observable { get; set; }
    protected System.Random valuesGenerator;
    protected int _count = 10;
    protected List<Action> AttackSequence;

    //TODO: Move directly inside CommandSequence
    public void AddObservable(CharacterObservable obs)
    {
        if (obs != null)
            Observable = obs;
        else
            throw new ArgumentNullException();
    }

    private bool CanDoExtraDamage()
    {
        var nextValue = valuesGenerator.Next(0, _count);
        _count--;
        return _count > 6;
    }

    private float GetIncreaseTemporaryStrength(float strength)
    {
        return strength * 0.25f;
    }

    private void HarmCharacter(CharacterObservable human)
    {
        var isSelfWizardType = Observable.IsOfTypeWizard();
        var isDefenseBonusActivated = human.Stats.DefenseBonusActivated;
        Observable.Animator.SetTrigger(isSelfWizardType ? "Attack" : "Conjuring");
        var attackPower = isSelfWizardType
            ? Observable.Stats.Power + Observable.Stats.MagicPower - CalculateDamageReduction(human)
            : Observable.Stats.Power - CalculateDamageReduction(human);
        if (CanDoExtraDamage())
            attackPower *= GetIncreaseTemporaryStrength(attackPower);
        human.Health.TakeDamageFromCharacter((isDefenseBonusActivated ? (attackPower - human.Stats.TemporaryDefenseBonusValue) : attackPower));
        human.Animator.SetTrigger(isDefenseBonusActivated ? "Hurt" : "Defense");
        human.Notify();
    }

    private int CalculateDamageReduction(CharacterObservable human)
    {
        return (int)(human.IsOfTypeWizard()
            ? human.Stats.Defense + human.Stats.MagicResist
            : human.Stats.Defense);
    }

    private void SelectTarget(params CharacterObservable[] humans)
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
        observableAsEnemy.Target = (CharacterBase)closestCharacter;
    }
}