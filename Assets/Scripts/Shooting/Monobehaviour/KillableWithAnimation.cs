using UnityEngine;

namespace TinyHorror
{
    public class KillableWithAnimation : MonoBehaviour, IKillable
    {
        private Animator _animator;

        public void OnDeath(GameObject killer)
        {
            _animator.SetTrigger("Dead");
        }

        private void Awake()
        {
            _animator = GetComponentInChildren<Animator>();
        }
    }
}