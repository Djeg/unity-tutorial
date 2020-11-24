using System;
using UnityEngine;
using UnityEngine.Events;
using VickingWrath.Persona;

namespace VickingWrath.Persona.Events
{
    /**
     * <summary>
     * Events triggered after anf before a personna has been taken some hit.
     * </summary>
     */
    [Serializable]
    public class TakeDamageEvent : UnityEvent<PersonaHealth, Hit>
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
