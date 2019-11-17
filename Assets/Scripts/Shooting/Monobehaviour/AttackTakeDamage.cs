using UnityEngine;

namespace TinyHorror
{
    public class AttackTakeDamage : MonoBehaviour, IAttackable
    {
        private ActorStats _actorStats;

        private void Awake()
        {
            _actorStats = GetComponent<ActorStats>();
        }

        public void OnAttack(GameObject attacker, Attack attack)
        {
            _actorStats.TakeDamage(attack.Damage);

            // Death
            if (_actorStats.IsDead())
            {
                var killableComponents = GetComponents<IKillable>();
                foreach (IKillable k in killableComponents)
                {
                    k.OnDeath(attacker);
                }
            }
        }
    }
}