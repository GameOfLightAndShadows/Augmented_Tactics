using System;
using System.Linq;

public static class CharacterObservableExtensions
{
    //IsOfType methods will have to be optimized to a single method in the future by simply passing a params of Type array
    //Presently, code works this way

    public static CharacterObservable ConvertAbstractToConcreteSniper(this CharacterObservable observable)
    {
        var go = observable.ObservableGameObject;
        if (!observable.IsOfTypeSniper()) return null;
        if (observable is Archer)
        {
            var archer = go.AddComponent<Archer>();
            archer.Health = observable.Health;
            archer.Stats = observable.Stats;
            archer.CharacterType = CharacterType.Archer;
            archer.CharacterRole = CharacterRole.Sniper;
            archer.Direction = observable.Direction;
            return archer;
        }
        if (!(observable is NormalArcher)) return null;
        {
            var archer = go.AddComponent<NormalArcher>();
            archer.Health = observable.Health;
            archer.Stats = observable.Stats;
            archer.CharacterType = CharacterType.Archer;
            archer.CharacterRole = CharacterRole.Sniper;
            archer.Direction = observable.Direction;
            return archer;
        }
    }

    public static bool IsNull(this CharacterObservable obs)
    {
        return obs == null;
    }

    public static bool IsSubClassOfType(this CharacterObservable obs, Type parentType)
    {
        return obs.GetType().IsInstanceOfType(parentType);
    }

    public static bool IsSubClassOfType(this CharacterObservable obs, params Type[] types)
    {
        return types.Any(t => obs.GetType().IsInstanceOfType(t));
    }

    public static bool IsOfTypeWizard(this CharacterObservable obs)
    {
        return obs.CharacterRole.Equals(CharacterRole.Wizard);
    }

    public static bool IsOfTypeSniper(this CharacterObservable obs)
    {
        return obs.CharacterRole.Equals(CharacterRole.Sniper);
    }

    public static bool IsOfTypeWarrior(this CharacterObservable obs)
    {
        return obs.CharacterRole.Equals(CharacterRole.Warrior);
    }

    public static bool IsOfTypeThief(this CharacterObservable obs)
    {
        return obs.CharacterRole.Equals(CharacterRole.Thief);
    }
}