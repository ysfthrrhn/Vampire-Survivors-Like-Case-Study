using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        public GameStates gameState = GameStates.Active;

        [SerializeField] private UIManager _UIManager;

        [SerializeField, Range(0,5)] private float endDelay = 3;

        private int coinCount = 0;
        private int killCount = 0;

        private void Awake()
        {
            if (Instance != null)
                Destroy(Instance);
            else
                Instance = this;
        }
        private void Start()
        {
            GetCoinInfo();
            UpdateKillInfo();
        }
        private void GetCoinInfo()
        {
            if (PlayerPrefs.HasKey("Coin"))
                coinCount = PlayerPrefs.GetInt("Coin");
            else
                PlayerPrefs.SetInt("Coin", 0);
            
            UpdateCoinInfo();
        }
        public void UpdateCoin()
        {
            coinCount++;
            UpdateCoinInfo();
            SavePlayerPrefs();// Hotfix! added later because, It wont save at Android.
        }
        
        private void UpdateCoinInfo()
        {
            _UIManager.UpdateCoinText(coinCount);
        }
        public void UpdateKill()
        {
            killCount++;
            UpdateKillInfo();
        }
        private void UpdateKillInfo()
        {
            _UIManager.UpdateKillText(killCount);
        }
        public IEnumerator GameFinished()
        {
            yield return new WaitForSeconds(endDelay);
            gameState = GameStates.Finished;
            PlayerPrefs.SetInt("Coin", coinCount);
            SavePlayerPrefs();
            _UIManager.OpenEndPanel();
        }

        public void Restart()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private void OnApplicationQuit()
        {
            SavePlayerPrefs();
        }

        private void SavePlayerPrefs()
        {
            PlayerPrefs.SetInt("Coin", coinCount);
            PlayerPrefs.Save();
        }
    }
}

