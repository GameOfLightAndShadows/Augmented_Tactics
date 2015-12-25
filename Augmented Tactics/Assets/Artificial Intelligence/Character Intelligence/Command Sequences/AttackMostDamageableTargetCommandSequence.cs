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

        public override void SelectTarget(CharacterObservable[] humans)
        {
            throw new NotImplementedException();
        }
    }
}
