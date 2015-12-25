public interface IReceiver
{
    void SetUserAction(GameActions userAction);

    void PerformCommand(ICharacter caller, ICharacter characterToInteractWith);
}