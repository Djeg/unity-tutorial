using System.Collections;
using UnityEngine;

namespace VickingWrath.PhysicalMotions
{
    /**
     * <summary>
     * Represent all the available direction that an
     * object can take. Since it's a 2D game with
     * only two direction we only have Left, Right and None.
     * </summary>
     */
    public enum Direction
    {
        None,
        Left,
        Right
    }

    /**
     * <summary>
     *  A simple static class wich converts a Direction into other types.
     * </summary>
     */
    public static class DirectionCaster
    {
        /**
         * <summary>
         *  Convert a direction into a float value
         * </summary>
         */
        public static float ToFloat(Direction direction)
        {
            switch(direction)
            {
                case Direction.Right:
                    return 1f;
                case Direction.Left:
                    return -1f;
                default:
                    return 0f;
            }
        }
    }
}
