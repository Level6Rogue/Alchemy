using Alchemy.Editor;
using Alchemy.Editor.Elements;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Tweaks.Editor
{
    [CustomAttributeDrawer(typeof(BlockAttribute))]
    public class BlockAttributeDrawer : AlchemyAttributeDrawer
    {
        public override void OnCreateElement()
        {
            switch (SerializedProperty.propertyType)
            {
                case SerializedPropertyType.ManagedReference:
                {
                    SerializeReferenceField serializeReferenceField = TargetElement.Q<SerializeReferenceField>();
                    if (serializeReferenceField != null)
                    {
                        serializeReferenceField.AddToClassList("reference-field-block");
                        return;
                    }
                    
                    // Foldout foldout = TargetElement.Q<Foldout>();
                    // if (foldout != null)
                    // {
                    //     foldout.AddToClassList("foldout-block");
                    //     return;
                    // }
                }
                break;

                case SerializedPropertyType.Generic:
                {
                   if (SerializedProperty.isArray)
                       break;
                    
                    Foldout foldout = TargetElement.Q<Foldout>();
                    if (foldout != null)
                    {
                        foldout.AddToClassList("foldout-block");
                        return;
                    }
                }
                break;
            }
            
            ListView listView = TargetElement.Q<ListView>();
            if (listView != null)
            {
                listView.AddToClassList("list-view-header-block");
            }
            else
            {
                Foldout parentFoldout = TargetElement.GetFirstOfType<Foldout>();
                if (parentFoldout != null)
                {
                    parentFoldout.AddToClassList("foldout-block");
                }
            }
        }
    }
}