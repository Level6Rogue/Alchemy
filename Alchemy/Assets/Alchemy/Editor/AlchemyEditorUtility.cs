using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using Alchemy.Inspector;
using UnityEngine;
using UnityEngine.UIElements;

namespace Alchemy.Editor
{
    /// <summary>
    /// Alchemy Editor utility functions.
    /// </summary>
    public static class AlchemyEditorUtility
    {
        /// <summary>
        /// Finds the type of drawer that corresponds to PropertyGroupAttribute.
        /// </summary>
        public static Type FindGroupDrawerType(PropertyGroupAttribute attribute)
        {
            return TypeCache.GetTypesWithAttribute<CustomGroupDrawerAttribute>()
                .FirstOrDefault(x => x.GetCustomAttribute<CustomGroupDrawerAttribute>().targetAttributeType == attribute.GetType());
        }
        
        internal static AlchemyGroupDrawer CreateGroupDrawer(PropertyGroupAttribute attribute, Type targetType)
        {
            var drawerType = FindGroupDrawerType(attribute);
            var drawer = (AlchemyGroupDrawer)Activator.CreateInstance(drawerType);
            drawer.SetUniqueId("AlchemyGroupId_" + targetType.FullName + "_" + attribute.GroupPath);
            return drawer;
        }
        
        public static VisualElement GetPropertyFieldWithAttribute(SerializedProperty property)
        {
            Type type = property.serializedObject.targetObject.GetType();

            var members = ReflectionHelper.GetMembers(type, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, true)
                .Where(x => x is MethodInfo or FieldInfo or PropertyInfo);
            
            var rootNode = InspectorHelper.BuildInspectorNode(type);

            foreach (var node in rootNode.DescendantsAndSelf())
            {
                Debug.Log("Node: " + node.Name);
            }
            
            // AlchemyPropertyField alchemyPropertyField = new(property, underlyingType);
            return new VisualElement();
            // alchemyPropertyField;
        }
    }
}