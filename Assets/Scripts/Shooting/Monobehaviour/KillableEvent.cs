using System;
using UnityEngine;

namespace TinyHorror
{
    public class KillableEvent : MonoBehaviour, IKillable
    {
        public event Action KillableDead;

        public void OnDeath(GameObject killer)
        {
            KillableDead?.Invoke();
        }
    }
}