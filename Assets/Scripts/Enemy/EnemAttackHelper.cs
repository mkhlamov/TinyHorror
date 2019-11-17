using UnityEngine;

namespace TinyHorror
{
    public class EnemAttackHelper : MonoBehaviour
    {
        [SerializeField]
        private EnemyAttacking _attacking;

        void Start()
        {
            _attacking = GetComponentInParent<EnemyAttacking>();
        }

        public void DealDamage()
        {
            _attacking.DealDamage();
        }
    }
}