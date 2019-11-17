using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using TinyHorror;

namespace VRStandardAssets.Utils
{
    // This class simply allows the user to return to the main menu.
    public class ReturnToMainMenu : MonoBehaviour
    {
        [SerializeField] private VRInput m_VRInput;                     // Reference to the VRInput in order to know when Cancel is pressed.


        private void OnEnable ()
        {
            m_VRInput.OnCancel += HandleCancel;
        }


        private void OnDisable ()
        {
            m_VRInput.OnCancel -= HandleCancel;
        }

        private void HandleCancel ()
        {
            GameManager.Instance.LoadLevel(GameManager.Instance.MAIN_LEVEL_NAME);
        }
    }
}