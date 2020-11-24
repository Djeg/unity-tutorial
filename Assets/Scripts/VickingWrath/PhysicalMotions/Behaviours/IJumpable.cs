using UnityEngine;
using VickingWrath.PhysicalMotions.Events;

namespace VickingWrath.PhysicalMotions.Behaviours
{
    /**
     * <summary>
     * Define the ability of any object to jump.
     * </summary>
     */
    public interface IJumpable
    {
        # region Properties

        /**
         * <summary>
         * Define here the jump force.
         * </summary>
         */
        float JumpForce { get; set; }

        /**
         * <summary>
         * Test if the current object if jumping.
         * </summary>
         */
        bool IsJumping { get; }

        /**
         * <summary>
         * Test if the ability to jump is currently frozen
         * </summary>
         */
        bool IsJumpFrozen { get; set; }

        /**
         * <summary>
         * Define the number of jump that the object can do.
         * </summary>
         */
        int MaxJump { get; set; }

        /**
         * <summary>
         * Define here a game object wich will be used as a ground detector.
         * </summary>
         */
        GameObject GroundDetector { get; set; }

        /**
         * <summary>
         * Allow to hook any jump event
         * </summary>
         */
        JumpEvent OnJump { get; }

        # endregion

        # region Methods

        /**
         * <summary>
         * Make the character jump if it hasn't reached the MaxJump
         * </summary>
         */
        void Jump();

        # endregion
    }
}
