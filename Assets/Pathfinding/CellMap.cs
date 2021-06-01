using UnityEngine;

namespace Grok.Pathfinding
{
    [ExecuteInEditMode]
    public class CellMap : MonoBehaviour
    {
        public Vector2Int Dimensions;

        private Array2D<CellData> _cells;
        public Array2D<CellData> Cells;

        public int Width => _cells.Width;
        public int Height => _cells.Height;

        #region Unity Lifecycle
        protected void Awake()
        {
            _cells = new Array2D<CellData>(Mathf.Max(1, Dimensions.x), Mathf.Max(1, Dimensions.y));
        }

        protected void Update()
        {

        }
        #endregion
    }
}
