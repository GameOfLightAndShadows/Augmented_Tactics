using System.Linq;
using Random = System.Random;

public class CharacterBase : CharacterObservable
{
    public bool CanDoExtraDamage()
    {
        if (Stats.ChanceForCriticalStrike * Stats.Luck < 50) return false;
        Stats.CriticalStrikeCounter--;
        Stats.ChanceForCriticalStrike = new Random().Next(0, Stats.CriticalStrikeCounter);
        Stats.AjustCriticalStrikeChances();
        return true;
    }

    public override bool Equals(object obj)
    {
        var that = (CharacterBase)obj;
        return ReferenceEquals(this, that) &&
                GetType() == that.GetType() &&
                Health.CurrentHealth == that.Health.CurrentHealth &&
                Health.MaxHealth == that.Health.MaxHealth &&
                Stats == that.Stats;
    }

    public override void Notify()
    {
        if (isDamage || healthWasRaised)
        {
            foreach (var o in ObserversList.OfType<HealthManager>())
            {
                o.UpdateObserver(this);
            }
        }

        if (Health.IsDead)
        {
            foreach (var o in ObserversList.OfType<GameManager>())
            {
                o.UpdateObserver(this);
            }
        }
    }

    // Use this for initialization
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }
}