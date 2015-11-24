using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System; 

public abstract class CharacterObservable : MonoBehaviour, ICharacterObservable {
	protected ArrayList ObserversList = new ArrayList();
	public CharacterRole CharacterRole;
	public bool isDamage;
	public bool healthWasRaised;
	public CharacterStats BaseStats { get; set; }
	public Health Health { get; set; }
	public List<ICharacter> CurrentEnnemies { get; set; }
	public List<ICharacter> TeamMembers { get; set; }
	public int MovementPoints { get; set; }
	public PlayerDirection Direction { get; set; }
	public int[] Position { get; set; } // [0]=x, [1]=y
	public Animator Animator { get; set; }
	public CharacterType CharacterType { get; set; }
	//public Cell CurrentCoordinates { get; set; }
	//public Cell OldCoordinates { get; set; }
	
	public abstract bool CanDoExtraDamage();
	public abstract void CheckIfCharacterDead();


	public void Attach(ICharacterObserver observer)
	{
		if (observer == null)
			throw new ArgumentNullException();
		if (ObserversList.Contains(observer))
			throw new InvalidOperationException("Cannot have duplicate observers.");
		ObserversList.Add(observer);
	}
	
	public void Detach(ICharacterObserver observer)
	{
		if(observer == null)
			throw new ArgumentNullException();
		if(!ObserversList.Contains(observer))
			throw  new InvalidOperationException("Cannot remove observer when observer not in the list.");
		if(ObserversList.Count == 0)
			throw new InvalidOperationException("The observer list is empty. Cannot detach.");
		ObserversList.Remove(observer);
	}
	
	public abstract void Notify();


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
