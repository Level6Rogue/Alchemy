using Alchemy.Inspector;
using UnityEngine;
using UnityEngine.UIElements;

namespace Alchemy.Editor.Elements
{
    [UxmlElement]
    public partial class AlchemyFoldout : Foldout
    {
        private readonly StyleSheet _styleSheet = Resources.Load<StyleSheet>("Alchemy/Alchemy-Foldout-Styles");
        
        private readonly AlchemyGroupBase _groupBase;
        private readonly Toggle _toggle;
        private readonly VisualElement _unityContent;
        
        public Toggle Toggle => _toggle;
        public AlchemyGroupBase GroupBase => _groupBase;
        
        
        #region Uxml Attributes
        
        private GroupStyle _style = GroupStyle.Default;
        [UxmlAttribute("box-group-style")]
        public GroupStyle Style
        {
            get => _style;
            set
            {
                _style = value;
                UpdateStyle();
            }
        }
        
        private HeaderStyle _headerStyle = HeaderStyle.Default;
        [UxmlAttribute("header-style")]
        public HeaderStyle HeaderStyle
        {
            get => _headerStyle;
            set
            {
                _headerStyle = value;
                UpdateStyle();
            }
        }
        
        private BodyStyle _bodyStyle = BodyStyle.Default;
        [UxmlAttribute("body-style")]
        public BodyStyle BodyStyle
        {
            get => _bodyStyle;
            set
            {
                _bodyStyle = value;
                UpdateStyle();
            }
        }
        
        
        private TintColor _tintColor = TintColor.Active;
        [UxmlAttribute]
        public TintColor TintColor
        {
            get => _tintColor;
            set
            {
                _tintColor = value;
                UpdateStyle();
            }
        }
        
        #endregion Uxml Attributes
        
        public AlchemyFoldout()
        {
            styleSheets.Add(_styleSheet);
         
            AddToClassList("alchemy-foldout");
            
            _groupBase = new() { name = "group" };
            hierarchy.Insert(0, _groupBase);
            
            _toggle = this.Q<Toggle>();
            _unityContent = this.Q<VisualElement>("unity-content");

            //Let this foldout be openable even if it's disabled
            Clickable clickable = InternalAPIHelper.GetClickable(_toggle);
            InternalAPIHelper.SetAcceptClicksIfDisabled(clickable, true);
            
            _groupBase.Header.Add(_toggle);
            _groupBase.contentContainer.Add(_unityContent);

            _toggle.AddToClassList("alchemy-foldout__toggle");
            
            RegisterCallback<ChangeEvent<bool>>(FoldoutToggledEvent);
            
            UpdateStyle();
        }

        private void FoldoutToggledEvent(ChangeEvent<bool> evt)
        {
            UpdateStyle();
        }

        protected virtual void UpdateStyle()
        {
            _groupBase.Style = Style;
            _groupBase.HeaderStyle = HeaderStyle;
            _groupBase.BodyStyle = BodyStyle;
            _groupBase.TintColor = TintColor;
            _groupBase.ShowBody = value;
            
            _toggle.EnableInClassList("alchemy-foldout__toggle--boxed", Style == GroupStyle.Boxed);
            _unityContent.EnableInClassList("alchemy-foldout__content--boxed", Style == GroupStyle.Boxed);
        }
        
        public void HideFoldout(bool hide)
        {
            VisualElement checkmark = _toggle.Q<VisualElement>("unity-checkmark");
            checkmark.EnableInClassList("alchemy-foldout__toggle__checkmark--hidden", hide);
            
            Label label = _toggle.Q<Label>();
            label.EnableInClassList("alchemy-foldout__toggle__label--hidden", hide);
            
            Clickable clickable = InternalAPIHelper.GetClickable(_toggle);
            InternalAPIHelper.SetAcceptClicksIfDisabled(clickable, !hide);
            
            Toggle.SetEnabled(!hide);
            _toggle.EnableInClassList("alchemy-foldout__toggle--hidden", hide);
            
            value = !hide;
        }
    }
}