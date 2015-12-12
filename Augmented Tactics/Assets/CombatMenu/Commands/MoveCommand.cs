using UnityEngine;

public class MoveCommand :CharacterAction
{
    private Tile _destTile;

    public MoveCommand(Tile dest)
    {
        _destTile = dest;
    }

    public override void Execute()
    {
        moveCurrentPlayer();
    }

    private void moveCurrentPlayer()
    {
        if (destTile.visual.transform.GetComponent<Renderer>().materials[0].color != Color.white && !destTile.impassible && players[currentPlayerIndex].positionQueue.Count == 0)
        {
            removeTileHighlights();
            foreach (Tile t in TilePathFinder.FindPath(map[(int)players[currentPlayerIndex].gridPosition.x][(int)players[currentPlayerIndex].gridPosition.y], destTile, players.Where(x => x.gridPosition != destTile.gridPosition && x.gridPosition != players[currentPlayerIndex].gridPosition).Select(x => x.gridPosition).ToArray()))
            {
                players[currentPlayerIndex].positionQueue.Add(map[(int)t.gridPosition.x][(int)t.gridPosition.y].transform.position + 1.5f * Vector3.up);
                Debug.Log("(" + players[currentPlayerIndex].positionQueue[players[currentPlayerIndex].positionQueue.Count - 1].x + "," + players[currentPlayerIndex].positionQueue[players[currentPlayerIndex].positionQueue.Count - 1].y + ")");
            }
            players[currentPlayerIndex].gridPosition = destTile.gridPosition;

        }
        else
        {
            destTile.visual.transform.GetComponent<Renderer>().materials[0].color = Color.cyan;
        }
    }
}