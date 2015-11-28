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

    protected int CalculateDamageReduction(CharacterBase target)
    {
        return (int) (target.IsOfTypeWizard()
            ? target.Stats.Defense + target.Stats.MagicResist
            : target.Stats.Defense);
    }

    private bool CanDoExtraDamage()
    {
        var nextValue = valuesGenerator.Next(0, _count);
        _count--;
        return _count > 6;
    }

    private float GetIncreaseTemporaryStrength(float strength)
    {
        return strength * 0.15f;
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


    protected bool IsFacingPlayer(ICharacter human)
    {
        return Math.Abs(Observable.Direction - human.Direction) == 1;
    }

    protected bool IsInsideAttackRange(CharacterObservable bs)
    {
        var enemyObservable = (EnemyBase)Observable;
        return enemyObservable.Map.IsInRange(enemyObservable, bs);
    }

    private int CalculateDamageReduction(CharacterObservable human)
    {
        return (int)(human.IsOfTypeWizard()
            ? human.Stats.Defense + human.Stats.MagicResist
            : human.Stats.Defense);
    }

    public abstract void SelectTarget(CharacterObservable[] humans);
}