using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public float HealthPoints { get; set; }
    public float Power { get; set; }
    public float Defense { get; set; }
    public float Agility { get; set; }
    public float Speed { get; set; }
    public float MagicPower { get; set; }
    public float MagicResist { get; set; }
    public int ChanceForCriticalStrike { get; set; }
    public float Luck { get; set; }
    public int CriticalStrikeCounter = 20;
    public int TemporaryDefenseBonusValue;
    public System.Random RandomValueGenerator { get; set; }
    public bool DefenseBonusActivated = false;

    public IStats StatGeneration(float h, float p, float d, float a, float s, float mp, float mr, float l)
    {
        Power = p;
        Defense = d;
        Agility = a;
        Speed = s;
        MagicPower = mp;
        MagicResist = mr;
        Luck = l;
        HealthPoints = h;
        RandomValueGenerator = new System.Random();
        return (IStats)this;
    }

    public void AjustCriticalStrikeChances()
    {
        if (CriticalStrikeCounter <= 5)
        {
            CriticalStrikeCounter = 5;
        }
    }

    public int DetermineDefenseBonusForTurn()
    {
        TemporaryDefenseBonusValue = RandomValueGenerator.Next(10, 20);
        return TemporaryDefenseBonusValue;
    }

    public void ResetTemporaryBonus()
    {
        TemporaryDefenseBonusValue = 0;
    }

    public bool CheckStatsValues()
    {
        if (HealthPoints <= 0) return false;
        if (Power <= 0) return false;
        if (Defense <= 0) return false;
        if (Speed <= 0) return false;
        if (Luck <= 0) return false;
        if (MagicPower <= 0) return false;
        if (MagicResist <= 0) return false;
        return true;
    }

    public override bool Equals(object obj)
    {
        var that = (CharacterStats)obj;
        return GetType() == that.GetType() &&
            Power == that.Power &&
                Defense == that.Defense &&
                Agility == that.Agility &&
                Speed == that.Speed &&
                MagicPower == that.MagicPower &&
                MagicResist == that.MagicResist;
    }

    /// <summary>
    /// Returns the attack strenght of the blow from the attack
    /// </summary>
    /// <param name="fst">The attacker</param>
    /// <param name="snd">The defender</param>
    /// <returns></returns>
    public int GetAttackStrenght(ICharacter fst, ICharacter snd)
    {
        var fStats = fst.Stats;
        var sStats = snd.Stats;
        var power = (fst is BattleWizard || fst is Wizard)
            ? (int)(fStats.MagicPower + fStats.Power)
                : (int)fStats.Power;
        var defense = (snd.Stats.DefenseBonusActivated)
            ? (int)sStats.Defense + sStats.TemporaryDefenseBonusValue
                : (int)sStats.Defense;
        if (fst is BattleWizard || fst is Wizard)
            defense += (int)sStats.MagicResist;

        return power - defense;
    }
}