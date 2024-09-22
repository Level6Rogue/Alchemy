using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace Alchemy.Editor.Elements
{
    [UxmlElement]
    public partial class AlchemyInlineButton : Button, IInlineElement
    {
        private readonly StyleSheet _styleSheet = Resources.Load<StyleSheet>("Alchemy/Inline-Button-Styles");
        
        VisualElement IInlineElement.DefaultParent { get; set; }
        IInlineHeaderControl IInlineElement.InlineHeaderControl { get; set; }
        VisualElement IInlineElement.InlineTarget { get; set; }
        
        public AlchemyInlineButton() => Setup();
        public AlchemyInlineButton(Action clickEvent) : base(clickEvent) => Setup();
        public AlchemyInlineButton(Background iconImage, Action clickEvent = null) : base(iconImage, clickEvent) => Setup();

        void Setup()
        {
            styleSheets.Add(_styleSheet);
            
            AddToClassList("alchemy-inline-button");
            
            RegisterCallback<AttachToPanelEvent>(_ => this.MoveToInline());
        }

        void IInlineElement.SetInlineTarget(VisualElement inlineParent) => this.AddInlineElement(inlineParent);
        
        
    }
}