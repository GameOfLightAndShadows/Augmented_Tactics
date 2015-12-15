using System.Linq;
using UnityEngine;
using Random = System.Random;

public class CharacterBase : CharacterObservable
{
    public CommandManager _cmdManager;

    public bool CanDoExtraDamage()
    {
        if (Stats.ChanceForCriticalStrike * Stats.Luck < 50) return false;
        Stats.CriticalStrikeCounter--;
        Stats.ChanceForCriticalStrike = new Random().Next(0, Stats.CriticalStrikeCounter);
        Stats.AjustCriticalStrikeChances();
        return true;
    }

    public override bool Equals(object obj)
    {
        var that = (CharacterBase)obj;
        return ReferenceEquals(this, that) &&
                GetType() == that.GetType() &&
                Health.CurrentHealth == that.Health.CurrentHealth &&
                Health.MaxHealth == that.Health.MaxHealth &&
                Stats == that.Stats;
    }

    public override void Notify()
    {
        if (isDamage || healthWasRaised)
        {
            foreach (var o in ObserversList.OfType<HealthManager>())
            {
                o.UpdateObserver(this);
            }
        }

        if (Health.IsDead)
        {
            foreach (var o in ObserversList.OfType<GameManager>())
            {
                o.UpdateObserver(this);
            }
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

    public override void TurnOnGUI()
    {
        float buttonHeight = 50;
        float buttonWidth = 150;

        Rect buttonRect = new Rect(0, Screen.height - buttonHeight * 7, buttonWidth, buttonHeight);
        ICharacterActionCommand cmd;
        //move button
        if (GUI.Button(buttonRect, "Move"))
        {
            if (!_isMoving)
            {
                GameManager.instance.removeTileHighlights();
                _isMoving = true;
                _isAttacking = false;
                _hasPerformedAtLeastOneMove = true;
                _isDefending = false;
                _isRotating = false;
                cmd = new MoveCommand(null, null);
                _cmdManager.CommandStack.Push(cmd);
                cmd.Execute();
                GameManager.instance.highlightTilesAt(gridPosition, Color.blue, movementPerActionPoint, false);
            }
            else
            {
                _isMoving = false;
                _isAttacking = false;
                _hasPerformedAtLeastOneMove = false;
                _isDefending = false;
                _isRotating = false;
                //GameManager.instance.removeTileHighlights();
            }
        }

        //attack/heal button
        buttonRect = new Rect(0, Screen.height - buttonHeight * 6, buttonWidth, buttonHeight);

        if (GUI.Button(buttonRect, "Attack"))
        {
            if (!_isAttacking)
            {
                //GameManager.instance.removeTileHighlights();
                _isMoving = false;
                _isAttacking = true;
                _hasPerformedAtLeastOneMove = true;
                _isDefending = false;
                _isRotating = false;
                cmd = new AttackCommand(null);
                _cmdManager.CommandStack.Push(cmd);
                cmd.Execute();
                //GameManager.instance.highlightTilesAt(gridPosition, Color.red, attackRange);
            }
            else
            {
                _isMoving = true;
                _isAttacking = false;
                _hasPerformedAtLeastOneMove = true;
                _isDefending = false;
                _isRotating = false;
                //GameManager.instance.removeTileHighlights();
            }
        }

        //defend button
        buttonRect = new Rect(0, Screen.height - buttonHeight * 5, buttonWidth, buttonHeight);

        if (GUI.Button(buttonRect, "Defend"))
        {
            if (!_isDefending)
            {
                //GameManager.instance.removeTileHighlights();
                _isMoving = false;
                _isAttacking = false;
                _hasPerformedAtLeastOneMove = true;
                _isDefending = true;
                _isRotating = false;
                cmd = new DefendCommand(null);
                _cmdManager.CommandStack.Push(cmd);
                cmd.Execute();
                //GameManager.instance.highlightTilesAt(gridPosition, Color.red, attackRange);
            }
            else
            {
                _isMoving = false;
                _isAttacking = false;
                _isDefending = false;
                _isRotating = false;
                //GameManager.instance.removeTileHighlights();
            }
        }

        //rotate button
        buttonRect = new Rect(0, Screen.height - buttonHeight * 4, buttonWidth, buttonHeight);

        if (GUI.Button(buttonRect, "Rotate"))
        {
            if (!_isRotating)
            {
                //GameManager.instance.removeTileHighlights();
                _isMoving = false;
                _isAttacking = true;
                _hasPerformedAtLeastOneMove = false;
                _isDefending = false;
                _isRotating = true;
                cmd = new RotateCommand(null);
                _cmdManager.CommandStack.Push(cmd);
                cmd.Execute();
                //GameManager.instance.highlightTilesAt(gridPosition, Color.red, attackRange);
            }
            else
            {
                _isMoving = true;
                _isAttacking = false;
                _hasPerformedAtLeastOneMove = true;
                _isDefending = false;
                _isRotating = false;
                //GameManager.instance.removeTileHighlights();
            }
        }

        //undo button
        buttonRect = new Rect(0, Screen.height - buttonHeight * 3, buttonWidth, buttonHeight);

        if (GUI.Button(buttonRect, "Undo"))
        {
            if (_hasPerformedAtLeastOneMove)
            {
                _cmdManager.Undo(this, _cmdManager.LastCharacterAttacked);
                if (_cmdManager.CommandStack.Count == 0)
                    _hasPerformedAtLeastOneMove = false;
            }
        }

        //skip button
        buttonRect = new Rect(0, Screen.height - buttonHeight * 2, buttonWidth, buttonHeight);

        if (GUI.Button(buttonRect, "Skip"))
        {
            GameManager.instance.GoToNextCharacter();
        }

        //end turn button
        buttonRect = new Rect(0, Screen.height - buttonHeight * 1, buttonWidth, buttonHeight);

        if (GUI.Button(buttonRect, "End Turn"))
        {
            GameManager.instance.GoToNextCharacter();
        }

        base.TurnOnGUI();
    }
}