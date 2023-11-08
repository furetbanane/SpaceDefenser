using Sirenix.OdinInspector;
using UnityEngine;

namespace StudioXP.Scripts.Components.Common
{
    public class GroupList : MonoBehaviour
    {
        [LabelText("Groupes")]
        [SerializeField] private GroupFilter groups;

        public GroupFilter GroupFilter
        {
            get => groups;
            set => groups = value;
        }
        
        private void Awake()
        {
            foreach(Transform child in transform)
            {
                var groupAttribute = child.GetComponent<GroupList>();
                if (groupAttribute) continue;
                var newGroupAttribute = child.gameObject.AddComponent<GroupList>();
                newGroupAttribute.groups ??= groups;
                newGroupAttribute.Awake();
            }
        }
    }
}
