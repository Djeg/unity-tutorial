using System;
using UnityEngine;
using VickingWrath.Persona;

namespace VickingWrath.Persona.Animations
{
    /**
     * <summary>
     * This behaviours could be attached to any animation that could lead
     * to a hurt animation.
     * </summary>
     */
    public class Persona_GetHurt_Behaviours : StateMachineBehaviour
    {
        # region Properties

        /**
         * <summary>
         * The persona health
         * </summary>
         */
        private PersonaHealth _health = null;

        /**
         * <summary>
         * The hurt trigger animation name
         * </summary>
         */
        [SerializeField]
        private string _hurtTriggerName = "Hurt";

        /**
         * <summary>
         * The die trigger animation name
         * </summary>
         */
        [SerializeField]
        private string _dieTriggerName = "Die";

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
        # endregion

        # region PrivateMethods

        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _animator = animator;
            _health = animator.gameObject.GetComponent<PersonaHealth>();

            if (null == _health)
              throw new Exception("You must attach a Persona health if you want to use the get hurt animation.");

            _health.OnAfterTakingDamage.AddListener(TriggerHurtAnimation);
        }

        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (null == _health)
              return;

            _health.OnAfterTakingDamage.RemoveListener(TriggerHurtAnimation);
        }

        /**
         * <summary>
         * Function triggered when the persona is tacking damages
         * </summary>
         */
        public void TriggerHurtAnimation(PersonaHealth health, Hit hit)
        {
            if (null == _animator)
              return;

            if (health.IsDead)
                _animator.SetTrigger(_dieTriggerName);
            else
                _animator.SetTrigger(_hurtTriggerName);
        }

        # endregion
    }
}
