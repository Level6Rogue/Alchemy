using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UIElements;
using UnityEditor;
using Alchemy.Inspector;
using Alchemy.Editor.Elements;
using Tweaks;
using UnityEngine;

namespace Alchemy.Editor.Drawers
{
    [CustomGroupDrawer(typeof(GroupAttribute))]
    public sealed class GroupDrawer : AlchemyGroupDrawer
    {
        private AlchemyGroup _rootElement;

        public override VisualElement CreateRootElement(string label)
        {
            GroupDefaultStyles defaults = AlchemySettings.GetOrCreateSettings().DefaultGroupStyle;
            
            _rootElement = new AlchemyGroup
            {
                Title = label,
                Style = defaults.groupStyle,
                ShowTitleSeparator = defaults.seperateHeader,
                HeaderStyle = defaults.headerStyle,
                BodyStyle = defaults.bodyStyle,
                TintColor = defaults.tintColor,
            };
            return _rootElement;
        }

        public override VisualElement GetGroupElement(Attribute attribute)
        {
            if (attribute is not GroupAttribute groupAttribute) 
                return base.GetGroupElement(attribute);
            
            if (!string.IsNullOrEmpty(groupAttribute.Title)) _rootElement.Title = groupAttribute.Title;
            if (!string.IsNullOrEmpty(groupAttribute.Subtitle)) _rootElement.SubTitle = groupAttribute.Subtitle;
            if (groupAttribute.ShowTitleSeparator != ShowTitleSeparator.Unset) _rootElement.ShowTitleSeparator = groupAttribute.ShowTitleSeparator == ShowTitleSeparator.Show;
            
            if (groupAttribute.Style.GetGroupStyle(out GroupStyle groupStyle)) _rootElement.Style = groupStyle;
            if (groupAttribute.Style.GetHeaderStyle(out HeaderStyle headerStyle)) _rootElement.HeaderStyle = headerStyle;
            if (groupAttribute.Style.GetBodyStyle(out BodyStyle bodyStyle)) _rootElement.BodyStyle = bodyStyle;
            if (groupAttribute.Color != 0) _rootElement.TintColor = groupAttribute.Color;

            return base.GetGroupElement(attribute);
        }
    }

    [CustomGroupDrawer(typeof(BoxGroupAttribute))]
    public sealed class BoxGroupDrawer : AlchemyGroupDrawer
    {
        private AlchemyGroup _rootElement;

        public override VisualElement CreateRootElement(string label)
        {
            BoxGroupDefaultStyles defaults = AlchemySettings.GetOrCreateSettings().DefaultBoxGroupStyle;
            
            _rootElement = new AlchemyGroup
            {
                Title = label,
                Style = defaults.groupStyle,
                ShowTitleSeparator = defaults.seperateHeader,
                HeaderStyle = defaults.headerStyle,
                BodyStyle = defaults.bodyStyle,
                TintColor = defaults.tintColor,
            };
            return _rootElement;
        }

        public override VisualElement GetGroupElement(Attribute attribute)
        {
            if (attribute is not BoxGroupAttribute groupAttribute) 
                return base.GetGroupElement(attribute);
            
            if (!string.IsNullOrEmpty(groupAttribute.Title)) _rootElement.Title = groupAttribute.Title;
            if (!string.IsNullOrEmpty(groupAttribute.Subtitle))  _rootElement.SubTitle = groupAttribute.Subtitle;
            if (groupAttribute.ShowTitleSeparator != ShowTitleSeparator.Unset) _rootElement.ShowTitleSeparator = groupAttribute.ShowTitleSeparator == ShowTitleSeparator.Show;
            
            if (groupAttribute.Style.GetGroupStyle(out GroupStyle groupStyle)) _rootElement.Style = groupStyle;
            if (groupAttribute.Style.GetHeaderStyle(out HeaderStyle headerStyle)) _rootElement.HeaderStyle = headerStyle;
            if (groupAttribute.Style.GetBodyStyle(out BodyStyle bodyStyle)) _rootElement.BodyStyle = bodyStyle;
            if (groupAttribute.Color != 0) _rootElement.TintColor = groupAttribute.Color;
            
            return base.GetGroupElement(attribute);
        }
    }

    [CustomGroupDrawer(typeof(TabGroupAttribute))]
    public sealed class TabGroupDrawer : AlchemyGroupDrawer
    {
        private AlchemyTabView _rootElement;
        readonly Dictionary<string, VisualElement> tabElements = new();

