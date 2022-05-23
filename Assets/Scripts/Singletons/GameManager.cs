using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
//using Bazisti.Singletons.AudioManager;
//using Bazisti.Player.Fish;

namespace Bazisti.Singletons.GameManager
{
    public class GameManager : SingletonBehaviour<GameManager>
    {
        #region Private Fields
        [SerializeField] int fishObjective = 20;
        [Space]
        [SerializeField] float hungerIncrementStep = 0.2f;
        [SerializeField] float hungerIncrementFrequency = 15f;
        [Space]
        [SerializeField] float lostFishIncrementStep = 1f;
        [SerializeField] float lostFishIncrementFrequency = 20f;
        [Space]
        [SerializeField] float foodIncrementStep = 1.5f;
        [SerializeField] float foodIncrementFrequency = 10f;

        int _foodCount = 0;
        int _lostFishCount = 0;
        float _elapsedTime;

        IEnumerator _timeCounterCoroutine;
        //FishController _player;
        //Spawner _spawner;
        Scene _gameOverScene;
        #endregion

        #region Public Fields
        public int FoodCount { get => _foodCount; set { if (!(value < 0)) _foodCount = value; } }
        public int LostFishCount { get => _lostFishCount; set { if (!(value < 0)) _lostFishCount = value; } }
        public int FishObjective => fishObjective;
        public float ElapsedTime => _elapsedTime;
        public bool HasWon { private set; get;}
        #endregion

        #region Events
        #endregion

        #region Private Methods
        IEnumerator TimeCounter()
        {
            float workingHFrequency = hungerIncrementFrequency;
            float workingLFrequency = lostFishIncrementFrequency;
            float workingFFrequency = foodIncrementFrequency;
            _elapsedTime = 0f;

            while (true)
            {
                yield return null;
                _elapsedTime += Time.deltaTime;

                if (_elapsedTime > workingHFrequency)
                {
                    //_player.SetHungerDecayRate(hungerIncrementStep, true);
                    workingHFrequency += hungerIncrementFrequency;
                }

                if (false && _elapsedTime > workingLFrequency)
                {
                   // _spawner.LostFishFrequency = _spawner.LostFishFrequency + lostFishIncrementStep;
                    workingLFrequency += lostFishIncrementFrequency;
                }

                if (_elapsedTime > workingFFrequency)
                {
                   // _spawner.FoodFrequency = _spawner.FoodFrequency + foodIncrementStep;
                    workingFFrequency += foodIncrementFrequency;
                }
            }
        }

        void FetchReferences(Scene scene, LoadSceneMode this_is_not_used)
        {
            if (scene.name == "Main")
            {   
              //  _player = GameObject.FindWithTag("Player").GetComponent<FishController>();
              //  _spawner = GameObject.FindWithTag("Spawner").GetComponent<Spawner>();

                _timeCounterCoroutine = TimeCounter();
                StartCoroutine(_timeCounterCoroutine);
            }

            if (_gameOverScene == null && scene.name == "GameOver") _gameOverScene = scene;
        }
        #endregion

        #region Public Methods
        public void StartGame()
        {
            if (_gameOverScene != null && _gameOverScene.isLoaded)
                SceneManager.UnloadSceneAsync(_gameOverScene);

            SceneManager.LoadScene("Main");
            SceneManager.LoadScene("UI", LoadSceneMode.Additive);
          //  AudioManager.AudioManager.Instance.Play("Background");
        }

        public void GameOver(bool hasWon)
        {
            if (_timeCounterCoroutine != null)
            {
                StopCoroutine(_timeCounterCoroutine);
                _timeCounterCoroutine = null;
            }

            SceneManager.UnloadSceneAsync("UI");

           // AudioManager.AudioManager.Instance.Play("GameOver");
           // Destroy(_player.gameObject);
           // Destroy(_spawner.gameObject);
           // _player = null;
           // _spawner = null;


            HasWon = hasWon;
            SceneManager.LoadScene("GameOver", LoadSceneMode.Additive);
        }
        #endregion

        #region MonoBehaviour Methods
        void Start()
        {
            DontDestroyOnLoad(Camera.main.gameObject);
            SceneManager.sceneLoaded += FetchReferences;
        }

        void OnDestroy()
        {
            SceneManager.sceneLoaded -= FetchReferences;
        }
        #endregion
    }
}