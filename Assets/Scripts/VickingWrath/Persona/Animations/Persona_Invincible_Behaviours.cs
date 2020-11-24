using System;
using UnityEngine;
using VickingWrath.Persona;

namespace VickingWrath.Persona.Animations
{
    /**
     * <summary>
     * This behaviour could be attached in order to make the persona invincible
     * during the animation.
     * </summary>
     */
    public class Persona_Invincible_Behaviours : StateMachineBehaviour
    {
        # region Properties

        /**
         * <summary>
         * The persona health
         * </summary>
         */
        private PersonaHealth _health = null;

        # endregion

        # region PropertyAccessors
        # endregion

        # region PublicMethods
        # endregion

        # region PrivateMethods

        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _health = animator.gameObject.GetComponent<PersonaHealth>();

            if (null == _health)
                throw new Exception("You must attach a persona health to the animator object in order to play Invincible animation");

            _health.Invincible = true;
        }

        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (null == _health)
                return;

            _health.Invincible = false;
        }

        # endregion
    }
}
