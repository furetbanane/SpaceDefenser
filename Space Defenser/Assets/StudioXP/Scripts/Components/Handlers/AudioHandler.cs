using StudioXP.Scripts.Registries;
using UnityEngine;

namespace StudioXP.Scripts.Components.Handlers
{
    /// <summary>
    /// Permet de jouer un son enregistré dans le <see cref="SoundRegistry"/>
    /// </summary>
    public class AudioHandler : MonoBehaviour
    {
        /// <summary>
        /// Jouer le son passé en paramètre.
        /// Doit être un ID enregistré dans le <see cref="SoundRegistry"/>
        /// </summary>
        /// <param name="identifier">Identifiant du son à jouer</param>
        public void PlaySound(string identifier)
        {
            SoundRegistry.Instance.Play(identifier);
        }
    }
}
