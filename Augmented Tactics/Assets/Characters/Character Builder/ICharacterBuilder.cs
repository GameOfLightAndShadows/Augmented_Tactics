using UnityEngine;
using System.Collections;

public interface ICharacterBuilder {
	CharacterBuilder CreateCharacterBuilder(GameManager gm, HealthManager hm);
	CharacterBuilder WithDirection(int direction);
	CharacterBuilder WithHealth();
	CharacterBuilder WithObservers();
	CharacterBuilder WithStats();
	CharacterBuilder WithType(int type);
	CharacterObservable BuildCharacter();
}
