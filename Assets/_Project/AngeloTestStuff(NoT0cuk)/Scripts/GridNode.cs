using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridNode
{
    public int gCost, hCost, fCost;
    public bool open, closed, isWalkable;
    public Vector3Int gridPosition;
    public Vector3 worldPosition;
    public GridNode parentNode;

    public GridNode(Vector3Int gridPos, Vector3 worldPos, bool canWalk)
    {
      gridPosition = gridPos;
      worldPosition = worldPos;
      isWalkable = canWalk;
    }
}
