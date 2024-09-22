using UnityEngine.UIElements;

namespace Alchemy.Editor.Elements
{
    public interface IInlineElement
    {
        IInlineHeaderControl InlineHeaderControl { get; set; }
        VisualElement DefaultParent { get; set; }
        VisualElement InlineTarget { get; set; }
        void SetInlineTarget(VisualElement inlineParent);
    }
}