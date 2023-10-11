using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace StudioXP.Scripts.Utils
{
    [CreateAssetMenu(fileName = "SceneConfiguration", menuName = "Studio XP/Scene Configuration", order = 1)]
    public class SceneConfiguration : ScriptableObject
    {
        [SerializeField, AssetList] private List<GameObject> baseSceneObjects = new List<GameObject>();
        
        public List<GameObject> BaseSceneObjects => baseSceneObjects;
    }
}
