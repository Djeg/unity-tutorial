using System;
using UnityEngine;
using VickingWrath.PhysicalMotions.Behaviours;

namespace VickingWrath.Player.Animations
{
    /**
     * <summary>
     * Handle the jump animation of a player
     * </summary>
     */
    public class Player_Jump_Behaviour : StateMachineBehaviour
    {
        # region Properties

        /**
         * <summary>
         * The motion of the player
         * </summary>
         */
        private IJumpable _motion = null;

        /**
         * <summary>
         * A reference to the animator
         * </summary>
         */
        private Animator _animator = null;

        /**
         * <summary>
         * The jump trigger name 
         * </summary>
         */
        [SerializeField]
        private string _jumpTriggerName = "Jump";

        /**
         * <summary>
         * The stop jump trigger name 
         * </summary>
         */
        [SerializeField]
        private string _stopJumpTriggerName = "StopJump";

        # endregion

        # region PropertyAccessors
        # endregion

        # region PublicMethods
        # endregion

        # region PrivateMethods

        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _animator = animator;
            _motion = animator.gameObject.GetComponent<IJumpable>();

            _motion.OnJump.AddListener(OnJump);

            if (null == _motion)
                throw new Exception("The player must have a terestrial motion attached in order to be animated.");
        }

        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (_motion.IsJumping)
                return;

            animator.SetTrigger(_stopJumpTriggerName);
        }

        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _motion.OnJump.RemoveListener(OnJump);
        }

        /**
         * <summary>
         * Trigger when the object is jumping again.
         * </summary>
         */
        public void OnJump(IJumpable motion)
        {
            _animator.SetTrigger(_jumpTriggerName);
        }

        # endregion
    }
}
