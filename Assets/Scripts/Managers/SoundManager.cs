using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace TinyHorror
{
    public class SoundManager : Singleton<SoundManager>
    {
        [SerializeField]
        private List<AudioSource> _audioInScene;

        protected override void Start()
        {
            base.Start();
            DontDestroyOnLoad(gameObject);
            _audioInScene = new List<AudioSource>();
            GameManager.Instance.OnLevelLoadingComplete += HandleLevelLoaded;
            HandleLevelLoaded();
        }

        private void HandleLevelLoaded()
        {
            _audioInScene.Clear();
            _audioInScene = FindObjectsOfType<AudioSource>().ToList();
            float volume = PlayerPrefsManager.GetMasterVolume();
            SetVolume(volume);
        }

        public void SetVolume(float volume)
        {
            foreach (var a in _audioInScene)
            {
                a.volume = volume;
            }
        }
    }
}