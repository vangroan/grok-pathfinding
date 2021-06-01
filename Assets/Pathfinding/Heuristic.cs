using UnityEngine;

namespace Grok.Pathfinding
{
    public static class Heuristic
    {
        public static int OctileDistance(Vector2Int a, Vector2Int b)
        {
            int dx = Mathf.Abs(a.x - b.x);
            int dy = Mathf.Abs(a.y - b.y);

            int dmin = Mathf.Min(dx, dy);
            int dmax = Mathf.Max(dx, dy);

            // How much does moving diagonally cost over moving orthagonally?
            //
            // The cost that is lost by subtracting the orthagonal cost, is
            // made up by the orthagonal cost being multiplied by dmax.
            // ie. The largest vector component covers the diagonal movement
            // as well.
            int excess2D = Cost.Diagonal2DCost - Cost.OrthagonalCost;

            // Which profit is multiplied with which component depends
            // on the precedent of the pather, which depends on how
            // expensive the movement is.
            // 
            // Moving diagonally along three axes is the most expensive,
            // so the pather will try to do it least.
            return dmin * excess2D + dmax * Cost.OrthagonalCost;
        }
    }
}
