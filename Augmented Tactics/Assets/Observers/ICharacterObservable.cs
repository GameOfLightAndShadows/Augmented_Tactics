using UnityEngine;
using System.Collections;

public interface ICharacterObservable : ICharacter {
	void Attach(ICharacterObserver observer);
	void Detach(ICharacterObserver observer);
	void Notify();
}
