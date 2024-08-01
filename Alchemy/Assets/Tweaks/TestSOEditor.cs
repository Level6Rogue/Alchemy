using Alchemy.Editor;
using Alchemy.Editor.Elements;
using UnityEditor;
using UnityEngine.UIElements;

[CustomEditor(typeof(TestSO))]
public class TestSOEditor : AlchemyEditor
{
    public override VisualElement CreateInspectorGUI()
    {
        VisualElement visualElement = base.CreateInspectorGUI();

        visualElement.styleSheets.Add(AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Tweaks/Editor-List-Block-Style.uss"));
        visualElement.styleSheets.Add(AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Tweaks/Editor-ReferenceField-Block-Style.uss"));
        
        ListView listView = visualElement.Q<ListView>();
        listView.AddToClassList("list-view-header-block");
        
        SerializeReferenceField referenceField = visualElement.Q<SerializeReferenceField>();
        referenceField.AddToClassList("reference-field-block");
        
        return visualElement;
    }
}
