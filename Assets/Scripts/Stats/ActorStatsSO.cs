using UnityEngine;

namespace TinyHorror
{
    [CreateAssetMenu(fileName = "NewActorStats", menuName = "Actor/Stats", order = 1)]
    public class ActorStatsSO : ScriptableObject
    {
        #region VariableDeclaration
        public int _maxHealth       = 0;
        public int _currentHealth   = 0;
        public int _damage          = 0;
        #endregion

        #region Health
        public void AddHealth(int health)
        {
            if ((_currentHealth + health) > _maxHealth)
            {
                _currentHealth = _maxHealth;
            } else
            {
                _currentHealth += health;
            }
        }

        public void TakeDamage(int amount)
        {
            _currentHealth -= amount;
        }
        #endregion
    }
}