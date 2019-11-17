using UnityEngine;
using VRStandardAssets.Utils;

namespace TinyHorror
{
    [RequireComponent(typeof(VRInteractiveItem))]
    public class AttackDebug : MonoBehaviour, IAttackable
    {
        public void OnAttack(GameObject attacker, Attack attack)
        {
            if (attack.IsCritical)
            {
                Debug.Log("Critical");
            }
        }
    }
}