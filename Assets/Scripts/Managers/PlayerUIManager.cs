using UnityEngine;

namespace TinyHorror
{
    public class PlayerUIManager : MonoBehaviour
    {
        #region VariableDeclaration
        [SerializeField]
        private GameObject _endGameScreen;
        [SerializeField]
        private GameObject _winText;
        [SerializeField]
        private GameObject _loseText;
        #endregion

        #region Monobehaviour
        private void OnEnable()
        {
            GameManager.Instance.OnGameStateChanged += ShowEndGameUI;
        }

        private void OnDisable()
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.OnGameStateChanged -= ShowEndGameUI;
            }
        }
        #endregion

        private void ShowEndGameUI(GameState currentGameState, GameState prevGameState)
        {
            _endGameScreen.SetActive(currentGameState == GameState.WON || currentGameState == GameState.LOST);
            _winText.SetActive(currentGameState == GameState.WON);
            _loseText.SetActive(currentGameState == GameState.LOST);
        }
    }
}