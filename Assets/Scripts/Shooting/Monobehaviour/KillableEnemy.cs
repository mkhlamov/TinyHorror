using UnityEngine;
using UnityEngine.AI;
using VRStandardAssets.Utils;

namespace TinyHorror
{
    public class KillableEnemy : MonoBehaviour, IKillable
    {
        private NavMeshAgent _navMeshAgent;
        private EnemyNavigationAnimation _enemyNavigationAnimation;
        private EnemyAttacking _enemyAttacking;
        private EnemyWalkAround _enemyWalkAround;
        private VRInteractiveItem _VRInteractiveItem;
        private BoxCollider _boxCollider;

        public void OnDeath(GameObject killer)
        {
            _enemyNavigationAnimation.enabled   = false;
            _enemyAttacking.enabled             = false;
            _enemyWalkAround.enabled            = false;
            _navMeshAgent.enabled               = false;
            _VRInteractiveItem.enabled          = false;
            _boxCollider.enabled                = false;
        }

        private void Start()
        {
            _navMeshAgent               = GetComponent<NavMeshAgent>();
            _enemyAttacking             = GetComponent<EnemyAttacking>();
            _enemyNavigationAnimation   = GetComponent<EnemyNavigationAnimation>();
            _enemyWalkAround            = GetComponent<EnemyWalkAround>();
            _VRInteractiveItem          = GetComponent<VRInteractiveItem>();
            _boxCollider                = GetComponent<BoxCollider>();
        }
    }
}