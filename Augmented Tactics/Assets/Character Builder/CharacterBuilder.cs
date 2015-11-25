using UnityEngine;
using System.Collections;

public class CharacterBuilder : ICharacterBuilder {

	private CharacterObservable _character;
	private CharacterStats _stats;
	private Health _health;
	private GameManager _gm;
	private HealthManager _hm;
	private PlayerDirection _direction;

}
