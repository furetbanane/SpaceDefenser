using Sirenix.OdinInspector;
using UnityEngine;

namespace StudioXP.Scripts.Components.Movements.Characters
{
    public class ClimbFunction : MonoBehaviour
    {
        [LabelText("Vitesse")]
        [SerializeField] private float speed = 5;

        public void MoveHorizontal(float direction)
        {
            var objectTransform = transform;
            objectTransform.position += (Vector3.right * direction * speed * Time.deltaTime);
        }
        
        public void MoveVertical(float direction)
        {
            var objectTransform = transform;
            objectTransform.position += (Vector3.up * direction * speed * Time.deltaTime);
        }
    }
}
