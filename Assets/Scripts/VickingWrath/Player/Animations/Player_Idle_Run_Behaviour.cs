using System;
using UnityEngine;
using VickingWrath.PhysicalMotions;
using VickingWrath.Player;

namespace VickingWrath.Player.Animations
{
    /**
     * <summary>
     * This contains the behaviour of the idle and run animation
     * of the player.
     * </summary>
     */
    public class Player_Idle_Run_Behaviour : StateMachineBehaviour
    {
        # region Properties

        /**
         * <summary>
         * Store the player motion
         * </summary>
         */
        private TerestrialMotion _motion = null;

        /**
         * <summary>
         * The player controller
         * </summary>
         */
        private PlayerController _controller = null;

        /**
         * <summary>
         * Store the running boolean of the animator
         * </summary>
         */
        [SerializeField]
        private string _runningBoolName = "Running";

        /**
         * <summary>
         * Store the attack animation trigger
         * </summary>
         */
        [SerializeField]
        private string _attackTriggerName = "Attack";

        /**
         * <summary>
         * Store the jump trigger name
         * </summary>
         */
        [SerializeField]
        private string _jumpTriggerName = "Jump";

        # endregion

        # region PropertyAccessors
        # endregion

        # region PublicMethods
        # endregion

        # region PrivateMethods

        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _motion = animator.gameObject.GetComponent<TerestrialMotion>();
            _controller = animator.gameObject.GetComponent<PlayerController>();

            if (null == _motion || null == _controller)
                throw new Exception("The player must contains a TerestrialMotion and a PlayerController in order to be animated.");
        }

        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetBool(_runningBoolName, _motion.IsMooving);

            if (_motion.IsJumping)
                animator.SetTrigger(_jumpTriggerName);

            if (_controller.IsAttacking)
                animator.SetTrigger(_attackTriggerName);
        }

        # endregion
    }
}
