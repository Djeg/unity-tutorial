using System.Collections;
using UnityEngine;
using VickingWrath.Persona;
using TMPro;

namespace VickingWrath.Persona.Damage
{
    /**
     * <summary>
     * Allows to display damages on a given game object.
     * </summary>
     */
    [RequireComponent(typeof(PersonaHealth))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class DamageDisplay : MonoBehaviour
    {
        # region Properties

        /**
         * <summary>
         * The damage prefab
         * </summary>
         */
        [SerializeField]
        [Header("Damage Prefabs")]
        private GameObject _damagePrefab = null;

        /**
         * <summary>
         * The critical damage prefab
         * </summary>
         */
        [SerializeField]
        private GameObject _criticalDamagePrefab = null;

        /**
         * <summary>
         * The persona health
         * </summary>
         */
        private PersonaHealth _health = null;

        /**
         * <summary>
         * The object sprite renderer
         * </summary>
         */
        private SpriteRenderer _sprite = null;

        # endregion

        # region PropertyAccessors
        # endregion

        # region PublicMethods

        /**
         * <summary>
         * Display the amount of damage
         * </summary>
         */
        public void Display(PersonaHealth health, Hit hit)
        {
            if (null == _damagePrefab || null == _criticalDamagePrefab)
            {
                Debug.LogWarning("You must attach a damage and a critical damage prefabs");

                return;
            }

            GameObject prefab = hit.Damage.IsCritical ? _criticalDamagePrefab : _damagePrefab;

            GameObject damage = Instantiate(
                prefab,
                _sprite.transform.position,
                Quaternion.identity,
                gameObject.transform
            );

            TMP_Text text = damage.GetComponentInChildren<TMP_Text>();

            if (null == text)
                return;

            text.text = hit.Damage.Value.ToString();
        }

        # endregion

        # region PrivateMethods

        /**
         * <summary>
         * Initialize the component
         * </summary>
         */
        private void Awake()
        {
            _health = GetComponent<PersonaHealth>();
            _sprite = GetComponent<SpriteRenderer>();
        }

        /**
         * <summary>
         * Attach the event when the component is enabled
         * </summary>
         */
        private void OnEnable()
        {
            if (null == _health)
                return;

            _health.OnAfterTakingDamage.AddListener(Display);
        }

        /**
         * <summary>
         * Detach the event when the component is disabled
         * </summary>
         */
        private void OnDisable()
        {
            if (null == _health)
                return;

            _health.OnAfterTakingDamage.RemoveListener(Display);
        }

        # endregion
    }
}
