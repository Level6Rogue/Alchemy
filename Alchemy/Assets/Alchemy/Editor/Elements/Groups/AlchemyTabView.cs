using System;
using Alchemy.Inspector;
using UnityEngine;
using UnityEngine.UIElements;

namespace Alchemy.Editor.Elements
{

    [UxmlElement]
    public partial class AlchemyTabView : TabView
    {
        private readonly StyleSheet _baseStyleSheet = Resources.Load<StyleSheet>("Alchemy/Custom-Variables-Dark");
        private readonly StyleSheet _tabStyleSheet = Resources.Load<StyleSheet>("Alchemy/Alchemy-Tab-Group-Styles");
        private readonly StyleSheet _highlightStyleSheet = Resources.Load<StyleSheet>("Alchemy/Highlight-Styles");
        
        private VisualElement _headerContainer;
        private AlchemyGroupBase _groupBase;


        #region Uxml Attributes
        
        private GroupStyle _style = GroupStyle.Default;
        [UxmlAttribute("tab-group-style")]
        public GroupStyle Style
        {
            get => _style;
            set
            {
                _style = value;
                UpdateStyle();
            }
        }

        private bool _expandTabs = true;
        private VisualElement _contentContainer;

        [UxmlAttribute]
        public bool ExpandTabs
        {
            get => _expandTabs;
            set
            {
                _expandTabs = value;
                UpdateStyle();
            }
        }
        
        private HeaderStyle _tabsStyle = HeaderStyle.Default;
        [UxmlAttribute("tabs-style")]
        public HeaderStyle TabsStyle
        {
            get => _tabsStyle;
            set
            {
                _tabsStyle = value;
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
     
        
        private TintColor _tintColor = Inspector.TintColor.Active;
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

        public AlchemyTabView()
        {
            styleSheets.Add(_baseStyleSheet);
            styleSheets.Add(_tabStyleSheet);
            styleSheets.Add(_highlightStyleSheet);

            _headerContainer = this.Q("unity-tab-view__header-container");
            _contentContainer = this.Q("unity-tab-view__content-container");

            _groupBase = new AlchemyGroupBase()
            {
                ShowHeader = false
            };
            
            _groupBase.styleSheets.Add(_tabStyleSheet);
            
            _groupBase.AddToClassList("alchemy-tab-view__group-base");
            
            hierarchy.Add(_groupBase);
            _groupBase.Add(_contentContainer);
            
            AddToClassList("alchemy-tab-view");
            
            _contentContainer.AddToClassList("alchemy-tab-view__content-container");
            
            _headerContainer.RegisterCallback<GeometryChangedEvent>(HeaderGeometryChanged);
            
            UpdateStyle();
        }

        private void HeaderGeometryChanged(GeometryChangedEvent evt) => UpdateTabStyles();

        private void UpdateStyle()
        {
            _groupBase.Style = _style;
            _groupBase.BodyStyle = _bodyStyle;
            _groupBase.TintColor = _tintColor;
            
            _groupBase.EnableInClassList("alchemy-tab-view__group-base--boxed", _style == GroupStyle.Boxed);
            EnableInClassList("alchemy-tab-view--boxed", _style == GroupStyle.Boxed);
            
            _headerContainer.EnableInClassList("alchemy-tab-view__header-container--expanded-tabs", _expandTabs);
            _headerContainer.EnableInClassList("alchemy-tab-view__header-container--normal-tabs", !_expandTabs);
            _contentContainer.EnableInClassList("alchemy-tab-view__content-container--boxed", _style == GroupStyle.Boxed);
            _headerContainer.EnableInClassList("alchemy-tab-view__header-container--boxed", _style == GroupStyle.Boxed);
            _contentContainer.EnableInClassList("alchemy-tab-view__content-container--background", _bodyStyle == BodyStyle.Default);
            _contentContainer.EnableInClassList("alchemy-tab-view__content-container--none", _bodyStyle == BodyStyle.None);

            UpdateTabStyles();
        }

        private void UpdateTabStyles()
        {
            for (int i = 0; i < _headerContainer.childCount; i++)
            {
                VisualElement tabElement = _headerContainer[i];
                tabElement.AddToClassList("alchemy-tab__header");
                tabElement.EnableInClassList("alchemy-tab__header--boxed", _style == GroupStyle.Boxed);
                
                tabElement.EnableInClassList("alchemy-unity-tab__header--first", i == 0);
                tabElement.EnableInClassList("alchemy-unity-tab__header--last", i == _headerContainer.childCount - 1);
                
                TintColor[] colors = (TintColor[])Enum.GetValues(typeof(TintColor));
                foreach (TintColor color in colors)
                {
                    tabElement.EnableInClassList($"highlight-color-{color.ToString().ToLower()}-button", _tabsStyle == HeaderStyle.Tint && _tintColor == color);
                }
            }
        }
    }
}