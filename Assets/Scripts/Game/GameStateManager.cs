using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Game
{
    public class GameStateManager : MonoBehaviour
    {
        public static GameStateManager Instance;

        [SerializeField] private string nextScene;

        public UnityEvent onDeath = new UnityEvent();
        public UnityEvent onWin = new UnityEvent();

        private bool isDead;
        private HashSet<GameObject> players = new HashSet<GameObject>();
        private HashSet<GameObject> playersFinished = new HashSet<GameObject>();

        private void Awake()
        {
            Instance = this;
        }

        public void ReloadScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void NextScene()
        {
            SceneManager.LoadScene(nextScene);
        }

        public void TriggerDeath()
        {
            if (!isDead)
            {
                onDeath.Invoke();
            }

            isDead = true;
        }

        public void RegisterPlayer(GameObject player)
        {
            players.Add(player);
        }

        public void PlayerFinished(GameObject player)
        {
            playersFinished.Add(player);
            if (playersFinished.Count == players.Count)
            {
                onWin.Invoke();
            }
        }
    }
}