using System;

namespace VickingWrath.DiceSystem
{
    /**
     * <summary>
     * Represent the result of a dice roll
     * </summary>
     */
    public sealed class DiceResult
    {
        /**
         * <summary>
         * A random static instance
         * </summary>
         */
        private static Random randomizer = new Random();

        /**
         * <summary>
         * The value of the dice roll
         * </summary>
         */
        public int Value { get; private set; }

        /**
         * <summary>
         * The original value of the dice roll (without the modifier and
         * critical modifier).
         * </summary>
         */
        public int OriginalValue { get; private set; }

        /**
         * <summary>
         * A boolean that tell us if this roll is a critical one or not.
         * </summary>
         */
        public bool IsCritical { get; private set; }

        /**
         * <summary>
         * The dice roll used in order to create this dice result,
         * </summary>
         */
        public DiceRoll Roll { get; private set; }

        /**
         * <summary>
         * Create a dice result
         * </summary>
         */
        public DiceResult(DiceRoll diceRoll)
        {
            Roll = diceRoll;

            OriginalValue = randomizer.Next(1, ((int)diceRoll.face) + 1);
            IsCritical = OriginalValue == (int)diceRoll.face;
            Value = (OriginalValue + diceRoll.modifier) * diceRoll.criticalModifier;
        }
    }
}
