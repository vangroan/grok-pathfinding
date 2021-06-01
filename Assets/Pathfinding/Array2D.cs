using System;
using System.Collections;
using System.Collections.Generic;

namespace Grok.Pathfinding
{
    public class Array2D<T> : IEnumerable<T>
    {
        private T[,] _data;

        public Array2D(int width, int height) : this(new Size(width, height))
        {
        }

        public Array2D(Size size)
        {
            _data = new T[size.Height, size.Width];
        }

        public T this[int x, int y]
        {
            get => _data[y, x];
            set => _data[y, x] = value;
        }

        public T this[UnityEngine.Vector2Int coord]
        {
            get => _data[coord.y, coord.x];
            set => _data[coord.y, coord.x] = value;
        }

        public int Width => _data.GetLength(1);
        public int Height => _data.GetLength(0);

        public bool InBounds(UnityEngine.Vector2Int coord)
        {
            return InBounds(coord.x, coord.y);
        }

        public bool InBounds(int x, int y)
        {
            return x >= 0 && x < _data.GetLength(1)
                && y >= 0 && y < _data.GetLength(0);
        }

        public IEnumerable<Pair> GetNeighbours(UnityEngine.Vector2Int coord)
        {
            foreach (var offset in Direction.DiagonalNeighbours)
            {
                UnityEngine.Vector2Int neighCoord = coord + offset;
                if (InBounds(neighCoord))
                {
                    yield return new Pair
                    {
                        Coord = neighCoord,
                        Item = _data[neighCoord.y, neighCoord.x]
                    };
                }
            }
        }

        public IEnumerable<Pair> Pairs()
        {
            for (int y = 0; y < _data.GetLength(0); y++)
            {
                for (int x = 0; x < _data.GetLength(1); x++)
                {
                    yield return new Pair
                    {
                        Coord = new UnityEngine.Vector2Int(x, y),
                        Item = _data[y, x]
                    };
                }
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int y = 0; y < _data.GetLength(0); y++)
            {
                for (int x = 0; x < _data.GetLength(1); x++)
                {
                    yield return _data[y, x];
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        public struct Size
        {
            public int Width;
            public int Height;

            public Size(int width, int height)
            {
                Width = width;
                Height = height;
            }
        }

        public struct Pair
        {
            public UnityEngine.Vector2Int Coord;
            public T Item;
        }
    }
}

