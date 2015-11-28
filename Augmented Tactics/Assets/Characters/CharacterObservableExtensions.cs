public static class CharacterObservableExtensions
{
    //IsOfType methods will have to be optimized to a single method in the future by simply passing a params of Type array
    //Presently, code works this way

    public static bool IsOfTypeWizard(this CharacterObservable obs)
    {
        return obs.CharacterRole.Equals(CharacterRole.Wizard) &&
               (!obs.GetType().IsSubclassOf(typeof(BattleWizard)) ||
                !obs.GetType().IsSubclassOf(typeof(Healer)) ||
                !obs.GetType().IsSubclassOf(typeof(Wizard)));
    }

    public static bool IsOfTypeSniper(this CharacterObservable obs)
    {
        return obs.CharacterRole.Equals(CharacterRole.Sniper) &&
               !obs.GetType().IsSubclassOf(typeof(Archer));
    }

    public static bool IsOfTypeWarrior(this CharacterObservable obs)
    {
        return obs.CharacterRole.Equals(CharacterRole.Warrior) &&
               (!obs.GetType().IsSubclassOf(typeof(Knight)) ||
                !obs.GetType().IsSubclassOf(typeof(Swordsman)));
    }

    public static bool IsOfTypeThief(this CharacterObservable obs)
    {
        return obs.CharacterRole.Equals(CharacterRole.Thief) &&
               !obs.GetType().IsSubclassOf(typeof(Assassin));
    }
}