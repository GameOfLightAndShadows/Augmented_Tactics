using UnityEngine;
using System.Collections;
using System;
 
public class Health {

	private int _currentHealth;
	private int _counter;
	private int _lifePoints;
	public int CurrentHealth
	{
		get { return _currentHealth; }
		set
		{
			_currentHealth = value;
			//See previous versions of C# to find out how to invoke Event Handler
			//HealthChanged?.Invoke(this, new HealthChangedEventArgs(0f, _currentHealth, MaxHealth));
		}
	}
	public int MaxHealth { get; set; }
	public bool IsDead { get; set; }
//	public event EventHandler<HealthChangedEventArgs> HealthChanged;
	
	public Health(int lf)
	{
		if(lf<=0 )
			throw new ArgumentException("Life points must be superior to 0!");
		_lifePoints = lf;
	}
	
	public Health HealthGeneration()
	{
		if(this == null)
			throw new NullReferenceException("Cannot generate health when class is null");
		return new Health(_lifePoints) {CurrentHealth = _lifePoints, MaxHealth = _lifePoints, IsDead = false};
	}
	
	private void CapHealth()
	{
		if(CurrentHealth > MaxHealth)
			CurrentHealth = MaxHealth;
		if(CurrentHealth < 0)
			CurrentHealth = 0;
	}
	
	public void TakeDamageFromCharacter(CharacterObservable observable)
	{
		CurrentHealth -= (int)observable.Stats.Power;
		if (CurrentHealth <= 0)
			IsDead = true;
	}

	public void RaiseHealth(CharacterObservable observable)
	{
		if (!IsDead) {
			CurrentHealth += (int)observable.Stats.MagicPower;
			CapHealth ();
		}
	}
}
