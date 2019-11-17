using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TinyHorror
{
    public class OptionsManager : Singleton<OptionsManager>
    {
        #region VariableDeclaration
        [SerializeField]
        private Text text;
        [SerializeField]
        private Slider _masterVolumeSlider;
        [SerializeField]
        private Button _saveButton;
        [SerializeField]
        private Button _startButton;
        // Hardcoded level name is just fine for this prototype, 
        // but we can load SceneManager.GetActiveScene().buildIndex + 1 or 
        // last level from PlayerPrefs for example (as done for the volume level)
        private readonly string FIRST_LEVEL = "Level_0";
        #endregion

        #region MonoBehaviour
        protected override void Start()
        {
            base.Start();
            float savedVolume = PlayerPrefsManager.GetMasterVolume();
            SetVolumeText(savedVolume);
            if (_masterVolumeSlider)
            {
                _masterVolumeSlider.value = savedVolume;
            }
            _masterVolumeSlider.onValueChanged.AddListener(OnMasterVolumeValueChanged);
            if (SoundManager.Instance != null)
            {
                _masterVolumeSlider.onValueChanged.AddListener(SoundManager.Instance.SetVolume);
            }
            _saveButton.onClick.AddListener(OnSaveButtonClick);
            _startButton.onClick.AddListener(OnStartButtonClick);
        }
        #endregion

        #region PrivateMethods
        private void OnMasterVolumeValueChanged(float value)
        {
            SetVolumeText(value);
        }

        private void SetVolumeText(float value)
        {
            text.text = "Master Volume " + value.ToString("F2");
        }

        private void OnSaveButtonClick()
        {
            if (_masterVolumeSlider)
            {
                PlayerPrefsManager.SetMasterVolume(_masterVolumeSlider.value);
            }
            PlayerPrefsManager.Save();
        }

        private void OnStartButtonClick()
        {
            GameManager.Instance.LoadLevel(FIRST_LEVEL);
        }

        protected override void OnDestroy()
        {
            _masterVolumeSlider.onValueChanged.RemoveAllListeners();
            base.OnDestroy();
        }
        #endregion
    }
}