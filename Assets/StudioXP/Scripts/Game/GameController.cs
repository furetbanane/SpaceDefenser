using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace StudioXP.Scripts.UI
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private UnityEvent onPause;
        [SerializeField] private UnityEvent onResume;
        
        public static GameController Instance { get; private set; }

        public bool IsPaused => Time.timeScale == 0;
        
        private void Awake()
        {
            if (Instance == null)
                Instance = this;
        }
        
        public void ReloadActiveScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void LoadNextScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        public void LoadScene(int scene)
        {
            SceneManager.LoadScene(scene);
        }

        public void Quit()
        {
            Application.Quit();
        }

        public void TogglePause()
        {
            if(IsPaused)
                Resume();
            else
                Pause();
        }

        public void Pause()
        {
            Time.timeScale = 0;
            onPause.Invoke();
        }
        
        public void Resume()
        {
            Time.timeScale = 1;
            onResume.Invoke();
        }
    }
}
