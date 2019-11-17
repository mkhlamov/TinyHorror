using UnityEngine;

namespace TinyHorror
{
    [CreateAssetMenu(fileName = "New AttackSO", menuName = "Attack")]
    public class AttackSO : ScriptableObject
    {
        public float baseDamage;
        public float criticalChance;
        public float criticalHitBuff;

        // defenderStats can be used if we give armor to actors
        // not implemented now, just for the demonstration of extensibility
        public Attack CreateAttack(ActorStats attackerStats, ActorStats defenderStats = null)
        {
            float damage = attackerStats.GetDamage();
            damage += baseDamage;

            bool isCritical = Random.value < criticalChance;
            if (isCritical)
            {
                damage *= criticalHitBuff;
            }

            return new Attack((int)damage, isCritical);
        }
    }
}