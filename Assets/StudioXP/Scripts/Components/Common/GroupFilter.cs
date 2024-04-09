using System;
using System.Collections.Generic;
using UnityEngine;

namespace StudioXP.Scripts.Components.Common
{
    [Serializable]
    public class GroupFilter
    {
        private const uint NoneFlag = 0;
        private const uint AnyFlag = 0xFFFFFFFF;

        public static readonly GroupFilter None = new GroupFilter(NoneFlag);
        public static readonly GroupFilter Any = new GroupFilter(AnyFlag);
        
        [SerializeField] private List<int> groupsList;
        [SerializeField] private uint flags = 0;

        public List<int> Groups
        {
            get => groupsList;
            set => groupsList = value;
        }

        public float Flags => flags;
        
        public bool Match(uint otherFlags)
        {
            return (flags & otherFlags) > 0;
        }
        
        public bool Match(GroupFilter filter)
        {
            return filter.Match(flags);
        }
 
        public bool Equals(uint otherFlags) 
        {
            return flags == otherFlags;
        }
        
        public bool Equals(GroupFilter filter)
        {
            return filter.Equals(flags);
        }

        private GroupFilter(uint flags)
        {
            groupsList = new List<int>();
            this.flags = flags;
        }
    }
}
