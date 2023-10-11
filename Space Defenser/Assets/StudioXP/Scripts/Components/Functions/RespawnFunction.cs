using System;
using StudioXP.Scripts.Game;
using UnityEngine;

namespace StudioXP.Scripts.Components.Functions
{
    /// <summary>
    /// Une fonction permet d'exécuter une action spécifique à partir d'un évenement Unity.
    ///
    /// Permet à un objet de réapparaitre à la position de la caméra. La caméra est déplacé avant la réapparition pour
    /// empêcher l'objet de se retrouver en dehors de la caméra (Ce qui metterait l'objet dans la killzone).
    /// </summary>
    public class RespawnFunction : MonoBehaviour
    {
        private GameObject _camera;
        
        private void Awake()
        {
            _camera = GameObject.FindWithTag("MainCamera");
        }

        void Start()
        {
            if (!GameData.Instance.CheckpointIsActive) return;
            
            var position = GameData.Instance.CheckpointPosition;
            position.z = _camera.transform.position.z;
            _camera.transform.position = position;

            var currentTransform = transform;
            position.z = currentTransform.position.z;
            currentTransform.position = position;
        }
    }
}
