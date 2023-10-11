using UnityEngine;

namespace StudioXP.Scripts.UI
{
    /// <summary>
    /// Un contrôleur est une classe globale qui gère des éléments logiques du jeu.
    ///
    /// Le contrôleur de menu contrôle tout ce qui a trait au menu, pause, game over, etc.
    /// Peut être appelé par un objet du jeu à l'aide de MenuFunction.
    /// </summary>
    public class MenuController : MonoBehaviour
    {
        [SerializeField] private GameObject menuPause;
        [SerializeField] private GameObject menuRetry;
        
        public static MenuController Instance { get; private set; }
        
        private void Awake()
        {
            if (Instance == null)
                Instance = this;
        }
        
        private void Start()
        {
            menuPause.SetActive(false);
            menuRetry.SetActive(false);
        }

        private void Update()
        {
            if (Input.GetButtonDown("Cancel"))
                TogglePause();
        }

        /// <summary>
        /// Reload la scène courante
        /// </summary>
        public void ReloadActiveScene()
        {
            GameController.Instance.ReloadActiveScene();
        }

        /// <summary>
        /// Met le joueur en mode Game Over.
        /// </summary>
        public void SetGameOver()
        {
            if (menuPause.activeInHierarchy) return;
            menuRetry.SetActive(true);
        }

        /// <summary>
        /// Pause ou Resume selon l'état actuel de jeu.
        /// </summary>
        public void TogglePause()
        {
            if (menuRetry.activeInHierarchy) return;
            
            if(GameController.Instance.IsPaused)
                Resume();
            else
                Pause();
        }

        /// <summary>
        /// Pause le jeu et affiche un menu pause
        /// </summary>
        public void Pause()
        {
            if (menuRetry.activeInHierarchy) return;
            
            menuPause.SetActive(true);
            GameController.Instance.Pause();
        }

        /// <summary>
        /// Continue le jeu et cache le menu pause.
        /// </summary>
        public void Resume()
        {
            if (menuRetry.activeInHierarchy) return;
            
            menuPause.SetActive(false);
            GameController.Instance.Resume();
        }

        /// <summary>
        /// Quitte le jeu
        /// </summary>
        public void Quit()
        {
            GameController.Instance.Quit();
        }
    }
}
