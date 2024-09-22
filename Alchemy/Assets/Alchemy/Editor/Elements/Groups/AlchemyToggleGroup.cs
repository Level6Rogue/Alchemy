
using Alchemy.Inspector;
using UnityEngine;
using UnityEngine.UIElements;

namespace Alchemy.Editor.Elements
{
    [UxmlElement]
    public partial class AlchemyToggleGroup : AlchemyGroupBase
    {
        private readonly StyleSheet _styleSheet = Resources.Load<StyleSheet>("Alchemy/Alchemy-Toggle-Group-Styles");
        
        private readonly Toggle _toggle;
      
        #region Uxml Attributes
        
        private string _title = "Group Box";
        [UxmlAttribute]
        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                _toggle.text = value;
            }
        }
        
        private string toggleBindingPath;
        [UxmlAttribute]
        public string ToggleBindingPath
        {
            get => toggleBindingPath;
            set
            {
                toggleBindingPath = value;
                _toggle.bindingPath = value;
            }
        }
        
        private bool _disableChildren = false;
        [UxmlAttribute]
        public bool DisableChildren
        {
            get => _disableChildren;
            set
            {
                _disableChildren = value;
                SetContentEnabled(_toggle.value);
            }
        }
        
        private bool _disableIf = false;
        [UxmlAttribute]
        public bool DisableIf
        {
            get => _disableIf;
            set
            {
                _disableIf = value;
                SetContentEnabled(_toggle.value);
            }
        }
        
        #endregion Uxml Attributes
        
        public AlchemyToggleGroup()
        {
            styleSheets.Add(_styleSheet);
            
            _toggle = new() { name = "toggle" };
            _toggle.AddToClassList("alchemy-toggle-group__toggle");
            Header.Add(_toggle);
            
            _toggle.RegisterValueChangedCallback(OnToggleChanged);
        }

        private void OnToggleChanged(ChangeEvent<bool> evt) => SetContentEnabled(evt.newValue);

        private void SetContentEnabled(bool enabled)
        {
            foreach (VisualElement child in contentContainer.Children())
                child.SetEnabled(!_disableChildren || !_disableIf == enabled);
        }

        protected override void UpdateStyles()
        {
            base.UpdateStyles();
            
            _toggle.EnableInClassList("alchemy-toggle-group__toggle--boxed", Style == GroupStyle.Boxed);
        }
    }
}