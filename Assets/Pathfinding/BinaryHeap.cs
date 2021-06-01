using System;
using System.Collections.Generic;

namespace Grok.Pathfinding
{
    /// <summary>
    ///     Minimum binary heap.
    /// </summary>
    /// <remarks>
    ///     Can be used as a priority queue.
    /// </remarks>
    public class BinaryHeap<T> where T : IComparable<T>
    {
        public int Count => m_data.Count;
        public bool IsEmpty => m_data.Count == 0;
        private readonly List<T> m_data = new List<T>();

        public void Insert(T item)
        {
            // Index of newly inserted node.
            int i = m_data.Count;

            // Insert new node in the bottom of the tree
            m_data.Add(item);

            // Minimum heap swaps when parent is greater than child.
            while (m_data[(i - 1) / 2].CompareTo(m_data[i]) > 0)
            {
                // With interger division -1 / 2 is 0
                // So works for root (index 0) node as well.
                int parentIdx = (i - 1) / 2;

                // Swap
                T tmp = m_data[parentIdx];
                m_data[parentIdx] = m_data[i];
                m_data[i] = tmp;

                // Inserted node has now moved to its parent.
                i = parentIdx;
            }
        }

        /// <summary>
        ///     Remove and return the minimum item from the heap.
        /// </summary>
        public T Extract()
        {
            T item = m_data.Count > 0 ? m_data[0] : default(T);

            // Replace the root with the node at the bottom.
            int lastIdx = m_data.Count - 1;
            m_data[0] = m_data[lastIdx];
            m_data.RemoveAt(lastIdx);

            int i = 0;
            while (true)
            {
                int lhs = (i * 2) + 1;
                int rhs = (i * 2) + 2;

                if (lhs >= m_data.Count)
                {
                    // No children left
                    break;
                }
                else if (lhs == m_data.Count - 1)
                {
                    // Left child only
                    if (m_data[i].CompareTo(m_data[lhs]) > 0)
                    {
                        // Left child is smaller than current node.
                        T tmp = m_data[i];
                        m_data[i] = m_data[lhs];
                        m_data[lhs] = tmp;
                        i = lhs;
                    }
                    else
                    {
                        break;
                    }
                }
                else if (rhs < m_data.Count)
                {
                    // Both left and right children present
                    int smallestChild = m_data[rhs].CompareTo(m_data[lhs]) > 0 ? lhs : rhs;
                    if (m_data[i].CompareTo(m_data[smallestChild]) > 0)
                    {
                        T tmp = m_data[i];
                        m_data[i] = m_data[smallestChild];
                        m_data[smallestChild] = tmp;
                        i = smallestChild;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            return item;
        }

        /// <summary>
        ///     Return the minimum item from the heap.
        /// </summary>
        public T Peek()
        {
            // Root is always the first element as long as collection invariants hold true.
            return m_data.Count > 0 ? m_data[0] : default(T);
        }

        public void Clear()
        {
            m_data.Clear();
        }
    }
}
