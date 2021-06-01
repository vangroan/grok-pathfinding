using System;
using UnityEngine;

namespace Grok.Pathfinding
{
    internal struct SearchNode : IComparable<SearchNode>
    {
         /// <summary>
        ///     Actual cost from starting coordinate to this node.
        /// </summary>
        public int G;

        /// <summary>
        ///     Heuristic guestimate cost to end goal node.
        /// </summary>
        public int H;

        /// <summary>
        ///     Index to the parent node in the node buffer.
        /// </summary>
        /// <remarks>
        /// <p>
        ///     Nullable because starting node has no parent.
        ///     Terminates the path walk.
        /// </p>
        /// <p>
        ///     Calculated in the usual way of converting n-dimensional
        ///     array coordinates to a 1D index.
        ///     Example: <c>x + y * width + z * width * height</c>
        /// </p>
        /// </remarks>
        public Nullable<Vector2Int> ParentIndex;

        /// <summary>
        ///     Voxel coordinate in map.
        /// </summary>
        public Vector2Int Coord;

        public int Total => G + H;
        
        public int CompareTo(SearchNode other)
        {
            return this.Total.CompareTo(other.Total);
        }
    }
}
