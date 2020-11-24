using System;
using UnityEngine;
using UnityEngine.Events;
using VickingWrath.Persona;

namespace VickingWrath.Persona.Events
{
    /**
     * <summary>
     * This event is triggered before and after a Persona is healing.
     * </summary>
     */
    [Serializable]
    public class HealEvent : UnityEvent<PersonaHealth, int>
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
