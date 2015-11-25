using UnityEngine;
using System.Collections;

public interface ICharacterActionCommand  {
	IReceiver Receiver { get; set; }
	bool IsExecuted { get; set; }
	
	void Execute(ICharacter caller);
	void Execute(ICharacter caller, ICharacter characterToAttack);
	void Execute(ICharacter caller, int raiseDefense);
}
