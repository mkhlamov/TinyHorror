using UnityEngine;

namespace TinyHorror
{
    public interface IAttackable
    {
        void OnAttack(GameObject attacker, Attack attack);
    }
}