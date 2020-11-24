using System.Collections.Generic;
using UnityEngine;
using DiceSystem = VickingWrath.DiceSystem;
using VickingWrath.Persona.Events;
using VickingWrath.PhysicalMotions.Behaviours;
using VickingWrath.PhysicalMotions;

namespace VickingWrath.Persona
{
    /**
     * <summary>
     * This mono behaviour allows any game object to attack other PersonaHealth.
     *
     * </summary>
     */
    public class PersonaDamage : MonoBehaviour
    {
        # region Properties

        /**
         * <summary>
         * The max damage number this persona can inflict
         * </summary>
         */
        [SerializeField]
        [Header("Parameters")]
        private DiceSystem.DiceFace _dice = DiceSystem.DiceFace.Six;

        /**
         * <summary>
         * Define here the base modifier (example, this personna is whering
         * a sword wich inflict 3 damage per attack no matters the dice
         * result).
         * </summary>
         */
        [SerializeField]
        [Range(0, 20)]
        private int _baseModifier = 0;

        /**
         * <summary>
         * Define here the critical modifier which happens when a dice
         * is making the maximum of it's result.
         * </summary>
         */
        [SerializeField]
        [Range(1, 5)]
        private int _criticalModifier = 1;

        /**
         * <summary>
         * attach a hit point wich represent the hit box used to determine
         * damage collision.
         * </summary>
         */
        [SerializeField]
        [Header("Physics")]
        private GameObject _hitPoint = null;

        /**
         * <summary>
         * Represent the radius of the hit point circle collider
         * </summary>
         */
        [SerializeField]
        private float _hitPointRadius = 0.25f;

        /**
         * <summary>
         * Represent the layers that the hit point circle collides with.
         * </summary>
         */
        [SerializeField]
        private LayerMask _hitPointLayer = Physics.AllLayers;

        /**
         * <summary>
         * Define the force to apply to the given hit
         * </summary>
         */
        [SerializeField]
        private Vector2 _hitForce = Vector2.zero;

        /**
         * <summary>
         * Events triggered before this persona attack an other persona health
         * </summary>
         */
        [SerializeField]
        [Header("Events")]
        private AttackEvent _beforeAttacking = new AttackEvent();

        /**
         * <summary>
         * Events triggered after this persona has attacked an other persona
         * health
         * </summary>
         */
        [SerializeField]
        private AttackEvent _afterAttacking = new AttackEvent();

        # endregion

        # region PropertyAccessors

        /**
         * <summary>
         * Retrieve or update this persona dice damage
         * </summary>
         */
        public DiceSystem.DiceFace Dice
        {
            get => _dice;
            set => _dice = value;
        }

        /**
         * <summary>
         * Retrive or update the base modifier
         * </summary>
         */
        public int BaseModifier
        {
            get => _baseModifier;
            set => _baseModifier = value;
        }

        /**
         * <summary>
         * Retrieve or update the critical modifier
         * </summary>
         */
        public int CriticalModifier
        {
            get => _criticalModifier;
            set => _criticalModifier = value;
        }

        /**
         * <summary>
         * Returns a list of game object that are in range of attack
         * </summary>
         */
        public List<GameObject> EnemiesInAttackRange
        {
            get
            {
                List<GameObject> enemies = new List<GameObject>();

                if (!_hitPoint)
                    return enemies;

                Collider2D[] colliders = Physics2D.OverlapCircleAll(
                    _hitPoint.transform.position,
                    _hitPointRadius,
                    _hitPointLayer
                );

                foreach (Collider2D collider in colliders)
                {
                    enemies.Add(collider.gameObject);
                }

                return enemies;
            }
        }

        /**
         * <summary>
         * Hook any behaviour that can happe before attacking a persona health.
         * </summary>
         */
        public AttackEvent OnBeforeAttacking { get => _beforeAttacking; }

        /**
         * <summary>
         * Hook any behaviour that can happen after attacking a persona health.
         * </summary>
         */
        public AttackEvent OnAfterAttacking { get => _afterAttacking; }

        # endregion

        # region PublicMethods

        /**
         * <summary>
         * Launch an attack
         * </summary>
         */
        public void Attack()
        {
            if (null == _hitPoint)
                return;

            Hit hit = new Hit(
                DiceSystem.Dice.Roll(_dice, _baseModifier, _criticalModifier),
                _hitForce,
                GuessDirection()
            );

            Collider2D[] colliders = Physics2D.OverlapCircleAll(
                _hitPoint.transform.position,
                _hitPointRadius,
                _hitPointLayer
            );

            if (colliders.Length <= 0)
                return;

            foreach (Collider2D collider in colliders)
            {
                PersonaHealth health = collider.gameObject.GetComponent<PersonaHealth>();

                if (null == health)
                    continue;

                _beforeAttacking.Invoke(this, health, hit);

                health.TakeDamage(hit);

                _afterAttacking.Invoke(this, health, hit);
            }
        }

        # endregion

        # region PrivateMethods

        /**
         * <summary>
         * Draw the hit circle collider when a hitPoint is attached to
         * this component.
         * </summary>
         */
        private void OnDrawGizmosSelected()
        {
            if (!_hitPoint)
                return;

            Gizmos.DrawWireSphere(
                _hitPoint.transform.position,
                _hitPointRadius
            );
        }

        /**
         * <summary>
         * Guess a hit direction by cheking if any IMovable physical motion
         * is present in this game object, otherwise return Direction.None
         * </summary>
         */
        private Direction GuessDirection()
        {
            IMovable motion = gameObject.GetComponent<IMovable>();

            if (null == motion)
                return Direction.None;

            return motion.Direction;
        }

        # endregion
    }
}
