public interface ICharacterActionCommand
{
    IReceiver Receiver { get; set; }
    bool IsExecuted { get; set; }

    void Execute();

    void Execute(CharacterObservable caller);

    void Execute(CharacterObservable caller, CharacterObservable characterToAttack);

    void Execute(CharacterObservable caller, int raiseDefense);
}