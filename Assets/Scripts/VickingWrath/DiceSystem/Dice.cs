using System.Collections;
using UnityEngine;

namespace VickingWrath.DiceSystem
{
    /**
     * <summary>
     * Allows to throw dice and get back a result
     * </summary>
     */
    public static class Dice
    {
        /**
         * <summary>
         * A simple shortcut to GetResult. It creates the
         * dice roll instance itself and return a DiceResult
         * </summary>
         */
        public static DiceResult Roll(
            DiceFace face = DiceFace.Six,
            int modifier = 0,
            int criticalModifier = 1
        ) {
            DiceRoll roll = new DiceRoll(face, modifier, criticalModifier);

            return GetResult(roll);
        }

        /**
         * <summary>
         * Roll a dice and return a DiceResult
         * </summary>
         */
        public static DiceResult GetResult(DiceRoll diceRoll)
        {
            return new DiceResult(diceRoll);
        }
    }
}
