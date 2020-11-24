using System;
using UnityEngine;
using VickingWrath.PhysicalMotions;

namespace VickingWrath.Player.Animations
{
    /**
     * <summary>
     * Handle the attack animation of a player
     * </summary>
     */
    public class Player_Attack_Behaviour : StateMachineBehaviour
    {
        # region Properties

        /**
         * <summary>
         * The player motion
         * </summary>
         */
        private TerestrialMotion _motion = null;

        # endregion

        # region PropertyAccessors
        # endregion

        # region PublicMethods
        # endregion

        # region PrivateMethods

        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _motion = animator.gameObject.GetComponent<TerestrialMotion>();

            if (null == _motion)
                throw new Exception("The player must have a terestrial motion in order to be animated");

            _motion.IsMoveFrozen = true;
            _motion.IsJumpFrozen = true;
        }

        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _motion.IsMoveFrozen = false;
            _motion.IsJumpFrozen = false;
        }

        // OnStateMove is called right after Animator.OnAnimatorMove()
        //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    // Implement code that processes and affects root motion
        //}

        // OnStateIK is called right after Animator.OnAnimatorIK()
        //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    // Implement code that sets up animation IK (inverse kinematics)
        //}

        # endregion
    }
}
