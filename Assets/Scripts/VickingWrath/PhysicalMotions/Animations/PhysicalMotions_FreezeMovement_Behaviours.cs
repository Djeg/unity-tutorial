using System;
using UnityEngine;
using VickingWrath.PhysicalMotions.Behaviours;

namespace VickingWrath.PhysicalMotions.Animations
{
    /**
     * <summary>
     * Freeze the moovement during the animation
     * </summary>
     */
    public class PhysicalMotions_FreezeMovement_Behaviours : StateMachineBehaviour
    {
        # region Properties

        /**
         * <summary>
         * The movable motion
         * </summary>
         */
        private IMovable _moveMotion = null;

        /**
         * <summary>
         * The jumpable motion
         * </summary>
         */
        private IJumpable _jumpMotion = null;

        # endregion

        # region PropertyAccessors
        # endregion

        # region PublicMethods

        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _moveMotion = animator.gameObject.GetComponent<IMovable>();
            _jumpMotion = animator.gameObject.GetComponent<IJumpable>();

            if (null == _moveMotion)
                throw new Exception("You must attach a IMovable motion to the animator component");

            _moveMotion.IsMoveFrozen = true;

            if (null == _jumpMotion)
                return;

            _jumpMotion.IsJumpFrozen = true;
        }

        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _moveMotion.IsMoveFrozen = false;

            if (null == _jumpMotion)
                return;

            _jumpMotion.IsJumpFrozen = false;
        }

        # endregion

        # region PrivateMethods
        # endregion
    }
}
