using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using VickingWrath.Enemies.Guardian;

namespace VickingWrath.Enemies.Guardian.Events
{
    /**
     * <summary>
     * Triggered when the guardian has enemies in vision or in attack range
     * </summary>
     */
    [Serializable]
    public class EnemiesEvent : UnityEvent<GuardianController, List<GameObject>>
    {
    }
}
