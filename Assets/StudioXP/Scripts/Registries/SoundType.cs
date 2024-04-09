using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace StudioXP.Scripts.Registries
{
    [Serializable]
    public class SoundType : IRegistryType
    {
        [LabelText("Identifiant")]
        [SerializeField] private string name;
        
        [LabelText("Clip Audio")]
        [AssetSelector(Paths = "Assets/Etudiant/Sounds|Assets/StudioXP/Sounds")]
        [SerializeField] private AudioClip audioClip;
        
        [LabelText("Volume")]
        [MinValue(0)]
        [SerializeField] private float volume = 0.4f;
        
        public String Name => name;
        public AudioClip AudioClip => audioClip;

        public float Volume => volume;

        private SoundType()
        {
        }
    }
}
