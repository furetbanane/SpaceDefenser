using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using StudioXP.Scripts.Components.Common;
using StudioXP.Scripts.Registries;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace StudioXP.Scripts.Attributes.Editor
{
        [CustomPropertyDrawer(typeof(GroupFilter))]
        public class GroupFilterProperty : PropertyDrawer
        {
                private bool _initialized = false;
                private List<int> _values;

                public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
                {
                        return base.GetPropertyHeight(property, label) * 2;
                }

                public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
                {
                        bool groupsChanged = false;
                        if (!_initialized)
                        {
                                Init(property.Copy());
                                groupsChanged = true;
                        }
                        
                        EditorGUI.BeginProperty(position, label, property);
                        property.Next(true);
                        if (property.isArray)
                        {
                                var listProperty = property.Copy();
                                if (SirenixEditorFields.Dropdown(position, label, _values, GroupRegistry.Instance.GetView(),
                                        true))
                                {
                                        groupsChanged = true;
                                        listProperty.arraySize = _values.Count;
                                        for (int i = 0; i < _values.Count; i++)
                                        {
                                                listProperty.GetArrayElementAtIndex(i).intValue = _values[i];
                                        }

                                        listProperty.serializedObject.ApplyModifiedProperties();
                                }
                        }

                        property.Next(false);
                        UpdateFlags(property.Copy(), groupsChanged);
                        
                        EditorGUI.EndProperty();
                }

                private void Init(SerializedProperty property)
                {
                        _values ??= new List<int>();
                        _values.Clear();

                        property.Next(true);
                        if (property.isArray)
                        {
                                int length = property.arraySize;
                                for (int i = 0; i < length; i++)
                                {
                                        _values.Add(property.GetArrayElementAtIndex(i).intValue);
                                }
                        }
                        
                        _initialized = true;
                }

                private void UpdateFlags(SerializedProperty property, bool groupsChanged)
                {
                        if (!groupsChanged) return;
                        int flags = _values.Aggregate(0, (current, groupId) => current | 1 << groupId);

                        property.intValue = flags;
                }
        }
}
