using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

namespace TinyHorror
{
    public class GameManager : Singleton<GameManager>
    {
        #region VariableDeclaration

        #region PublicVars

        #region LevelLoadingPublic
        public GameObject[] SystemPrefabs;
        public event Action OnLevelLoadingStarted;
        public event Action OnLevelLoadingComplete;
        public event Action OnLevelUnloadingStarted;
        public event Action OnLevelUnloadingComplete;
        // Can be done with buildIndex for scene too. 
        public readonly string MAIN_LEVEL_NAME = "_Start";
        public Weapon _defaultWeapon;
        #endregion

        // Transition from old to new game state
        public event Action<GameState, GameState> OnGameStateChanged;
        public GameState CurrentGameState
        {
            get
            {
                return _currentGameState;
            }
        }
        #endregion

        #region PrivateVars
        private List<GameObject> _instancedSystemPrefabs;
        private string _currentLevelName = string.Empty;
        private GameState _currentGameState = GameState.PREGAME;
        private Player _player;
        private int _enemiesCount = 0;
        private Weapon _chosenWeapon;
        #endregion
        #endregion

        #region LevelLoadingMethods
        public void LoadLevel(string levelName)
        {
            AsyncOperation ao = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Single);
            if (ao == null)
            {
                Debug.LogError("[GameManger]: Level " + levelName + " not loaded.");
                return;
            }

            ao.completed += OnLoadOperationComplete;
            _currentLevelName = levelName;
        }

        public void UnLoadLevel(string levelName)
        {
            OnLevelUnloadingStarted?.Invoke();
            AsyncOperation ao = SceneManager.UnloadSceneAsync(levelName);
            if (ao == null)
            {
                Debug.LogError("[GameManger]: Level " + levelName + " not unloaded.");
                return;
            }
            ao.completed += OnUnloadOperationComplete;
        }

        private void OnLoadOperationComplete(AsyncOperation ao)
        {
            if (_currentLevelName == MAIN_LEVEL_NAME)
            {
                UpdateState(GameState.PREGAME);
            }
            else
            {
                UpdateState(GameState.RUNNING);
                _player.EquipWeapon(_chosenWeapon == null ? _defaultWeapon : _chosenWeapon);
            }

            OnLevelLoadingComplete?.Invoke();
        }

        private void OnUnloadOperationComplete(AsyncOperation ao)
        {
            OnLevelUnloadingComplete?.Invoke();
        }
        #endregion

        #region Monobehaviour
        protected override void Start()
        {
            base.Start();
            _instancedSystemPrefabs = new List<GameObject>();
            InstantiateSystemPrefabs();
            DontDestroyOnLoad(gameObject);
        }

        protected override void OnDestroy()
        {
            if (_instancedSystemPrefabs != null)
            {
                foreach (GameObject go in _instancedSystemPrefabs)
                {
                    Destroy(go);
                }
                _instancedSystemPrefabs.Clear();
            }
            base.OnDestroy();
        }
        #endregion

        #region PrivateMethods
        private void InstantiateSystemPrefabs()
        {
            GameObject prefabInstance;
            for (int i = 0; i < SystemPrefabs.Length; i++)
            {
                prefabInstance = Instantiate(SystemPrefabs[i]);
                _instancedSystemPrefabs.Add(prefabInstance);
            }
        }

        private void UpdateState(GameState state)
        {
            GameState prevGameState = _currentGameState;
            _currentGameState = state;

            if (_currentGameState == GameState.PREGAME) { }
            else if (_currentGameState == GameState.RUNNING) {
                FindPlayer();
                FindEnemies();
            }
            else if (_currentGameState == GameState.LOST) { }
            else if (_currentGameState == GameState.WON) { }
            else { }

            OnGameStateChanged?.Invoke(_currentGameState, prevGameState);
        }

        private void OnPlayerDeath()
        {
            UpdateState(GameState.LOST);
        }

        private void FindEnemies()
        {
            var killables = FindObjectsOfType<KillableEvent>();
            var enemies = killables.Where(t => t.GetComponent<Player>() == null);
            _enemiesCount = enemies.Count();
            foreach (var e in enemies)
            {
                e.GetComponent<KillableEvent>().KillableDead += OnEnemyDead;
            }
        }

        private void FindPlayer()
        {
            _player = FindObjectOfType<Player>();
            _player.GetComponent<KillableEvent>().KillableDead += OnPlayerDeath;
        }

        private void OnEnemyDead()
        {
            _enemiesCount -= 1;
            if (0 == _enemiesCount)
            {
                UpdateState(GameState.WON);
            }
        }
        #endregion

        #region PublicMethods
        public void SetNewWeapon(Weapon weapon)
        {
            _chosenWeapon = weapon;
            if (_player != null)
            {
                _player.EquipWeapon(weapon);
            }
        }
        #endregion
    }

    public enum GameState
    {
        PREGAME,
        RUNNING,
        LOST,
        WON
    }
}