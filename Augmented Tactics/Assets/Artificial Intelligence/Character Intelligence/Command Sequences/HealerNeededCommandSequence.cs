using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Artificial_Intelligence.Character_Intelligence.Command_Sequences
{
    public class HealerNeededCommandSequence : DefensiveCommandSequence
    {
        private List<Action> _healingActionToPerform;
        private Dictionary<EnemyBase, Cell> _healerData;
        public int _healingRating = 0;
        private bool _movementNotNeeded;
        private bool _isHealerInRange;

        public HealerNeededCommandSequence()
        {
            _healerData = new Dictionary<EnemyBase, Cell>();
            _healingActionToPerform = new List<Action>();
            _healingRating = 0;
            FindHealer();
            EvaluateCommandSequence();
        }

        private bool IsHealingMageInRange(Cell destCell)
        {
            //Destination should not be null
            if (destCell == null || Observable.CurrentCoordinates == null)
                return false;

            //There is no character on this cell
            if (!destCell.UseByCharacter)
                return false;

            var movePoints = Observable.MovementPoints;
            var obsCoordinates = Observable.CurrentCoordinates.gridPosition;
            var healerCoordinates = destCell.gridPosition;
            return obsCoordinates.x + movePoints <= healerCoordinates.x ||
                   obsCoordinates.x - movePoints <= healerCoordinates.x &&
                   obsCoordinates.y + movePoints <= healerCoordinates.y ||
                   obsCoordinates.y - movePoints <= healerCoordinates.y;
        }

        private void FindHealer()
        {
            var enemyObs = (EnemyBase)Observable;
            var obsParty = enemyObs.TeamParty;
            EnemyBase partyHealer = (EnemyBase)obsParty.FirstOrDefault(member => member is NormalHealer);
            var healerCell = enemyObs.Map.FindPlayerCoordinates(partyHealer);
            if (partyHealer == null || healerCell == null)
                return;
            _healerData.Add(partyHealer, healerCell);
        }

        public override void MoveTowardsCharacter(CharacterObservable character)
        {
            var enemyObservable = (EnemyBase)Observable;
            var start = enemyObservable.CurrentCoordinates;
            var end = character.CurrentCoordinates;
            var pathfinder = enemyObservable.PathFinder;
            pathfinder.FindPath(start, end, enemyObservable.Map.CellGameMap, false);
            enemyObservable.Animator.SetTrigger("Move");
            //need to call move command
        }

        private bool IsMoveNeeded()
        {
            var healer = _healerData.First().Key;
            var enemy = (EnemyBase)Observable;
            var directionMoves = enemy.Map.GetAvailableMoveActions(enemy); // Will need to modify to a simple call from the GameManager property that will accessible from everywhere
            return directionMoves.Any(x => x.gridPosition == _healerData.First().Value.gridPosition);
        }

        public override List<Action> MakeCommandSequence()
        {
            if (!_movementNotNeeded)
                _healingActionToPerform.Add(() => MoveTowardsCharacter(_healerData.First().Key));
            _healingActionToPerform.Add(InvokeDefendBonus);
            return _healingActionToPerform;
        }

        public override void EvaluateCommandSequence()
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

            _movementNotNeeded = IsMoveNeeded();
            if (_movementNotNeeded)
            {
                _healingRating += 50;
                return;
            }

            var destCell = _healerData.First().Value;
            _isHealerInRange = IsHealingMageInRange(destCell);
            if (_isHealerInRange)
                _healingRating += 25;
        }

        private bool InDireNeedOfHealingMagic(float percent)
        {
            var curHealth = (float)Observable.Health.CurrentHealth;
            var maxHealth = (float)Observable.Health.MaxHealth;
            return curHealth / maxHealth <= percent;
        }
    }
}