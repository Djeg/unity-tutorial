using System;
using System.Collections;
using UnityEngine;
using VickingWrath.PhysicalMotions.Behaviours;

namespace VickingWrath.Camera
{
    /**
     * <summary>
     *  Control the camera moovement based on the player.
     * </summary>
     */
    public class CameraController : MonoBehaviour
    {
        # region Properties

        /**
         * <summary>
         * The player to follow
         * </summary>
         */
        [SerializeField]
        [Header("Paramaters")]
        private GameObject _subject = null;

        /**
         * <summary>
         * The smooth time of teh Vector2.SmoothDamp
         * </summary>
         */
        [SerializeField]
        private float _smoothTime = 0.25f;

        /**
         * <summary>
         * The offset of the camera if you don't want the camera to be
         * exactly centered.
         * </summary>
         */
        [SerializeField]
        private Vector2 _offset = Vector2.zero;

        /**
         * <summary>
         * The velocity used for the smooth damp moovement.
         * </summary>
         */
        private Vector2 _velocity = Vector2.zero;

        # endregion

        # region PropertyAccessors
        # endregion

        # region PublicMethods

        /**
         * <summary>
         * Flip the horizontal offset
         * </summary>
         */
        public void FlipHorizontalOffset(IMovable motion)
        {
            _offset = new Vector2(
                _offset.x > 0 ? -_offset.x : Math.Abs(_offset.x),
                _offset.y
            );
        }

        # endregion

        # region PrivateMethods

        /**
         * <summary>
         * When enabled
         * </summary>
         */
        private void OnEnable()
        {
            if (null == _subject)
                return;

            IMovable motion = _subject.GetComponent<IMovable>();

            if (null == motion)
                return;

            motion.OnDirectionChange.AddListener(FlipHorizontalOffset);
        }

        /**
         * <summary>
         * When Disabled
         * </summary>
         */
        private void OnDisable()
        {
            if (null == _subject)
                return;

            IMovable motion = _subject.GetComponent<IMovable>();

            if (null == motion)
                return;

            motion.OnDirectionChange.RemoveListener(FlipHorizontalOffset);
        }

        /**
         * <summary>
         * 
         * </summary>
         */
        private void FixedUpdate()
        {
            if (null == _subject)
                return;

            Vector2 position = new Vector2(
                _subject.transform.position.x + _offset.x,
                _subject.transform.position.y + _offset.y
            );

            transform.position = Vector2.SmoothDamp(
                transform.position,
                position,
                ref _velocity,
                _smoothTime
            );
        }
        # endregion
    }
}
