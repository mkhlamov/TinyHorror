using UnityEngine;

namespace TinyHorror
{
    [RequireComponent(typeof(AudioSource))]
    public class EnemySounds : MonoBehaviour
    {
        #region VariableDeclaration
        [SerializeField]
        private AudioClip _walkSound;
        [SerializeField]
        private AudioClip _attackSound;
        [SerializeField]
        private AudioClip _deathSound;
        private AudioSource _audioSource;
        #endregion

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void PlaySound(AudioClip ac)
        {
            _audioSource.Stop();
            _audioSource.clip = ac;
            _audioSource.Play();
        }

        #region PublicMethods
        public void PlayWalkSound()
        {
            PlaySound(_walkSound);
        }

        public void PlayAttackSound()
        {
            PlaySound(_attackSound);
        }

        public void PlayDeathSound()
        {
            PlaySound(_deathSound);
        }
        #endregion
    }
}