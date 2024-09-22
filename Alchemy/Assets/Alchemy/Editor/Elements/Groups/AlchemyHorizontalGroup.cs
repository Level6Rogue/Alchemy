using Alchemy.Editor.Elements;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Tweaks
{
    [UxmlElement]
    public partial class AlchemyHorizontalGroup : VisualElement
    {
        private readonly StyleSheet _styleSheet = Resources.Load<StyleSheet>("Elements/HorizontalGroupDrawer-Styles");
        
        private int _lastChildCount = -1;
        
        public AlchemyHorizontalGroup()
        {
            styleSheets.Add(_styleSheet);
            
            AddToClassList("horizontal-group__main-element");
            
            RegisterCallback<GeometryChangedEvent>(OnGeometryChanged);
        }

        private void OnGeometryChanged(GeometryChangedEvent evt)
        {
            if (childCount == _lastChildCount)
                return;
            
            _lastChildCount = childCount;
            
            foreach (VisualElement child in Children())
            {
                int count = 100 / parent.childCount;
                child.style.width = new StyleLength(new Length(count, LengthUnit.Percent));
                
                foreach (AlchemyPropertyField field in child.Query<AlchemyPropertyField>().Build())
                    field.AlignField(true, true);
            }
        }
    }
}