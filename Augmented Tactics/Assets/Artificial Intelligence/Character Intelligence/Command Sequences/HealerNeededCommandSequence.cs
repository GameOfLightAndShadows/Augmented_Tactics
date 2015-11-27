using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
public class HealerNeededCommandSequence : DefensiveCommandSequence {
	private List<Action> _healingActionToPerform;
	private Dictionary<BaseEnemy, Cell> _healerData;
	private int _healingRating=0;
	private bool _movementNotNeeded;
	private bool _isHealerInRange;

	public CommandSequence MakeCommandSequence()
	{
		return null;
	}

	public void EvaluateCommandSequence()
	{
		if (!InDireNeedOfHealingMagic(0.5f))
			return;
		_healingRating += 50;
		if (InDireNeedOfHealingMagic(0.4f))
			_healingRating += 80;
		if (InDireNeedOfHealingMagic(0.3f))
			_healingRating += 120;
		if (InDireNeedOfHealingMagic(0.2f))
			_healingRating += 160;
		if (InDireNeedOfHealingMagic(0.1f))
			_healingRating += 200;
	}


	private bool InDireNeedOfHealingMagic(float percent)
	{
		var curHealth = (float)Observable.Health.CurrentHealth;
		var maxHealth = (float)Observable.Health.MaxHealth;
		return curHealth / maxHealth <= percent;
	}
}
