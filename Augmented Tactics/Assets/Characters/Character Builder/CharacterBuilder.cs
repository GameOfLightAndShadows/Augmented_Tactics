using System;

public class CharacterBuilder : ICharacterBuilder
{
    private CharacterObservable _character;
    private CharacterStats _stats;
    private Health _health;
    private GameManager _gm;
    private HealthManager _hm;
    private PlayerDirection _direction;

    public CharacterObservable BuildCharacter()
    {
        if (_character.Health == null ||
            _character.Stats == null ||
            _character.Direction == default(PlayerDirection) ||
            _character.Position == null ||
            !_character.GetType().IsSubclassOf(typeof(ICharacter)))
            throw new NullReferenceException("Base components for character creation cannot be null");
        return _character;
    }

    public CharacterBuilder CreateCharacterBuilder(GameManager gm, HealthManager hm)
    {
        if (gm == null || hm == null)
            throw new ArgumentNullException();
        _gm = gm;
        _hm = hm;
        return this;
    }

    public CharacterBuilder WithDirection(int direction)
    {
        _direction = (PlayerDirection)direction;
        return this;
    }

    public CharacterBuilder WithHealth()
    {
        if (_stats == null)
            throw new NullReferenceException("Stats cannot be null otherwise character cannot have health");
        _health = new Health((int)_stats.HealthPoints).HealthGeneration();
        return this;
    }

    public CharacterBuilder WithObservers()
    {
        _character.Attach(_gm);
        _character.Attach(_hm);
        return this;
    }

    public CharacterBuilder WithStats()
    {
        if (_character.GetType() == typeof(Archer))
        {
            _stats = new CharacterStats();
            _stats.StatGeneration(2450, 260, 180, 100, 120, 0, 40, 22);

            return this;
        }
        if (_character.GetType() == typeof(Assassin))
        {
            _stats = new CharacterStats();
            _stats.StatGeneration(2380, 295, 225, 145, 150, 0, 65, 18);
            return this;
        }
        if (_character.GetType() == typeof(BattleWizard))
        {
            _stats = new CharacterStats();
            _stats.StatGeneration(2520, 225, 210, 85, 90, 140, 110, 25);
            return this;
        }

        if (_character.GetType() == typeof(Healer))
        {
            _stats = new CharacterStats();
            _stats.StatGeneration(2180, 175, 140, 80, 95, 80, 100, 16);
            return this;
        }
        if (_character.GetType() == typeof(Knight))
        {
            _stats = new CharacterStats();
            _stats.StatGeneration(2760, 310, 260, 65, 80, 0, 75, 13);
            return this;
        }

        if (_character.GetType() == typeof(Swordsman))
        {
            _stats = new CharacterStats();
            _stats.StatGeneration(2630, 300, 235, 70, 85, 0, 70, 15);
            return this;
        }
        if (_character.GetType() == typeof(Wizard))
        {
            _stats = new CharacterStats();
            _stats.StatGeneration(2380, 215, 170, 90, 90, 240, 160, 23);
            return this;
        }
        return this;
    }

    public CharacterBuilder WithType(int type)
    {
        _character.CharacterType = (CharacterType)type;
        return this;
    }
}