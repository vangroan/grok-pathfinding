using UnityEngine;

namespace Grok.Pathfinding
{
    public class PathNode
    {
        /// <summary>
        ///     Voxel coordinate where this node is located.
        /// </summary>
        public Vector2Int Coord;

        /// <summary>
        ///     Total cost of moving to this node.
        /// </summary>
        public int Cost;
    }
}
