
namespace Grok.Pathfinding
{
    public static class Cost
    {
        /// <summary>
        ///     Cost of moving along the orthagonal directions.
        /// </summary>
        public const int OrthagonalCost = 100;

        /// <summary>
        ///     Cost of moving diagonally on a 2D plane.
        /// </summary>
        /// <remarks>
        ///     <c>r = √(x² + y²)</c>
        /// </remarks>
        public const int Diagonal2DCost = 141;

        /// <summary>
        ///     Cost of moving diagonally in 3D, from one level to another (ramps).
        /// </summary>
        /// <remarks>
        ///     <c>r = √(x² + y² + z²)</c>
        /// </remarks>
        public const int Diagonal3DCost = 173;

        /// <summary>
        ///     Lookup table for direction costs.
        /// </summary>
        public static int[] Lookup = new int[] {
            OrthagonalCost,
            Diagonal2DCost,
            Diagonal3DCost,
        };
    }
}
