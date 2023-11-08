using StudioXP.Scripts.Components.Movements.Characters;
using UnityEngine;
using UnityEngine.UI;

namespace StudioXP.Scripts.Utils
{
    public class DebugValue : MonoBehaviour
    {
        [SerializeField] private WalkFunction walkFunction;

        private Text _text;

        void Awake()
        {
            _text = GetComponent<Text>();
        }
        
        void Update()
        {
            _text.text = walkFunction.CurrentSpeed.ToString("0.00");
        }
    }
}
