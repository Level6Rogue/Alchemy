
using Alchemy.Inspector;
using UnityEngine;
using UnityEngine.UIElements;

namespace Alchemy.Editor.Elements
{
    [UxmlElement]
    public partial class AlchemyToggleFoldoutGroup : AlchemyFoldout
    {
        private readonly StyleSheet _styleSheet = Resources.Load<StyleSheet>("Alchemy/Alchemy-Toggle-Foldout-Group-Styles");
        
        private readonly Toggle _toggle;
        
        public AlchemyToggleFoldoutGroup() : base()
        {
            styleSheets.Add(_styleSheet);
            
            _toggle = new Toggle();
            _toggle.AddToClassList("alchemy-toggle-foldout-group__toggle");
            
            GroupBase.Header.Insert(0, _toggle);
            
            UpdateStyle();
        }

        protected override void UpdateStyle()
        {
            base.UpdateStyle();
            
            _toggle?.EnableInClassList("alchemy-toggle-foldout-group__toggle--boxed", Style == GroupStyle.Boxed);
        }
    }
}