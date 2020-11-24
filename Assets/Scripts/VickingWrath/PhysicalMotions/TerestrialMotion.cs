using System;
using System.Collections;
using UnityEngine;
using VickingWrath.Persona;
using VickingWrath.PhysicalMotions.Events;
using VickingWrath.PhysicalMotions.Behaviours;

namespace VickingWrath.PhysicalMotions
{
    /**
     * <summary>
     * Define the ability of any game object to move on the ground. If you
     * are making any Character wich is not aerial. This motion is the one
     * to pick.
     * </summary>
     */
    [RequireComponent(typeof(Rigidbody2D))]
    public class TerestrialMotion : MonoBehaviour, IMovable, IJumpable
    {
        # region Properties

        /**
         * <summary>
         * The direction of the motion
         * </summary>
         */
        [SerializeField]
        [Header("Parameters")]
        private Direction _direction = Direction.Right;

        /**
         * <summary>
         * The speed attached to this motion.
         * </summary>
         */
        [SerializeField]
        private float _speed = 300f;

        /**
         * <summary>
         * The smooth time apply to the SmoothDamp when mooving
         * </summary>
         */
        [SerializeField]
        private float _smoothTime = 0.15f;

        /**
         * <summary>
         * The jump force attached to this motion.
         * </summary>
         */
        [SerializeField]
        private float _jumpForce = 200f;

        /**
         * <summary>
         * The maximum number of jump that this motion can make.
         * </summary>
         */
        [SerializeField]
        [Range(1, 5)]
        private int _maxJump = 1;

        /**
         * <summary>
         * Define the interval accepted between two jump
         * </summary>
         */
        [SerializeField]
        private float _jumpInterval = 0.2f;

        /**
         * <summary>
         * Define the sensisitivy from wich the object move.
         * </summary>
         */
        [SerializeField]
        private float _sensitivity = 0.3f;

        /**
         * <summary>
         * Freeze any movement (but not jump)
         * </summary>
         */
        [SerializeField]
        private bool _freezeMovement = false;

        /**
         * <summary>
         * Freeze the ability to jump
         * </summary>
         */
        [SerializeField]
        private bool _freezeJump = false;

        /**
         * <summary>
         * Attached here a ground detector object wich defines the ability
         * of the motion to jump and detect the ground.
         * </summary>
         */
        [SerializeField]
        [Header("Ground Detection Parameters")]
        private GameObject _groundDetector = null;

        /**
         * <summary>
         * Define the layer of the ground
         * </summary>
         */
        [SerializeField]
        private LayerMask _groundLayer = Physics2D.AllLayers;

        /**
         * <summary>
         * Events trigger when this motion is changing direction.
         * </summary>
         */
        [SerializeField]
        [Header("Events")]
        private ChangeDirectionEvent _onDirectionChange = new ChangeDirectionEvent();


        /**
         * <summary>
         * Events trigger when the persona is jumping
         * </summary>
         */
        [SerializeField]
        private JumpEvent _onJump = new JumpEvent();

        /**
         * <summary>
         * The current horizontal movement
         * </summary>
         */
        private float _horizontalMovement = 0f;

        /**
         * <summary>
         * The velocity used by the SmoothDamp deplacemnt.
         * </summary>
         */
        private Vector2 _velocity = Vector2.zero;

        /**
         * <summary>
         * The rigibody 2D attached to the object
         * </summary>
         */
        private Rigidbody2D _body;

        /**
         * <summary>
         * This is te current count of jump
         * </summary>
         */
        private int _currentJumpCount = 0;

        /**
         * <summary>
         * The last time in seconds since the last jump
         * </summary>
         */
        private float _lastJumpTime = 0;

        # endregion

        # region PropertyAccessors

        /**
         * <summary>
         * The movement direction
         * </summary>
         */
        public Direction Direction { get => _direction; set => _direction = value; }

        /**
         * <summary>
         * Test if the current object is mooving
         * </summary>
         */
        public bool IsMooving { get => Math.Abs(_horizontalMovement) >= _sensitivity; }

        /**
         * <summary>
         * The movement speed
         * </summary>
         */
        public float Speed
        {
            get => _speed;
            set => _speed = value;
        }

        /**
         * <summary>
         * The smooth time used by the SmoothDamp algorithm
         * </summary>
         */
        public float SmoothTime
        {
            get => _smoothTime;
            set => _smoothTime = value;
        }

        /**
         * <summary>
         * The horizontal movement
         * </summary>
         */
        public float HorizontalMovement { get => _horizontalMovement; }

        /**
         * <summary>
         * The current velocity
         * </summary>
         */
        public Vector2 Velocity { get => _velocity; }

        /**
         * <summary>
         * The event triggered when changing direction
         * </summary>
         */
        public ChangeDirectionEvent OnDirectionChange { get => _onDirectionChange; }

        /**
         * <summary>
         * The event trigger on jump
         * </summary>
         */
        public JumpEvent OnJump { get => _onJump; }

        /**
         * <summary>
         * The jump froce
         * </summary>
         */
        public float JumpForce
        {
            get => _jumpForce;
            set => _jumpForce = value;
        }

