using System;
using UnityEngine;
using VickingWrath.Enemies.Guardian;

namespace VickingWrath.Enemies.Guardian.Animations
{
    /**
     * <summary>
     * Control the run and walk behaviour of a guardian
     * </summary>
     */
    public class Guardian_Walk_Run_Behaviour : StateMachineBehaviour
    {
        # region Properties

        /**
         * <summary>
         * The guardian running boolean parameter name
         * </summary>
         */
        [SerializeField]
        private string _runningBoolName = "Running";

        /**
         * <summary>
         * The guardian attack trigger parameter name
         * </summary>
         */
        [SerializeField]
        private string _attackTriggerName = "Attack";

        /**
         * <summary>
         * The guardian controller
         * </summary>
         */
        private GuardianController _guardian = null;

        # endregion

        # region PropertyAccessors
        # endregion

        # region PublicMethods

        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _guardian = animator.gameObject.GetComponent<GuardianController>();

            if (null == _guardian)
              throw new Exception("You must specify a guardian controller in order to make hime walk and run");
        }

        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetBool(_runningBoolName, _guardian.HasEnemiesInVision);

            if (_guardian.HasEnemiesInAttackRange)
                animator.SetTrigger(_attackTriggerName);
        }

        # endregion

        # region PrivateMethods
        # endregion
    }
}
