using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grok.Pathfinding
{
    public class Direction
    {
        public static Vector2Int[] DiagonalNeighbours = new Vector2Int[] {
            new Vector2Int(0, 1),   // North
            new Vector2Int(0, -1),  // South
            new Vector2Int(1, 0),   // East
            new Vector2Int(-1, 0),  // West
            new Vector2Int(1, 1),   // North-East
            new Vector2Int(-1, -1), // South-West
            new Vector2Int(-1, 1),  // North-West
            new Vector2Int(1, -1),  // South-East
        };
    }
}
