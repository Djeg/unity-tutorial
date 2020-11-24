using System;
using System.Collections;
using UnityEngine.Events;
using VickingWrath.PhysicalMotions.Behaviours;

namespace VickingWrath.PhysicalMotions.Events
{
    /**
     * <summary>
     * This event is triggered when changing direction.
     * </summary>
     */
    [Serializable]
    public class ChangeDirectionEvent : UnityEvent<IMovable>
    {
    }
}
