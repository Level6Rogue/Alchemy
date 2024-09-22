using System;
using System.Collections.Generic;
using Alchemy.Inspector;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Alchemy.Editor.Elements
{
    [UxmlElement]
    public partial class AlchemyGroupBase : VisualElement
    {
        private readonly StyleSheet _baseStyleSheet = Resources.Load<StyleSheet>("Alchemy/Custom-Variables-Dark");
        private readonly StyleSheet _groupStyleSheet = Resources.Load<StyleSheet>("Alchemy/Alchemy-Group-Styles");
        private readonly StyleSheet _highlightStyleSheet = Resources.Load<StyleSheet>("Alchemy/Highlight-Styles");
        
        private readonly VisualElement _header;
        private readonly VisualElement _body;
        
        public virtual VisualElement Header => _header;
        public override VisualElement contentContainer => _body;

        protected VisualElement inlineContainer;
        private List<IInlineElement> _inlineElements = new();
        
        #region Uxml Attributes
        
        private bool _showHeader = true;
        [UxmlAttribute]
        public bool ShowHeader
        {
            get => _showHeader;
            set
            {
                _showHeader = value;
                UpdateStyles();
            }
        }
        
        private bool _showBody = true;
        [UxmlAttribute]
        public bool ShowBody
        {
            get => _showBody;
            set
            {
                _showBody = value;
                UpdateStyles();
            }
        }
        
        private GroupStyle _style = GroupStyle.Default;
        [UxmlAttribute("group-style")]
        public GroupStyle Style
        {
            get => _style;
            set
            {
                _style = value;
                UpdateStyles();
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
                UpdateStyles();
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
                UpdateStyles();
            }
        }
        
        private TintColor _tintColor = Inspector.TintColor.Active;
        [UxmlAttribute]
        public TintColor TintColor
        {
            get => _tintColor;
            set
            {
                _tintColor = value;
                UpdateStyles();
            }
        }
        
        #endregion Uxml Attributes
        
        public AlchemyGroupBase()
        {
            styleSheets.Add(_baseStyleSheet);
            styleSheets.Add(_groupStyleSheet);
            styleSheets.Add(_highlightStyleSheet);
            
            AddToClassList("alchemy-group");
            
            _header = new VisualElement { name = "header" };
            _header.AddToClassList("alchemy-group__header");
            hierarchy.Add(_header);
            
            _body = new VisualElement { name = "body" };
            _body.AddToClassList("alchemy-group__body");
            hierarchy.Add(_body);
            
            RegisterCallback<GeometryChangedEvent>(OnGeometryChanged);
        }

        private void OnGeometryChanged(GeometryChangedEvent evt)
        {
            foreach (var foldout in _body.Query<Foldout>().Build())
            {
                Toggle toggle = foldout.Q<Toggle>();
                toggle.style.marginLeft = 0;
            }

            foreach (AlchemyPropertyField field in this.Query<AlchemyPropertyField>().Build())
                field.AlignField(true, false);
        }

        protected virtual void UpdateStyles()
        {
            EnableInClassList("alchemy-group--boxed", _style == GroupStyle.Boxed);
            EnableInClassList("alchemy-group--body-none", _bodyStyle == BodyStyle.None);
            
            _header.EnableInClassList("alchemy-group__header--boxed", _style == GroupStyle.Boxed);
            _header.EnableInClassList("alchemy-group__header--show-header-none", !ShowHeader);
            _header.EnableInClassList("alchemy-group__header--show-body-none", !ShowBody);
            _header.EnableInClassList("alchemy-group__header--boxed--show-header-none", !ShowHeader && _style == GroupStyle.Boxed);
            _header.EnableInClassList("alchemy-group__header--boxed--show-body-none", !ShowBody && _style == GroupStyle.Boxed);
            
            _body.EnableInClassList("alchemy-group__body--boxed", _style == GroupStyle.Boxed);
            _body.EnableInClassList("alchemy-group__body--show-header-none", !ShowHeader);
            _body.EnableInClassList("alchemy-group__body--show-body-none", !ShowBody);
            _body.EnableInClassList("alchemy-group__body--boxed--show-header-none", !ShowHeader && _style == GroupStyle.Boxed);
            _body.EnableInClassList("alchemy-group__body--boxed--show-body-none", !ShowBody && _style == GroupStyle.Boxed);
            
            TintColor[] colors = (TintColor[])Enum.GetValues(typeof(TintColor));
            foreach (TintColor color in colors)
            {
                EnableInClassList($"highlight-color-{color.ToString().ToLower()}-background", _bodyStyle == BodyStyle.Tint && _tintColor == color);
                _header.EnableInClassList($"highlight-color-{color.ToString().ToLower()}-title", _headerStyle == HeaderStyle.Tint && _tintColor == color);
            }
        }
        
        public void ClearInlines()
        {
            _inlineElements.Clear();
        }
    }
}