using System;
using System.Collections;
using System.Linq;
using Sirenix.OdinInspector;
using StudioXP.Scripts.Components.Common;
using StudioXP.Scripts.Game;
using UnityEngine;

namespace StudioXP.Scripts.Events
{
    [Serializable]
    public class CollisionFilterEvent
    {
        [LabelText("Filtre (Collision)")] 
        [ValidateInput("HasValidFilter")]
        [SerializeField] private CollisionInfoFilter filter = CollisionInfoFilter.AllColliders | CollisionInfoFilter.AllDirections | CollisionInfoFilter.AllTypes;

        [LabelText("Filtre (Tag)")] 
        [HideInInspector]
        [ValueDropdown("GetTagsView")] [SerializeField]
        private string filterTag = GroupName.Any;
        
        [LabelText("Filtre (Groupes)")]
        [SerializeField] private GroupFilter filterGroups;
        
        [PropertySpace]
        [SerializeField] private ColliderEventInfoEvent collisionEvent;

        public CollisionInfoFilter Filter => filter;

        public string FilterTag => filterTag; 

        public GroupFilter FilterGroup => filterGroups;
        
        public ColliderEventInfoEvent Event => collisionEvent;

        private bool HasValidFilter(CollisionInfoFilter f, ref string errorMessage, ref InfoMessageType? messageType)
        {
            if ((filter & CollisionInfoFilter.AllColliders) == 0)
            {
                errorMessage =
                    "Tu n'as pas défini le type de collider à Trigger ou Collision! L'évènement ne sera jamais lancé!";
                messageType = InfoMessageType.Error;
                return false;
            }
            
            if ((filter & CollisionInfoFilter.AllTypes) == 0)
            {
                errorMessage =
                    "Tu n'as pas défini le type de collision à Entrer, Sortir ou Rester! L'évènement ne sera jamais lancé!";
                messageType = InfoMessageType.Error;
                return false;
            }
            
            if ((filter & CollisionInfoFilter.AllDirections) == 0)
            {
                errorMessage =
                    "Tu n'as pas défini la direction! L'évènement ne sera jamais lancé!";
                messageType = InfoMessageType.Error;
                return false;
            }

            return true;
        }
#if UNITY_EDITOR
        private IEnumerable GetTagsView()
        {
            var tags = UnityEditorInternal.InternalEditorUtility.tags.ToList();
            tags.Insert(0, GroupName.Any);
            return tags;
        }
#endif
    }
}
