using System;

namespace VickingWrath.DiceSystem
{
    /**
     * <summary>
     * Represent the data used to roll a dice
     * </summary>
     */
    public sealed class DiceRoll
    {
        /**
         * <summary>
         * The number of faces this dice roll have
         * </summary>
         */
        public DiceFace face;

        /**
         * <summary>
         * The base modifier
         * </summary>
         */
        public int modifier;

        /**
         * <summary>
         * The critical modifier
         * </summary>
         */
        public int criticalModifier;

        /**
         * <summary>
         * Create a dice roll by sending some basic data on it.
         * </summary>
         */
        public DiceRoll(DiceFace _face = DiceFace.Six, int _modifier = 0, int _criticalModifier = 1)
        {
            face = _face;
            modifier = _modifier;
            criticalModifier = _criticalModifier;
        }
    }
}
