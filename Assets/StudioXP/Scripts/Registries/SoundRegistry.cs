using System.Collections.Generic;
using UnityEngine;

namespace StudioXP.Scripts.Registries
{
    [ExecuteInEditMode]
    public class SoundRegistry : Registry<SoundType, Sound>
    {
        public static SoundRegistry Instance => (SoundRegistry)InstanceHidden;

        private readonly List<AudioSource> _audioSources = new List<AudioSource>();
        private readonly Dictionary<string, int> _audioSourcesById = new Dictionary<string, int>();

        public void Play(Sound sound)
        {
            if (sound.Identifier < 0 || sound.Identifier >= Elements.Count)
                return;
            
            _audioSources[sound.Identifier].Play();
        }
        
        public void Play(string sound)
        {
            _audioSources[GetElement(sound).Identifier].Play();
        }

        protected override void OnAwake()
        {
            if (!Application.isPlaying)
                return;

            var go = new GameObject();
            go.transform.parent = transform;
            go.name = "Sounds";

            _audioSources.Clear();
            foreach(var sound in Elements)
            {
                var goAudio = new GameObject();
                goAudio.transform.parent = go.transform;
                goAudio.name = sound.Name;
                var audioSource = goAudio.AddComponent<AudioSource>();
                audioSource.clip = sound.AudioClip;
                audioSource.playOnAwake = false;
                audioSource.volume = sound.Volume;
                _audioSources.Add(audioSource);
            }
        }
    }
}
