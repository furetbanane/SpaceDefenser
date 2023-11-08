using StudioXP.Scripts.UI;
using UnityEngine;

namespace StudioXP.Scripts.Components.Functions
{
    /// <summary>
    /// Une fonction permet d'exécuter une action spécifique à partir d'un évenement Unity.
    ///
    /// Rend disponible les méthodes du menu de jeu et donne accès à plusieurs contrôles de jeu.
    /// </summary>
    public class MenuFunction : MonoBehaviour
    {
        /// <summary>
        /// Reload la scène courante
        /// </summary>
        public void ReloadActiveScene()
        {
            MenuController.Instance.ReloadActiveScene();
        }

        /// <summary>
        /// Met le joueur en mode Game Over.
        /// </summary>
        public void SetGameOver()
        {
            MenuController.Instance.SetGameOver();
        }

        /// <summary>
        /// Pause ou Resume selon l'état actuel de jeu.
        /// </summary>
        public void TogglePause()
        {
            MenuController.Instance.TogglePause();
        }

        /// <summary>
        /// Pause le jeu et affiche un menu pause
        /// </summary>
        public void Pause()
        {
            MenuController.Instance.Pause();
        }

        /// <summary>
        /// Continue le jeu et cache le menu pause.
        /// </summary>
        public void Resume()
        {
            MenuController.Instance.Resume();
        }

        /// <summary>
        /// Quitte le jeu
        /// </summary>
        public void Quit()
        {
            MenuController.Instance.Quit();
        }
    }
}