        /**
         * <summary>
         * Does the current object is juming
         * </summary>
         */
        public bool IsJumping
        {
            get
            {
                if (null == _groundDetector)
                    return false;

                Collider2D[] colliders = Physics2D.OverlapPointAll(
                    _groundDetector.transform.position,
                    _groundLayer
                );

                return colliders.Length <= 0;
            }
        }

        /**
         * <summary>
         * Retrieve the maximum number of jump an object can do
         * </summary>
         */
        public int MaxJump
        {
            get => _maxJump;
            set => _maxJump = value;
        }

        /**
         * <summary>
         * The ground detector object used to detect ground collision.
         * </summary>
         */
        public GameObject GroundDetector
        {
            get => _groundDetector;
            set => _groundDetector = value;
        }

        /**
         * <summary>
         * Freeze or Unfreeze the movement
         * </summary>
         */
        public bool IsMoveFrozen
        {
            get => _freezeMovement;
            set => _freezeMovement = value;
        }

        /**
         * <summary>
         * Freeze or UnFreeze the ability to jump
         * </summary>
         */
        public bool IsJumpFrozen
        {
            get => _freezeJump;
            set => _freezeJump = value;
        }

        # endregion

        # region PublicMethods

        /**
         * <summary>
         * Move the object
         * </summary>
         */
        public void Move(float horizontalMovement)
        {
            if (IsMoveFrozen)
            {
                Stop();

                return;
            }

            GuessNewDirection(horizontalMovement);

            _horizontalMovement = horizontalMovement * _speed * Time.fixedDeltaTime;

            Vector2 target = new Vector2(
                _horizontalMovement,
                _body.velocity.y
            );

            _body.velocity = Vector2.SmoothDamp(
                _body.velocity,
                target,
                ref _velocity,
                _smoothTime
            );
        }

        /**
         * <summary>
         * Stop the object
         * </summary>
         */
        public void Stop()
        {
            _body.velocity = Vector2.SmoothDamp(
                _body.velocity,
                Vector2.zero,
                ref _velocity,
                _smoothTime
            );
        }

        /**
         * <summary>
         * Make the object jump
         * </summary>
         */
        public void Jump()
        {
            if (IsJumpFrozen)
                return;

            bool jumping = IsJumping;

            if (!IsJumping)
                _currentJumpCount = 0;

            bool hasReachedMaxJump = _currentJumpCount == _maxJump;
            bool canJumpAgain = (Time.fixedTime - _lastJumpTime) >= _jumpInterval;

            if (hasReachedMaxJump || !canJumpAgain)
                return;

            TriggerJump();
        }

        /**
         * <summary>
         * Switch the current direction
         * </summary>
         */
        public void SwitchDirection()
        {
            Direction newDirection = Direction == Direction.Left
                ? Direction.Right
                : Direction.Left
            ;

            _direction = newDirection;

            if (_onDirectionChange != null) {
                gameObject.transform.Rotate(0f, 180f, 0f);
                _onDirectionChange.Invoke(this);
            }
        }

        /**
         * <summary>
         * apply a force to the current Rigidbody2D
         * </summary>
         */
        public void ApplyForce(Vector2 force)
        {
            _body.AddForce(force);
        }

        /**
         * <summary>
         * Apply a hit force
         * </summary>
         */
        public void ApplyHitForce(PersonaHealth health, Hit hit)
        {
            if (health.IsDead)
                return;

            ApplyForce(hit.Strength);
        }

        # endregion

        # region PrivateMethods

        /**
         * <summary>
         * Trigger when the object is "created" by unity.
         * </summary>
         */
        private void Awake()
        {
            _body = GetComponent<Rigidbody2D>();
        }

        /**
         * <summary>
         * Apply the hit force if any persona health is present in the game
         * object.
         * </summary>
         */
        private void OnEnable()
        {
            PersonaHealth health = GetComponent<PersonaHealth>();

            if (null == health)
                return;

            health.OnAfterTakingDamage.AddListener(ApplyHitForce);
        }

        /**
         * <summary>
         * Remove the apply hit force listener
         * </summary>
         */
        private void OnDisable()
        {
            PersonaHealth health = GetComponent<PersonaHealth>();

            if (null == health)
                return;

            health.OnAfterTakingDamage.RemoveListener(ApplyHitForce);
        }

        /**
         * <summary>
         * Guess the new direction based on the horizontal movement given.
         * </summary>
         */
        private Direction GuessNewDirection(float horizontalMovement)
        {
            if (Math.Abs(horizontalMovement) < _sensitivity)
                return _direction;

            Direction newDirection = horizontalMovement > 0 ? Direction.Right : Direction.Left;

            if (newDirection == _direction)
                return _direction;

            SwitchDirection();

            return _direction;
        }

        /**
         * <summary>
         * Trigger a jump base on the jump force
         * </summary>
         */
        private void TriggerJump()
        {
            _body.AddForce(new Vector2(0f, _jumpForce));
            _currentJumpCount += 1;
            _lastJumpTime = Time.fixedTime;

            _onJump.Invoke(this);
        }

        # endregion
    }
}
