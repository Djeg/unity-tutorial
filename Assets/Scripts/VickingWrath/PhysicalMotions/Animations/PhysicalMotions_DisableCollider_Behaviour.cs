using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VickingWrath.PhysicalMotions.Behaviours;

namespace VickingWrath.PhysicalMotions.Animations
{
    /**
     * <summary>
     * This disable the collider attach whith the animator and set the Rigidbody
     * to kinetic.
     * </summary>
     */
    public class PhysicalMotions_DisableCollider_Behaviour : StateMachineBehaviour
    {
        # region Properties

        /**
         * <summary>
         * The collider
         * </summary>
         */
        private Collider2D _collider = null;

        /**
         * <summary>
         * The Rigidbody2D
         * </summary>
         */
        private Rigidbody2D _body = null;

        # endregion

        # region PropertyAccessors
        # endregion

        # region PublicMethods

        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _collider = animator.gameObject.GetComponent<Collider2D>();
            _body = animator.gameObject.GetComponent<Rigidbody2D>();

            if (null == _collider || null == _body)
                throw new Exception("You must attach a Collider2D, Rigidbody2D in order to disable collisions");

            _collider.enabled = false;
            _body.isKinematic = true;
        }

        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _collider.enabled = true;
            _body.isKinematic = false;
        }

        # endregion

        # region PrivateMethods
        # endregion
    }
}
