using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace TinyHorror
{
    public class EnemyWalkAround : MonoBehaviour
    {
        #region VariableDeclaration
        [SerializeField]
        private Transform _targetsParent;
        private List<Transform> _targets;
        private NavMeshAgent _agent;
        private bool _walkingAround;
        private bool _attacking;
        private bool _arrived;
        private int _destinationPoint;
        #endregion

        #region Monobehaviour
        void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
            _targets = new List<Transform>();
            foreach (Transform c in _targetsParent)
            {
                _targets.Add(c);
            }
        }

        void Update()
        {
            if (_agent.pathPending)
            {
                return;
            }

            if (!CanWalkAround()) {
                return;
            }

            if (_walkingAround)
            {
                if (_agent.remainingDistance <= _agent.stoppingDistance)
                {
                    if (!_arrived)
                    {
                        _arrived = true;
                        StartCoroutine(GoToNextPoint());
                    }
                }
                else
                {
                    _arrived = false;
                }
            }
            else
            {
                StartCoroutine("GoToNextPoint");
            }
        }
        #endregion

        #region PublicMethods
        public void SetAttacking(bool e)
        {
            _attacking = e;
            _walkingAround = !e;
        }
        #endregion

        #region PrivateMethods
        private bool CanWalkAround()
        {
            return !_attacking;
        }

        private IEnumerator GoToNextPoint()
        {
            if (_targets.Count == 0)
            {
                yield break;
            }
            _walkingAround = true;
            yield return new WaitForSeconds(1);
            _arrived = false;
            _agent.destination = _targets[_destinationPoint].position;
            int randomIndex = Random.Range(0, _targets.Count);
            if (randomIndex == _destinationPoint)
            {
                randomIndex = (randomIndex + 1) % _targets.Count;
            }
            _destinationPoint = randomIndex;
        }
        #endregion
    }
}