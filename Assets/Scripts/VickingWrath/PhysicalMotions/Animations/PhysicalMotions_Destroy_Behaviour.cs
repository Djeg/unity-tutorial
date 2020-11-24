using UnityEngine;

namespace VickingWrath.PhysicalMotions.Animations
{
    /**
     * <summary>
     * Allows a game object to be destroy as the animation exit.
     * </summary>
     */
    public class PhysicalMotions_Destroy_Behaviour : StateMachineBehaviour
    {
        # region Properties

        # endregion

        # region PropertyAccessors
        # endregion

        # region PublicMethods

        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            GameObject.Destroy(animator.gameObject);
        }

        # endregion

        # region PrivateMethods
        # endregion
    }
}
