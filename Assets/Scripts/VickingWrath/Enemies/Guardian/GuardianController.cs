using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VickingWrath.Persona;
using VickingWrath.PhysicalMotions;
using VickingWrath.PhysicalMotions.Behaviours;
using VickingWrath.Enemies.Guardian.Events;

namespace VickingWrath.Enemies.Guardian
{
    /**
     * <summary>
     * Contains all the data needed in order to make the guardian. A guardian
     * is an enemy wich watch an area and run to the player when he sees him
     * adn smash it has it can.
     * </summary>
     */
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(IMovable))]
    [RequireComponent(typeof(PersonaHealth))]
    [RequireComponent(typeof(PersonaDamage))]
    public class GuardianController : MonoBehaviour
    {
        # region Properties

        /**
         * <summary>
         * The guardian walking speed
         * </summary>
         */
        [SerializeField]
        [Header("Parameters")]
        private float _walkingSpeed = 300f;

        /**
         * <summary>
         * The guardian running speed
         * </summary>
         */
        [SerializeField]
        private float _runningSpeed = 600f;

        /**
         * <summary>
         * Attach here the left point of the guardian area
         * </summary>
         */
        [SerializeField]
        [Header("Physic Object")]
        private GameObject _areaLeftPoint = null;

        /**
         * <summary>
         * Attach here the right point of the guardian area
         * </summary>
         */
        [SerializeField]
        private GameObject _areaRightPoint = null;

        /**
         * <summary>
         * Attach here the vision point of the guardian
         * </summary>
         */
        [SerializeField]
        private GameObject _visionPoint = null;

        /**
         * <summary>
         * The layer wich the player is in
         * </summary>
         */
        [SerializeField]
        private LayerMask _enemyLayer = Physics2D.AllLayers;

        /**
         * <summary>
         * The event triggered each time the guardian sees new enemies
         * </summary>
         */
        [SerializeField]
        [Header("Events")]
        private EnemiesEvent _onEnemiesInVision = new EnemiesEvent();

        /**
         * <summary>
         * The event triggered each time the guardian has new enemies in
         * attack range
         * </summary>
         */
        [SerializeField]
        private EnemiesEvent _onEnemiesInAttackRange = new EnemiesEvent();

        /**
         * <summary>
         * The sprite renderer
         * </summary>
         */
        private SpriteRenderer _sprite = null;

        /**
         * <summary>
         * The motion used to move the guardian
         * </summary>
         */
        private IMovable _motion = null;

        /**
         * <summary>
         * The persona health
         * </summary>
         */
        private PersonaHealth _health = null;

        /**
         * <summary>
         * The persona damage
         * </summary>
         */
        private PersonaDamage _damage = null;

        # endregion

        # region PropertyAccessors

        /**
         * <summary>
         * Test if the guardian is seing an enemy
         * </summary>
         */
        public bool HasEnemiesInVision { get; private set; }

        /**
         * <summary>
         * Test if the guardian has enemie in attack range
         * </summary>
         */
        public bool HasEnemiesInAttackRange { get; private set; }

        /**
         * <summary>
         * Return a list of enemies that this guardian sees
         * </summary>
         */
        public List<GameObject> EnemiesInVision { get; private set; }

        /**
         * <summary>
         * Return a list of enemies that this guardian can hit
         * </summary>
         */
        public List<GameObject> EnemiesInAttackRange { get; private set; }

        /**
         * <summary>
         * Test if the guardian has reached the right point
         * </summary>
         */
        public bool HasReachedRightPoint
        {
            get
            {
                if (null == _areaRightPoint)
                    return false;

                return _sprite.transform.position.x >= _areaRightPoint.transform.position.x;
            }
        }

        /**
         * <summary>
         * Test if the guardian has reached left point
         * </summary>
         */
        public bool HasReachedLeftPoint
        {
            get
            {
                if (null == _areaLeftPoint)
                    return false;

                return _sprite.transform.position.x <= _areaLeftPoint.transform.position.x;
            }
        }

        /**
         * <summary>
         * This function is triggered when the guardian is being uder attack
         * </summary>
         */
        public void WhenUnderAttack(PersonaHealth health, Hit hit)
        {
            if (HasEnemiesInVision)
                return;

            _motion.SwitchDirection();
        }

        # endregion

        # region PublicMethods

        # endregion

        # region PrivateMethods

        /**
         * <summary>
         * Trigger when the guardian is created
         * </summary>
         */
        private void Awake()
        {
            _sprite = GetComponent<SpriteRenderer>();
            _motion = GetComponent<IMovable>();
            _health = GetComponent<PersonaHealth>();
            _damage = GetComponent<PersonaDamage>();
            HasEnemiesInVision = false;
            HasEnemiesInAttackRange = false;
            EnemiesInVision = new List<GameObject>();
        }

        /**
         * <summary>
         * Attach event when this component is enabled
         * </summary>
         */
        private void OnEnable()
        {
            _health.OnAfterTakingDamage.AddListener(WhenUnderAttack);
        }

        /**
         * <summary>
         * Detach the event when the component is disabled
         * </summary>
         */
        private void OnDisable()
        {
            _health.OnAfterTakingDamage.RemoveListener(WhenUnderAttack);
        }

        /**
         * <summary>
         * This method is trigger at each fixed update and update
         * all the data needed by this controller.
         * </summary>
         */
        private void FixedUpdate()
        {
            if (!_areaLeftPoint || !_areaRightPoint || !_visionPoint)
                return;

            UpdateEnemiesInVision();
            UpdateEnemiesInAttackRange();

            Direction direction = HasEnemiesInVision
                ? RunTowardClosestEnemy()
                : WalkTowardNextAreaPoint()
            ;

            _motion.Speed = HasEnemiesInVision ? _runningSpeed : _walkingSpeed;

            _motion.Move(DirectionCaster.ToFloat(direction));
        }

        /**
         * <summary>
         * Update the enemy vision
         * </summary>
         */
        private void UpdateEnemiesInVision()
        {
            Collider2D[] colliders = Physics2D.OverlapAreaAll(
                _sprite.transform.position,
                _visionPoint.transform.position,
                _enemyLayer
            );

            HasEnemiesInVision = colliders.Length > 0;

            if (!HasEnemiesInVision)
            {
                EnemiesInVision.Clear();

                return;
            }

            List<GameObject> newEnemies = new List<GameObject>();
            List<GameObject> enemies = new List<GameObject>();

            foreach (Collider2D collider in colliders)
            {
                GameObject enemy = collider.gameObject;
                bool isExistingEnemy = EnemiesInVision.Contains(enemy);

                enemies.Add(enemy);

                if (!isExistingEnemy)
                    newEnemies.Add(enemy);
            }

            EnemiesInVision = enemies;

            if (newEnemies.Count > 0)
                _onEnemiesInVision.Invoke(this, newEnemies);
        }

        /**
         * <summary>
         * Update the enemies in attack range
         * </summary>
         */
        private void UpdateEnemiesInAttackRange()
        {
            EnemiesInAttackRange = _damage.EnemiesInAttackRange;
            HasEnemiesInAttackRange = EnemiesInAttackRange.Count > 0;

            if (!HasEnemiesInAttackRange)
            {
                EnemiesInAttackRange.Clear();

                return;
            }

            List<GameObject> newEnemies = new List<GameObject>();

            foreach (GameObject enemy in EnemiesInAttackRange)
            {
                bool isExistingEnemy = EnemiesInAttackRange.Contains(enemy);

                if (isExistingEnemy)
                    continue;

                newEnemies.Add(enemy);
            }

            if (newEnemies.Count > 0)
                _onEnemiesInAttackRange.Invoke(this, newEnemies);
        }

        /**
         * <summary>
         * Move toward the closest enemy
         * </summary>
         */
        private Direction RunTowardClosestEnemy()
        {
            return _motion.Direction;
        }

        /**
         * <summary>
         * Walk toward the next area point
         * </summary>
         */
        private Direction WalkTowardNextAreaPoint()
        {
            if (_motion.Direction == Direction.Right && HasReachedRightPoint)
                return Direction.Left;

            if (_motion.Direction == Direction.Left && HasReachedLeftPoint)
                return Direction.Right;

            return _motion.Direction;
        }

        # endregion
    }
}
