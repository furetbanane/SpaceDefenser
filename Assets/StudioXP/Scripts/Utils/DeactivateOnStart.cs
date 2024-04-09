using UnityEngine;

namespace StudioXP.Scripts.Utils
{
    public class DeactivateOnStart : MonoBehaviour
    {
        void Start()
        {
            gameObject.SetActive(false);
        }
    }
}
