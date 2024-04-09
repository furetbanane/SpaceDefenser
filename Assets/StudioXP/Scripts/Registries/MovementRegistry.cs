using Sirenix.OdinInspector;
using UnityEngine;

namespace StudioXP.Scripts.Registries
{
    [ExecuteInEditMode]
    public class MovementRegistry : Registry<MovementType, Movement>
    {
        public static MovementRegistry Instance => (MovementRegistry)InstanceHidden;
        
        public override ValueDropdownList<Movement> GetView()
        {
            var valueDropdown = base.GetView();
            //valueDropdown.RemoveAt(0);
            return valueDropdown;
        }
        
        protected override void OnAwake()
        {
        }
    }
}
