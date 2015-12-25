using Assets.Artificial_Intelligence.Character_Intelligence.Command_Sequences;
using System;
using System.Collections.Generic;
using System.Linq;

public class ChoiceSelector
{
    private List<CommandSequence> _strategies;
    private Dictionary<List<Action>, int> _actionsAndScores;

    public ChoiceSelector(params CommandSequence[] sequences)
    {
        _strategies = sequences.ToList();
        _actionsAndScores = new Dictionary<List<Action>, int>(_strategies.Capacity);
    }

    public void ExecuteBestMove()
    {
        var bestMove = SelectCommandSequence();
        foreach (var action in bestMove)
        {
            action.Invoke();
        }
    }

    private List<Action> SelectCommandSequence()
    {
        Think();
        var bestMoves = _actionsAndScores.Aggregate((l, r) => l.Value > r.Value ? l : r).Key;
        return bestMoves;
    }

    private void Think()
    {
        foreach (var s in _strategies)
        {
            s.EvaluateCommandSequence();
            _actionsAndScores.Add(s.MakeCommandSequence(), s.CommandSequenceScore);
        }
    }
}