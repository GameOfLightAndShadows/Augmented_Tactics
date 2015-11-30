using System;

public class SkipCommand : CharacterAction
{
    public SkipCommand(IReceiver receiver)
        : base(receiver)
    {
    }

    public void Execute(ICharacter caller, GameManager manager)
    {
        Receiver.SetUserAction(GameActions.SkipAction);
        if (caller == null)
            throw new ArgumentException();
        if (manager == null)
            throw new ArgumentException();
        manager.GoToNextCharacter();
    }
}