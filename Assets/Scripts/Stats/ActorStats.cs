using UnityEngine;

namespace TinyHorror
{
    public class ActorStats : MonoBehaviour
    {
        #region VariableDeclaration
        public ActorStatsSO _actorStatsSOdefault;
        public ActorStatsSO _actorStatsSO;
        public GameObject _actorWeaponSlot;
        #endregion

        #region Monobehaviour
        private void Start()
        {
            // Here we just set default stats every time
            // but this can be modified to save stats between 
            //game sessions and so on
            if (_actorStatsSOdefault != null)
            {
                _actorStatsSO = Instantiate(_actorStatsSOdefault);
            }
        }
        #endregion

        #region Health
        public void AddHealth(int health)
        {
            _actorStatsSO.AddHealth(health);
        }

        public void TakeDamage(int damage)
        {
            _actorStatsSO.TakeDamage(damage);
        }

        public int GetHealth()
        {
            return _actorStatsSO._currentHealth;
        }

        public int GetDamage()
        {
            return _actorStatsSO._damage;
        }

        public bool IsDead()
        {
            return _actorStatsSO._currentHealth <= 0;
        }
        #endregion

        private void LogStats()
        {
            Debug.LogFormat("Actor {0} stats:", name);
            Debug.Log("maxHealth = " + _actorStatsSO._maxHealth);
            Debug.Log("currentHealth = " + _actorStatsSO._currentHealth);
            Debug.Log("damage = " + _actorStatsSO._damage);
        }
    }
}