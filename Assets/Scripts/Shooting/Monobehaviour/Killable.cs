using UnityEngine;

namespace TinyHorror
{
    public class Killable : MonoBehaviour, IKillable
    {
        public void OnDeath(GameObject killer)
        {
            Destroy(gameObject);
        }
    }
}