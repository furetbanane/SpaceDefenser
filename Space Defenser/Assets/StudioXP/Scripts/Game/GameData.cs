using System;
using UnityEngine;

namespace StudioXP.Scripts.Game
{
    public class GameData : MonoBehaviour
    {
        public static GameData Instance { get; private set; }
        public bool CheckpointIsActive { get; private set; }
        public Vector3 CheckpointPosition { get; private set; }
        
        private Checkpoint _checkpoint;

        private void Awake()
        {
            if(Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }

        public void SetCheckpoint(Checkpoint checkpoint)
        {
            if (_checkpoint)
                _checkpoint.Deactivate();

            _checkpoint = checkpoint;
            CheckpointIsActive = _checkpoint != null;
            if (CheckpointIsActive)
                CheckpointPosition = _checkpoint.transform.position;
        }
    }
}
