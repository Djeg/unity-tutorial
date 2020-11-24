using UnityEngine;
using VickingWrath.PhysicalMotions.Events;
using VickingWrath.PhysicalMotions;

namespace VickingWrath.PhysicalMotions.Behaviours
{
    /**
     * <summary>
     * Define the ability of any object to Move and Stop.
     * </summary>
     */
    public interface IMovable
    {
        # region Properties

        /**
         * <summary>
         * The direction where the object is goind
         * </summary>
         */
        Direction Direction { get; }

        /**
         * <summary>
         * Test if the object is currently mooving
         * </summary>
         */
        bool IsMooving { get; }

        /**
         * <summary>
         * Test if the movement is currently frozen
         * </summary>
         */
        bool IsMoveFrozen { get; set; }

        /**
         * <summary>
         * The move speed
         * </summary>
         */
        float Speed { get; set; }

        /**
         * <summary>
         * The smooth time
         * </summary>
         */
        float SmoothTime { get; set; }

        /**
         * <summary>
         * The current horizontal movement.
         * </summary>
         */
        float HorizontalMovement { get; }

        /**
         * <summary>
         * The velocity of the current deplacement.
         * </summary>
         */
        Vector2 Velocity { get; }

        /**
         * <summary>
         * Event triggered when the motion is changing direction.
         * </summary>
         */
        ChangeDirectionEvent OnDirectionChange { get; }

        # endregion

        # region Methods

        /**
         * <summary>
         * Move the current object according to the parameters above.
         * </summary>
         */
        void Move(float horizontalMovement);

        /**
         * <summary>
         * Stop the current from mooving
         * </summary>
         */
        void Stop();

        /**
         * <summary>
         * Switch the current direction
         * </summary>
         */
        void SwitchDirection();

        /**
         * <summary>
         * Apply a force to the given motion
         * </summary>
         */
        void ApplyForce(Vector2 force);

        # endregion
    }
}
