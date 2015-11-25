using UnityEngine;
using System.Collections;
using System;
public class CharacterBuilder : ICharacterBuilder {

	private CharacterObservable _character;
	private CharacterStats _stats;
	private Health _health;
	private GameManager _gm;
	private HealthManager _hm;
	private PlayerDirection _direction;

	public CharacterObservable BuildCharacter()
	{
		if (_character.Health == null ||
		    _character.BaseStats == null ||
		    _character.Direction == default(PlayerDirection) ||
		    _character.Position == null ||
		    !_character.GetType().IsSubclassOf(typeof(ICharacter)))
			throw new NullReferenceException("Base components for character creation cannot be null");
		return _character;
	}

	CharacterBuilder CreateCharacterBuilder(GameManager gm, HealthManager hm)
	{
		if (gm == null || hm == null)
			throw new ArgumentNullException ();
		_gm = gm;
		_hm = hm;
		return this;
	}

	CharacterBuilder WithDirection(int direction)
	{
		_direction = (PlayerDirection)direction;
		return this;
	}
	CharacterBuilder WithHealth()
	{
		if (_stats == null)
			throw new NullReferenceException ("Stats cannot be null otherwise character cannot have health");
		_health = new Health ((int)_stats.HealthPoints).HealthGeneration ();
		return this;
	}
	CharacterBuilder WithObservers()
	{
		_character.Attach (_gm);
		_character.Attach (_hm);
		return this;
	}
	CharacterBuilder WithStats();
	CharacterBuilder WithType(int type)
	{
		_character.CharacterType = (CharacterType)type;
		return this;
	}
}
