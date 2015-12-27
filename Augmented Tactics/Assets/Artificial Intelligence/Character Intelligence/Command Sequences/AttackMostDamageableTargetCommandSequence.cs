using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Artificial_Intelligence.Character_Intelligence.Command_Sequences
{
    public class AttackMostDamageableTargetCommandSequence : AttackCommandSequence
    {
        private List<Action> _mostDamageableTargetCommandSequenceAction;
        private int _mostDamageableRating;
        private bool _isMoveNeeded;
        private bool _isFacingCharacter;
        private bool _isTargetInAttackRange;
        private EnemyBase _selfAsEnemy;
        private List<Cell> _pathToTarget; 
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

        private bool IsTargetCloseBy(Cell destCell)
        {
            //Destination should not be null
            if (destCell == null || Observable.CurrentCoordinates == null)
                return false;

            //There is no character on this cell
            if (!destCell.UseByCharacter)
                return false;

            var movePoints = Observable.MovementPoints;
            var obsCoordinates = Observable.CurrentCoordinates.gridPosition;
            var targetCoordinates = destCell.gridPosition;
            return obsCoordinates.x + movePoints <= targetCoordinates.x ||
                   obsCoordinates.x - movePoints <= targetCoordinates.x &&
                   obsCoordinates.y + movePoints <= targetCoordinates.y ||
                   obsCoordinates.y - movePoints <= targetCoordinates.y;
        }

        private List<CharacterObservable> FilterDamageableTargets(CharacterObservable[] targets)
        {
            var targetRating = new Dictionary<CharacterObservable, int>();
            foreach (var target in targets)
            {
                var offense = (int)_selfAsEnemy.Stats.Power; //Missing equipment bonus points for power
                var defense = (int)target.Stats.Defense;     //Missing equipment bonus points for defense
                targetRating.Add(target, offense - defense);
            }

            return targetRating.OrderByDescending(key => key.Value).Take(3).Select(kvp => kvp.Key).ToList();
        }

        private bool CanReachTargetWithinTurns(CharacterObservable target, int numberTurn)
        {
            var enemyObservable = (EnemyBase)Observable;
            var start = enemyObservable.CurrentCoordinates;
            var end = target.CurrentCoordinates;
            var pathfinder = enemyObservable.PathFinder;
            pathfinder.FindPath(start, end, enemyObservable.Map.CellGameMap, false);
            if (enemyObservable.MovementPoints*numberTurn < pathfinder.CellsFromPath().Count) return false;
            _pathToTarget = pathfinder.CellsFromPath(numberTurn*enemyObservable.MovementPoints);
            return true;
        }

        private bool IsOutcomeFavorable(CharacterObservable target)
        {
            return false;
        }

        public override void SelectTarget(CharacterObservable[] humans)
        {
            var top3 = FilterDamageableTargets(humans);
            foreach (var target in top3)
            {
                var isFavorable = CanReachTargetWithinTurns(target, 2);
                _top3State.Add(target, isFavorable);
            }
        }
    }
}