        string[] keyArrayCache = new string[0];
        int tabIndex;
        int prevTabIndex;

        sealed class TabItem
        {
            public string name;
            public VisualElement element;
        }

        public override VisualElement CreateRootElement(string label)
        {
            var configKey = UniqueId + "_TabGroup";
            int.TryParse(EditorUserSettings.GetConfigValue(configKey), out tabIndex);
            prevTabIndex = tabIndex;

            TabGroupDefaultStyles defaults = AlchemySettings.GetOrCreateSettings().DefaultTabGroupStyle;

            _rootElement = new AlchemyTabView
            {
                Style = defaults.groupStyle,
                TabsStyle = defaults.headerStyle,
                BodyStyle = defaults.bodyStyle,
                TintColor = defaults.tintColor,
                ExpandTabs = defaults.expandTabs
            };

            return _rootElement;
        }

        public override VisualElement GetGroupElement(Attribute attribute)
        {
            TabGroupAttribute tabGroupAttribute = (TabGroupAttribute)attribute;

            var tabName = tabGroupAttribute.TabName;
            if (!tabElements.TryGetValue(tabName, out var element))
            {
                element = new Tab
                {
                    label = tabName
                };
                
                _rootElement.Add(element);
                tabElements.Add(tabName, element);

                keyArrayCache = tabElements.Keys.ToArray();
            }

            if (tabGroupAttribute.Style.GetHeaderStyle(out HeaderStyle headerStyle))_rootElement.TabsStyle = headerStyle;
            if (tabGroupAttribute.Style.GetBodyStyle(out BodyStyle bodyStyle)) _rootElement.BodyStyle = bodyStyle;
            if (tabGroupAttribute.Color != 0) _rootElement.TintColor = tabGroupAttribute.Color;
            
            // if (tabGroupAttribute.Style.ExpandTabs != null) _rootElement.ExpandTabs = tabGroupAttribute.Style.ExpandTabs == ExpandTabs.Expand;

            return element;
        }
    }

    [CustomGroupDrawer(typeof(FoldoutGroupAttribute))]
    public sealed class FoldoutGroupDrawer : AlchemyGroupDrawer
    {
        private AlchemyFoldout _rootElement;

        public override VisualElement CreateRootElement(string label)
        {
            var configKey = UniqueId + "_FoldoutGroup";
            bool.TryParse(EditorUserSettings.GetConfigValue(configKey), out var result);

            FoldoutGroupDefaultStyles defaults = AlchemySettings.GetOrCreateSettings().DefaultFoldoutGroupStyle;

            _rootElement = new AlchemyFoldout
            {
                Style = defaults.groupStyle,
                HeaderStyle = defaults.headerStyle,
                BodyStyle = defaults.bodyStyle,
                TintColor = defaults.tintColor,
                text = label,
                value = result
            };

            _rootElement.RegisterValueChangedCallback(x =>
            {
                EditorUserSettings.SetConfigValue(configKey, x.newValue.ToString());
            });

            return _rootElement;
        }

        public override VisualElement GetGroupElement(Attribute attribute)
        {
            FoldoutGroupAttribute groupAttribute = (FoldoutGroupAttribute)attribute;

            if (groupAttribute.GroupStyle != GroupStyle.Unset) _rootElement.Style = groupAttribute.GroupStyle;
            if (groupAttribute.HeaderStyle != HeaderStyle.Unset) _rootElement.HeaderStyle = groupAttribute.HeaderStyle;
            if (groupAttribute.BodyStyle != BodyStyle.Unset) _rootElement.BodyStyle = groupAttribute.BodyStyle;
            if (groupAttribute.TintColor != TintColor.Active) _rootElement.TintColor = groupAttribute.TintColor;
            
            return base.GetGroupElement(attribute);
        }
    }

    [CustomGroupDrawer(typeof(HorizontalGroupAttribute))]
    public sealed class HorizontalGroupDrawer : AlchemyGroupDrawer
    {
        public override VisualElement CreateRootElement(string label)
        {
            return new AlchemyHorizontalGroup();
        }
    }
    [CustomGroupDrawer(typeof(InlineGroupAttribute))]
    public sealed class InlineGroupDrawer : AlchemyGroupDrawer
    {
        public override VisualElement CreateRootElement(string label)
        {
            return new VisualElement();
        }
    }
}