using UnityEngine;
using UnityEngine.AI;

namespace TinyHorror
{
    public class EnemyAttacking : MonoBehaviour
    {
        #region VariableDeclaration
        [SerializeField]
        private Transform _eye;
        [SerializeField]
        private AttackSO _attack;
        [SerializeField]
        private float _speedRotation = 50f;
        private Player _player;
        private Transform _playerHeadPos;
        private NavMeshAgent _agent;
        private Animator _anim;
        private Vector3 _lastKnownPlayerPos;
        private EnemyWalkAround _walkAround;
        private bool _isPlayerAlive = true;
        private ActorStats _actorStats;
        #endregion

        #region Monobehaviour
        private void Start()
        {
            _anim           = GetComponentInChildren<Animator>();
            _agent          = GetComponent<NavMeshAgent>();
            _walkAround     = GetComponent<EnemyWalkAround>();
            _lastKnownPlayerPos  = transform.position;
            _player         = FindObjectOfType<Player>();
            _playerHeadPos  = _player.gameObject.GetComponentInChildren<OVRCameraRig>().transform;
            _actorStats     = GetComponent<ActorStats>();

            KillableEvent kEvent = _player.gameObject.GetComponent<KillableEvent>();
            if (kEvent != null)
            {
                kEvent.KillableDead += OnPlayerDeath;
            }
        }

        private void Update()
        {
            if (CanSeePlayer())
            {
                _agent.SetDestination(_playerHeadPos.position);
                _walkAround.SetAttacking(true);
                // Start attacking if we are close
                _anim.SetBool("Attack", _agent.remainingDistance <= _agent.stoppingDistance);

                // Player can teleport behind. We need to rotate to him
                Vector3 direction = _playerHeadPos.position - transform.position;
                direction.y = 0;
                Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * _speedRotation); ;
            } else
            {
                _anim.SetBool("Attack", false);
                _walkAround.SetAttacking(false);
            }
            _anim.SetFloat("Speed", _agent.velocity.sqrMagnitude);
        }
        #endregion

        #region PublicMethods
        public void DealDamage()
        {
            if (CanSeePlayer() && _agent.remainingDistance <= _agent.stoppingDistance)
            {
                GameObject target = _player.gameObject;
                // we can have multiple IAttackables on one object responsible for different effects
                Attack attack = _attack.CreateAttack(_actorStats);
                var attackableComponents = target.GetComponentsInChildren<IAttackable>();
                foreach (IAttackable a in attackableComponents)
                {
                    a.OnAttack(gameObject, attack);
                }
            }
        }
        #endregion

        #region PrivateMethods
        private bool CanSeePlayer()
        {
            if (!_isPlayerAlive)
            {
                return false;
            }
            bool canSee = false;
            Ray ray = new Ray(_eye.position, _playerHeadPos.position - _eye.position);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                canSee = hit.transform.gameObject == _player.gameObject;
            }
            if (canSee)
            {
                _lastKnownPlayerPos = _playerHeadPos.position;
            }
            return canSee;
        }

        private void OnPlayerDeath()
        {
            _isPlayerAlive = false;
        }
        #endregion
    }
}