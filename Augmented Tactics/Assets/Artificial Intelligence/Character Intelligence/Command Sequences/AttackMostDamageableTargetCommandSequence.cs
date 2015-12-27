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
            var targetRating = new Dictionary<CharacterObservable,int>();
            foreach (var target in targets)
            {
                var offense = (int)_selfAsEnemy.Stats.Power; //Missing equipment bonus points for power
                var defense = (int)target.Stats.Defense;     //Missing equipment bonus points for defense
                targetRating.Add(target,offense-defense);
            }

            return targetRating.OrderByDescending(key => key.Value).Take(3).Select(kvp => kvp.Key).ToList();
        }

        private bool CanReachTargetWithinTurns(CharacterObservable target, int numberTurn)
        {
            throw new NotImplementedException();
        }

        private void PredictTargetMoves(CharacterObservable target)
        {
            
        }

        public override void SelectTarget(CharacterObservable[] humans)
        {
            var top3 = FilterDamageableTargets(humans);
            foreach (var target in top3)
            {
                var isFavorable = CanReachTargetWithinTurns(target,2);
                _top3State.Add(target,isFavorable);
            }
        }
    }
}
