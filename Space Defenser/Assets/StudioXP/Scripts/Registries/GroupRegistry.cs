using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace StudioXP.Scripts.Registries
{
    [ExecuteInEditMode]
    public class GroupRegistry : Registry<GroupType, Group>
    {
        private readonly List<uint> _groupId = new List<uint>();

        public static GroupRegistry Instance => (GroupRegistry) InstanceHidden;

        public uint GetFlag(Group group)
        {
            if (group.Identifier < 0 || group.Identifier >= Elements.Count)
                return 0;
            
            return _groupId[group.Identifier];
        }
        
        public override ValueDropdownList<Group> GetView()
        {
            var valueDropdown = base.GetView();
            valueDropdown.RemoveAt(0);
            return valueDropdown;
        }

        protected override void OnAwake()
        {
            if (!Application.isPlaying)
                return;

            for (var i = 0; i < Elements.Count; i++)
            {
                var groupName = Elements[i].Name;
                if (groupName == GroupName.Any || groupName == GroupName.None)
                    throw new ArgumentException($"{groupName} is a reserved group name");
                _groupId.Add((uint)(1 << (i)));
            }
        }
        private static class GroupName
        {
            public static string None = "Aucun";
            public static string Any = "Tous";
        }
    }
}
