using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

namespace Grok.Pathfinding
{
    [DisallowMultipleComponent]
    public class AStar : MonoBehaviour
    {
        private readonly HashSet<Vector2Int> _seen = new HashSet<Vector2Int>();
        private readonly BinaryHeap<SearchNode> _frontier = new BinaryHeap<SearchNode>();
        private Array2D<SearchNode> _nodes;
        private CellMap _cellmap;

        #region Unity Lifecycle
        protected void Start()
        {
            _cellmap = GetComponent<CellMap>();
        }
        #endregion

        public PathNode[] FindPath(Vector2Int start, Vector2Int end, int maxDistance)
        {
            if (_cellmap == null) throw new NullReferenceException("CellMap component is null");
            var cells = _cellmap.Cells;

            if (_nodes == null
                || (_nodes.Width != _cellmap.Width
                    && _nodes.Height != _cellmap.Height))
            {
                // Reallocate node buffer.
                _nodes = new Array2D<SearchNode>(_cellmap.Width, _cellmap.Height);
            }

            _seen.Clear();
            _frontier.Clear();

            var startNode = new SearchNode
            {
                Coord = start,
                G = 0,
                H = Heuristic.OctileDistance(start, end),
                ParentIndex = null,
            };

            _frontier.Insert(startNode);
            _seen.Add(start);
            _nodes[start] = startNode;

            Profiler.BeginSample("A* Search");

            SearchNode node;
            while (!_frontier.IsEmpty)
            {
                node = _frontier.Extract();

                if (node.Coord == end)
                {
                    Profiler.EndSample();
                    return BuildPath(node);
                }

                foreach (var pair in cells.GetNeighbours(node.Coord))
                {
                    // Neighbours must not be added
                    // to the search frontier or node buffer
                    // if they have been seen.
                    //
                    // They would overwrite existing neighbour
                    // costs from other parent nodes.
                    if (_seen.Contains(pair.Coord))
                    {
                        continue;
                    }
                    _seen.Add(pair.Coord);

                    // Pick a movement cost based on direction
                    // from parent node.
                    // Direction can be determined by how different
                    // the delta vector is.
                    var delta = node.Coord - pair.Coord;
                    int dx = Math.Abs(delta.x);
                    int dy = Math.Abs(delta.y);

                    // Neighbours are either -1, 0, 1 on the 2 axes.
                    // ie 0 <= index < 2
                    var cost = Cost.Lookup[dx + dy];

                    var neighNode = new SearchNode
                    {
                        Coord = pair.Coord,
                        G = node.G + cost,
                        H = Heuristic.OctileDistance(pair.Coord, end),
                        ParentIndex = node.Coord,
                    };

                    _frontier.Insert(neighNode);
                    _nodes[pair.Coord] = neighNode;
                }
            }

            Profiler.EndSample();

            return Array.Empty<PathNode>();
        }

        /// <summary>
        ///     Walk a path from the given goal to the starting node
        ///     ie. terminated by the node with no parent index.
        /// </summary>
        PathNode[] BuildPath(SearchNode endGoal)
        {
            // Reuse hash set to save on re-allocating.
            _seen.Clear();
            var path = new List<PathNode>();

            Profiler.BeginSample("Build Path");

            SearchNode? head = endGoal;
            while (head.HasValue)
            {
                SearchNode node = head.Value;

                // Malformed path node relations cause infinite loop.
                if (_seen.Contains(node.Coord))
                {
                    Debug.LogWarningFormat("Cycle detected in built path. Node {0}", node.Coord);
                    break;
                }
                _seen.Add(node.Coord);

                path.Add(new PathNode
                {
                    Coord = node.Coord,
                    Cost = node.G,
                });
                head = node.ParentIndex.HasValue ? (SearchNode?)_nodes[node.ParentIndex.Value] : null;
            }

            path.Reverse();
            Profiler.EndSample();

            return path.ToArray();
        }
    }
}
