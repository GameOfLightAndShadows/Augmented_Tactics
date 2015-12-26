using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Artificial_Intelligence.Character_Intelligence.Command_Sequences
{
    public class AttackMostDamageableTargetCommandSequence: AttackCommandSequence
    {
        private List<Action> _mostDamageableTargetCommandSequenceAction;
        private int _mostDamageableRating;
        private bool _isMoveNeeded;
        private bool _isFacingCharacter;
        private bool _isTargetInAttackRange;
        private EnemyBase _selfAsEnemy;
        private Dictionary<CharacterObservable, bool> _top3State; 
         
        public AttackMostDamageableTargetCommandSequence(params CharacterObservable[] humans)
        {
            _selfAsEnemy = (EnemyBase)Observable;
            SelectTarget(humans);
            EvaluateCommandSequence();
        }

        public override List<Action> MakeCommandSequence()
        {
            throw new NotImplementedException();
        }

        public override void MoveTowardsCharacter(CharacterObservable character)
        {
            throw new NotImplementedException();
        }

        public override void EvaluateCommandSequence()
        {
            throw new NotImplementedException();
        }

        private List<CharacterObservable> FilterDamageableTargets(CharacterObservable[] targets)
        {
            throw new NotImplementedException();
        }

        private bool IsNextTurnFavorableWithTarget(CharacterObservable target)
        {
            throw new NotImplementedException();
        }

        public override void SelectTarget(CharacterObservable[] humans)
        {
            var top3 = FilterDamageableTargets(humans);
            foreach (var target in top3)
            {
                var isFavorable = IsNextTurnFavorableWithTarget(target);
                _top3State.Add(target,isFavorable);
            }
        }
    }
}
