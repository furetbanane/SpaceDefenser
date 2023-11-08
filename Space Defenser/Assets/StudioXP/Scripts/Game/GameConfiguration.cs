using UnityEngine;

namespace StudioXP.Scripts.Game
{
    public class GameConfiguration : MonoBehaviour
    {
        [SerializeField] private LayerMask walkableLayers;
        [SerializeField] private LayerMask wallLayers;
        
        public static GameConfiguration Instance { get; private set; }

        public LayerMask WalkableLayers => walkableLayers;
        public LayerMask WallLayers => wallLayers;
        
        private void Awake()
        {
            if (Instance == null)
                Instance = this;
        }
    }
}
