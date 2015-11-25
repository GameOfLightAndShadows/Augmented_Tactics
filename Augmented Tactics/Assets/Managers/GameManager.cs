using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class GameManager : ICharacterObserver {
	private int _indexOfCharacters;
	public List<CharacterObservable> GameCharacters;
	public CharacterObservable ActivePlayer;

	public void GoToNextCharacter()
	{
		_indexOfCharacters = _indexOfCharacters + 1 < GameCharacters.Count ? _indexOfCharacters + 1 : 0;
		ActivePlayer = GameCharacters[_indexOfCharacters];
	}

	public void UpdateObserver(CharacterObservable character)
	{
		throw new NotImplementedException ();
	}

	// Use this for initialization
	void Start () {
		_indexOfCharacters = 0;
		ActivePlayer = GameCharacters [_indexOfCharacters];
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
