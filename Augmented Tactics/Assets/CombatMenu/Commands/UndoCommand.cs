using System;

public class UndoCommand : CharacterAction
{
    public UndoCommand(IReceiver receiver)
        : base(receiver)
    {
    }

    public void Execute(CharacterObservable caller, CharacterObservable interactWith, CommandManager cm, GameManager gm)
    {
        if (caller == null)
            throw new ArgumentException("Caller cannot be null");
        if (cm.CommandStack == null)
            throw new NullReferenceException("Command stack cannot be null in order to perform an undo");
        if (cm.CurrentCharater == null && gm.ActivePlayer == null)
            throw new ArgumentException("The command and game manager can't be null at the same time");
        if (cm.CommandStack.Count == 0)
            throw new Exception("Active player did not select any actions that can be undone");
        if (cm != null && gm != null && !cm.CurrentCharater.Equals(gm.ActivePlayer))
            throw new Exception("Cannot Undo action of previous player");
        cm.Undo(caller, gm.ActivePlayer);
    }
}