using Sirenix.OdinInspector;
using StudioXP.Scripts.Game;
using UnityEngine;

namespace StudioXP.Scripts.Components.Functions
{
    /// <summary>
    /// Une fonction permet d'exécuter une action spécifique à partir d'un évenement Unity.
    ///
    /// Permet de faire apparaitre un prefab spécifique dans le monde.
    /// </summary>
    public class SpawnFunction : SXPMonobehaviour
    {
        [LabelText("Prefab à faire apparaître")]
        [AssetsOnly]
        [AssetSelector(Paths = Paths.Prefabs)]
        [Required("Ajoute le prefab qui apparaîtra lorsque le joueur activera le bloc")]
        [SerializeField] private GameObject prefabToSpawn;
        
        [LabelText("Position de spawn locale")]
        [SerializeField] private Vector2 localSpawnPosition;

        public void Spawn()
        {
            var go = Instantiate(prefabToSpawn, transform);
            go.transform.localPosition = localSpawnPosition;
        }
    }
}
