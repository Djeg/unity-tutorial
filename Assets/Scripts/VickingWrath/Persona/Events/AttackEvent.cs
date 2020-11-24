using System;
using UnityEngine.Events;
using VickingWrath.Persona;

namespace VickingWrath.Persona.Events
{
    /**
     * <summary>
     * This event is triggered before and after a persona is attacking an
     * other persona health.
     * </summary>
     */
    [Serializable]
    public class AttackEvent : UnityEvent<PersonaDamage, PersonaHealth, Hit>
    {
        # region Properties
        # endregion

        # region PropertyAccessors
        # endregion

        # region PublicMethods
        # endregion

        # region PrivateMethods
        # endregion
    }
}
