using System;
using UnityEngine;

namespace StudioXP.Scripts.Game
{
    public class Checkpoint : MonoBehaviour
    {
        [SerializeField] private Material activeMaterial;
        [SerializeField] private Material inactiveMaterial;
        [SerializeField] private SpriteRenderer flag;
    
        public bool IsActive { get; private set; }

        private Animator _flagAnimator;

        private void Awake()
        {
            _flagAnimator = flag.GetComponent<Animator>();
            
            if (GameData.Instance == null)
            {
                var go = new GameObject();
                go.AddComponent<GameData>();
                go.name = "GameData";
                Instantiate(go);
            }
        }

        public void Activate()
        {
            if (IsActive) return;
            IsActive = true;

            UpdateFlag();
            GameData.Instance.SetCheckpoint(this);
        }

        public void Deactivate()
        {
            if (!IsActive) return;
            IsActive = false;
            UpdateFlag();
        }

        private void UpdateFlag()
        {
            flag.material = IsActive ? activeMaterial : inactiveMaterial;
            if (_flagAnimator != null)
                _flagAnimator.enabled = IsActive;
        }
    }
}
