using System;
using System.Collections;
using Alchemy.Inspector;
using UnityEngine;
using UnityEngine.UIElements;

namespace Alchemy.Editor.Elements
{
    [UxmlElement]
    public partial class AlchemyListView : ListView
    {
        private readonly StyleSheet _baseStyleSheet = Resources.Load<StyleSheet>("Alchemy/Custom-Variables-Dark");
        private readonly StyleSheet _styleSheet = Resources.Load<StyleSheet>("Elements/AlchemyListView-Styles");
        private readonly StyleSheet _highlightStyleSheet = Resources.Load<StyleSheet>("Alchemy/Highlight-Styles");
        
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

        public AlchemyListView()
        {
            // styleSheets.Add(_baseStyleSheet);
            // styleSheets.Add(_styleSheet);
            // styleSheets.Add(_highlightStyleSheet);
            
            //AddToClassList("alchemy-list-view");

            //UpdateStyle();
        }

        public AlchemyListView(IList itemsSource, float itemHeight = -1, Func<VisualElement> makeItem = null, Action<VisualElement, int> bindItem = null) : base(itemsSource, itemHeight, makeItem, bindItem)
        {
            // styleSheets.Add(_baseStyleSheet);
            // styleSheets.Add(_styleSheet);
            // styleSheets.Add(_highlightStyleSheet);
            
            //AddToClassList("alchemy-list-view");

            //UpdateStyle();
        }

        private void UpdateStyle()
        {
            // Foldout foldout = this.Q<Foldout>("unity-list-view__foldout-header");
            // if (foldout != null)
            // {
            //     Toggle toggle = foldout.Q<Toggle>();
            //     toggle?.EnableInClassList("alchemy-list-view__foldout-header--boxed", _style == GroupStyle.Boxed);
            //
            //     ScrollView view = this.Q<ScrollView>();
            //     view?.EnableInClassList("alchemy-list-view__scroll-view--boxed", _style == GroupStyle.Boxed);
            //     
            //     TintColor[] colors = (TintColor[])Enum.GetValues(typeof(TintColor));
            //     foreach (TintColor color in colors)
            //     {
            //         view?.EnableInClassList($"highlight-color-{color.ToString().ToLower()}-background", _bodyStyle == BodyStyle.Tint && _tintColor == color);
            //         toggle?.EnableInClassList($"highlight-color-{color.ToString().ToLower()}-title", _headerStyle == HeaderStyle.Tint && _tintColor == color);
            //     }
            // }
            //
            // TextField sizeField = this.Q<TextField>("unity-list-view__size-field");
            // sizeField?.EnableInClassList("alchemy-list-view__size-field--boxed", _style == GroupStyle.Boxed);
        }
    }
}