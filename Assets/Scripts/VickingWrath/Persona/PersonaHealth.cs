using System.Collections;
using UnityEngine;
using VickingWrath.Persona.Events;

namespace VickingWrath.Persona
{
    /**
     * <summary>
     * Represent the ability of an game object to posses some health. It also
     * posses a complete event system allowing you to extend any features
     * you want when an object is healing or taking damage.
     * </summary>
     */
    public class PersonaHealth : MonoBehaviour
    {
        # region Properties

        /**
         * <summary>
         * The maximum health of this object
         * </summary>
         */
        [SerializeField]
        [Header("Parameters")]
        private int _maxHealth = 100;

        /**
         * <summary>
         * The current health of this object
         * </summary>
         */
        [SerializeField]
        private int _health = 100;

        /**
         * <summary>
         * Is this persona invincible
         * </summary>
         */
        [SerializeField]
        private bool _invincible = false;

        /**
         * <summary>
         * Triggered before this persona is taking damage
         * </summary>
         */
        [SerializeField]
        [Header("Events")]
        private TakeDamageEvent _beforeTakingDamage = new TakeDamageEvent();

        /**
         * <summary>
         * Triggered before adter this persona is taking damage
         * </summary>
         */
        [SerializeField]
        private TakeDamageEvent _afterTakingDamage = new TakeDamageEvent();

        /**
         * <summary>
         * Triggered before this persona is healing
         * </summary>
         */
        [SerializeField]
        private HealEvent _beforeHealing = new HealEvent();

        /**
         * <summary>
         * Triggered after this persona is healing.
         * </summary>
         */
        [SerializeField]
        private HealEvent _afterHealing = new HealEvent();

        /**
         * <summary>
         * Triggered when this persona die
         * </summary>
         */
        [SerializeField]
        private DieEvent _whenDying = new DieEvent();

        # endregion

        # region PropertyAccessors

        /**
         * <summary>
         * Retrieve and set the maximum health of this object
         * </summary>
         */
        public int MaxHealth
        {
            get => _maxHealth;
            set => _maxHealth = value;
        }

        /**
         * <summary>
         * Retrieve the current health
         * </summary>
         */
        public int Health { get => _health; }

        /**
         * <summary>
         * Returns true if this object is currently dead
         * </summary>
         */
        public bool IsDead { get => _health <= 0; }

        /**
         * <summary>
         * Hook any behaviour before this persona is taking damage
         * </summary>
         */
        public TakeDamageEvent OnBeforeTakingDamage { get => _beforeTakingDamage; }

        /**
         * <summary>
         * Hook any behaviour after this persona is taking damage
         * </summary>
         */
        public TakeDamageEvent OnAfterTakingDamage { get => _afterTakingDamage; }

        /**
         * <summary>
         * Hook any behaviour that happens before this persona is healing
         * </summary>
         */
        public HealEvent OnBeforeHealing { get => _beforeHealing; }

        /**
         * <summary>
         * Hook any behaviour that happens after this persona is healing
         * </summary>
         */
        public HealEvent OnAfterHealing { get => _afterHealing; }

        /**
         * <summary>
         * Hook any behaviour that happens when this persona die
         * </summary>
         */
        public DieEvent OnDying { get => _whenDying; }

        /**
         * <summary>
         * Allow the persona to be invincible
         * </summary>
         */
        public bool Invincible
        {
            get => _invincible;
            set => _invincible = value;
        }

        # endregion

        # region PublicMethods

        /**
         * <summary>
         * Take a damage
         * </summary>
         */
        public void TakeDamage(Hit hit)
        {
            if (Invincible)
                return;

            _beforeTakingDamage.Invoke(this, hit);

            _health -= hit.Damage.Value;

            if (_health < 0)
            {
                _whenDying.Invoke(this);

                _health = 0;
            }

            _afterTakingDamage.Invoke(this, hit);
        }

        /**
         * <summary>
         * Allow an object to heal
         * </summary>
         */
        public void Heal(int amount)
        {
            _beforeHealing.Invoke(this, amount);

            _health += amount;

            if (_health > _maxHealth)
                _health = _maxHealth;

            _afterHealing.Invoke(this, amount);
        }

        # endregion

        # region PrivateMethods
        # endregion
    }
}
