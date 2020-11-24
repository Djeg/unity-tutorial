using System.Collections;
using UnityEngine;
using VickingWrath.PhysicalMotions;

namespace VickingWrath.Player
{
    /**
     * <summary>
     * Control the player
     * </summary>
     */
    public class PlayerController : MonoBehaviour
    {
        # region Properties

        /**
         * <summary>
         * The player motion
         * </summary>
         */
        private TerestrialMotion _motion = null;

        /**
         * <summary>
         * The player movement based on the horizontal axis
         * </summary>
         */
        private float _movement = 0f;

        /**
         * <summary>
         * The player jump boolean
         * </summary>
         */
        private bool _jump = false;

        /**
         * <summary>
         * The player attack boolean
         * </summary>
         */
        private bool _attack = false;

        # endregion

        # region PropertyAccessors

        /**
         * <summary>
         * Test of the player has trigger the jump command
         * </summary>
         */
        public bool HasJump { get => _jump; }

        /**
         * <summary>
         * Return true if the controller is pressing the attack button
         * </summary>
         */
        public bool IsAttacking { get => _attack; }

        # endregion

        # region PublicMethods
        # endregion

        # region PrivateMethods

        /**
         * <summary>
         * Trigger when the object is append in the world
         * </summary>
         */
        private void Awake()
        {
            _motion = GetComponent<TerestrialMotion>();
        }

        /**
         * <summary>
         * Executed on each frames
         * </summary>
         */
        private void Update()
        {
            _movement = Input.GetAxis("Horizontal");
            _jump = Input.GetButton("Jump");
            _attack = Input.GetButton("Fire1");
        }

        /**
         * <summary>
         * Trigger on each frames but based on physical alogorithm
         * </summary>
         */
        private void FixedUpdate()
        {
            _motion.Move(_movement);

            if (_jump)
                _motion.Jump();
        }

        # endregion
    }
}
