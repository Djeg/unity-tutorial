using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using VickingWrath.Persona;

namespace VickingWrath.HUD.LifeBar
{
    /**
     * <summary>
     * Control the behavior of the life bar HUD.
     * </summary>
     */
    public class LifeBarController : MonoBehaviour
    {
        # region Properties

        /**
         * <summary>
         * Put here the life bar image to retract.
         * </summary>
         */
        [SerializeField]
        [Header("Objects")]
        private GameObject _bar = null;

        /**
         * <summary>
         * Add here the subject in for wich the life bar must
         * be aply.
         * </summary>
         */
        [SerializeField]
        private GameObject _subject = null;

        /**
         * <summary>
         * The gradient color to use
         * </summary>
         */
        [SerializeField]
        private Gradient _gradient = null;

        /**
         * <summary>
         * The subject health
         * </summary>
         */
        private PersonaHealth _health = null;

        /**
         * <summary>
         * The RectTransform object.
         * </summary>
         */
        private RectTransform _rect = null;

        /**
         * <summary>
         * The image attached to the bar
         * </summary>
         */
        private Image _image = null;

        # endregion

        # region PropertyAccessors

        /**
         * <summary>
         * The persona health
         * </summary>
         */
        public PersonaHealth Health { get => _health; }

        # endregion

        # region PublicMethods

        /**
         * <summary>
         * Trigger when taking a damage
         * </summary>
         */
        public void WhenTakingDamage(PersonaHealth health, Hit hit)
        {
            UpdateAnchor();
        }

        # endregion

        # region PrivateMethods

        /**
         * <summary>
         * Trigger when the component is created
         * </summary>
         */
        private void Awake()
        {
            if (null == _bar || null == _subject)
                throw new Exception("You must specify a life bar and subject game object.");

            _health = _subject.GetComponent<PersonaHealth>();

            if (null == _health)
                throw new Exception("The subject must have a PersonaHealth.");

            _rect = _bar.GetComponent<RectTransform>();
            _image = _bar.GetComponent<Image>();

            if (null == _rect || null == _image)
                throw new Exception("The life bar must have a RectTransform component.");
        }

        /**
         * <summary>
         * When the component is enabled
         * </summary>
         */
        private void OnEnable()
        {
            UpdateAnchor();

            _health.OnAfterTakingDamage.AddListener(WhenTakingDamage);
        }

        /**
         * <summary>
         * When the component is disable
         * </summary>
         */
        private void OnDisable()
        {
            _health.OnAfterTakingDamage.RemoveListener(WhenTakingDamage);
        }

        /**
         * <summary>
         * Update the anchor to match the current persona health
         * </summary>
         */
        private void UpdateAnchor()
        {
            float x = (float)_health.Health / (float)_health.MaxHealth;

            _rect.anchorMax = new Vector2(x, _rect.anchorMax.y);

            _image.color = _gradient.Evaluate(x);
        }

        # endregion
    }
}
