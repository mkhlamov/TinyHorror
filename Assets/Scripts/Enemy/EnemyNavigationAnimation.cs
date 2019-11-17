using UnityEngine;
using UnityEngine.AI;

namespace TinyHorror
{
    public class EnemyNavigationAnimation : MonoBehaviour
    {
        #region VariableDeclaration
        NavMeshAgent _agent;
        Animator _anim;
        #endregion

        void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
            _anim = GetComponentInChildren<Animator>();
        }

        void Update()
        {
            _anim.SetFloat("Speed", _agent.velocity.sqrMagnitude);
        }
    }
}