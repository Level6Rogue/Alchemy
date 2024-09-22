using UnityEngine.UIElements;

namespace Alchemy.Editor.Elements
{
    [UxmlElement]
    public partial class AlchemyInlineContainer : VisualElement, IInlineElement
    {
        public VisualElement DefaultParent { get; set; }
        IInlineHeaderControl IInlineElement.InlineHeaderControl { get; set; }
        VisualElement IInlineElement.InlineTarget { get; set; }

        public AlchemyInlineContainer()
        {
            RegisterCallback<AttachToPanelEvent>(_ => this.MoveToInline());
        }

        void IInlineElement.SetInlineTarget(VisualElement inlineParent) => this.AddInlineElement(inlineParent);
    }
}