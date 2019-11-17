using UnityEngine;

namespace TinyHorror
{
    public interface IKillable
    {
        void OnDeath(GameObject killer);
    }
}