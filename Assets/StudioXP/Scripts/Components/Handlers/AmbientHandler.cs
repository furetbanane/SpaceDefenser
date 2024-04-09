using UnityEngine;

namespace StudioXP.Scripts.Components.Functions
{
    
    public class AmbientHandler : MonoBehaviour
    {
        private AudioSource _audioSource;
        
        void Start()
        {
            _audioSource = GameObject.FindWithTag("CoreAmbience").GetComponent<AudioSource>();
        }

        public void ChangeMusic(AudioClip clip)
        {
            _audioSource.Stop();
            if (clip == null) return;
                
            _audioSource.clip = clip;
            _audioSource.Play();
        }

        public void StopMusic()
        {
            _audioSource.Stop();
        }

        public void PlayMusic()
        {
            _audioSource.Play();
        }
    }
}
