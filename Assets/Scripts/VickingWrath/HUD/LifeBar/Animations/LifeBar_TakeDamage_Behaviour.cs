using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VickingWrath.HUD.LifeBar;
using VickingWrath.Persona;

namespace VickingWrath.HUD.LifeBar.Animations
{
    /**
     * <summary>
     * Control the lifebar animation
     * </summary>
     */
    public class LifeBar_TakeDamage_Behaviour : StateMachineBehaviour
    {
        # region Properties

        /**
         * <summary>
         * The take damage trigger name
         * </summary>
         */
        [SerializeField]
        private string _takeDamageTriggerName = "TakeDamage";

        /**
         * <summary>
         * Display the animation only for critical hit
         * </summary>
         */
        [SerializeField]
        private bool _onlyOnCriticalHit = false;

        /**
         * <summary>
         * The life bar controller
         * </summary>
         */
        private LifeBarController _controller = null;

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
            _controller = animator.GetComponent<LifeBarController>();

            if (null == _controller)
                throw new Exception("The life bar must have a life bar controller in order to be animated.");

            _controller.Health.OnAfterTakingDamage.AddListener(AnimateTakeDamage);
        }

        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _controller.Health.OnAfterTakingDamage.RemoveListener(AnimateTakeDamage);
        }

        /**
         * <summary>
         * Animate take damage
         * </summary>
         */
        private void AnimateTakeDamage(PersonaHealth health, Hit hit)
        {
            if (_onlyOnCriticalHit && !hit.Damage.IsCritical)
                return;

            _animator.SetTrigger(_takeDamageTriggerName);
        }

        # endregion

        # region PrivateMethods
        # endregion
    }
}
