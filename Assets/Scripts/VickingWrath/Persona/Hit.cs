using System.Collections;
using VickingWrath.DiceSystem;
using VickingWrath.PhysicalMotions;
using UnityEngine;

namespace VickingWrath.Persona
{
    /**
     * <summary>
     * Represent the data used in order to Take or Receive a hit.
     * </summary>
     */
    public sealed class Hit
    {
        # region Properties
        # endregion

        # region PropertyAccessors

        /**
         * <summary>
         * Represent the dice result containing all numeric information
         * about the hit.
         * </summary>
         */
        public DiceResult Damage { get; private set; }

        /**
         * <summary>
         * Represent the force apply to this hit. Used to create physical
         * effect.
         * </summary>
         */
        public Vector2 Strength { get; private set; }

        # endregion

        # region PublicMethods

        /**
         * <summary>
         * A standard hit constructor
         * </summary>
         */
        public Hit(DiceResult damage, Vector2 strength, Direction direction)
        {
            this.Damage = damage;
            this.Strength = direction == Direction.Left
                ? new Vector2(-strength.x, strength.y)
                : strength
            ;
        }

        # endregion

        # region PrivateMethods
        # endregion
    }
}
