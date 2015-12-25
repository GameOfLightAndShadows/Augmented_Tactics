using UnityEngine;

public interface IStats
{
    float HealthPoints { get; set; }
    float Power { get; set; }
    float Defense { get; set; }
    float Agility { get; set; }
    float Speed { get; set; }
    float MagicPower { get; set; }
    float MagicResist { get; set; }
    int ChanceForCriticalStrike { get; set; }
    float Luck { get; set; }
    Random RandomValueGenerator { get; set; }

    IStats StatGeneration(float h, float p, float d, float a, float s, float mp, float mr, float l);

    bool CheckStatsValues();

    bool Equals(object obj);

    int GetAttackStrenght(ICharacter fst, ICharacter snd);
}