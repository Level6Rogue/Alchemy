using Alchemy.Editor.Elements;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine.UIElements;

namespace Alchemy.Editor
{
    //[CustomEditor(typeof(CustomEditorTest))]
    public class CustomEditorTestEditor : AlchemyEditor
    {
        public override VisualElement CreateInspectorGUI()
        {
            VisualElement inspector = base.CreateInspectorGUI();
            
            inspector.Add(new Button { text = "Test Button" } );
            
            inspector.Add(new VisualElement { style = { height = 40 }});
            
            SerializedProperty interfaceProperty = serializedObject.FindProperty("testInterface");

            //Missing the HorizontalLine attribute
            AlchemyPropertyField alchemyPropertyField1 = new(interfaceProperty, interfaceProperty.GetUnderlyingType());
            
            inspector.Add(alchemyPropertyField1);
            
            inspector.Add(new VisualElement { style = { height = 40 }});
            
            AlchemyPropertyField alchemyPropertyField2 = new(interfaceProperty, interfaceProperty.GetUnderlyingType());
            
            inspector.Add(alchemyPropertyField2);
            
            inspector.Add(AlchemyEditorUtility.GetPropertyFieldWithAttribute(interfaceProperty));
            
            return inspector;
        }
        
        
    }
}