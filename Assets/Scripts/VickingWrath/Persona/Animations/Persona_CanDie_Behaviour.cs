using System;
using UnityEngine;
using VickingWrath.Persona;

namespace VickingWrath.Persona.Animations
{
    /**
     * <summary>
     * Allows an animation to die but does not trigger any hurt stage.
     * </summary>
     */
    public class Persona_CanDie_Behaviour : StateMachineBehaviour
    {
        # region Properties

        /**
         * <summary>
         * The die animation trigger name
         * </summary>
         */
        [SerializeField]
        private string _dieTriggerName = "Die";

        /**
         * <summary>
         * The persona health
         * </summary>
         */
        private PersonaHealth _health = null;

        /**
         * <summary>
         * A reference to the animator
         * </summary>
         */
        private Animator _animator = null;

        # endregion

        # region PropertyAccessors
        # endregion

        # region PublicMethods

        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _animator = animator;
            _health = animator.gameObject.GetComponent<PersonaHealth>();

            if (null == _health)
                throw new Exception("You must attach a PersonaHealth in order to make a die animation");

            _health.OnDying.AddListener(WhenDying);
        }

        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _health.OnDying.RemoveListener(WhenDying);
        }

        /**
         * <summary>
         * Trigger when the persona is dying
         * </summary>
         */
        public void WhenDying(PersonaHealth health)
        {
            _animator.SetTrigger(_dieTriggerName);
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

        # region PrivateMethods
        # endregion
    }
}
