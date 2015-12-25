using System;

public class HealthManager : ICharacterObserver
{
    public void UpdateObserver(CharacterObservable character)
    {
        throw new NotImplementedException();
    }

    public void UpdateObserver(CharacterObservable obs, float strength, bool wasAttacked)
    {
        if (wasAttacked)
        {
            obs.Health.TakeDamageFromCharacter(strength);
            //Make sure the Health UI was changed too !
            if (obs.Health.IsDead)
                GameManager.instance.UpdateObserver(obs);
        }
        else
        {
            obs.Health.RaiseHealth((int)strength);
        }
    }

    // Use this for initialization
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }
}