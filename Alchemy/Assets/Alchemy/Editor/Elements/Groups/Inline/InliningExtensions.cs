using UnityEngine.UIElements;

namespace Alchemy.Editor.Elements
{
    public static class InliningExtensions
    {
        public static void MoveToInline<T>(this T inlineElement) where T : VisualElement, IInlineElement
        {
            if (inlineElement.parent == inlineElement.InlineTarget)
                return;

            inlineElement.DefaultParent = inlineElement.parent;
            IInlineHeaderControl inlineHeaderControl = inlineElement.GetFirstAncestorOfType<IInlineHeaderControl>();

            if (inlineHeaderControl == null || inlineHeaderControl != inlineElement.InlineHeaderControl)
            {
                inlineElement.InlineHeaderControl?.RemoveInline(inlineElement);
            }
            
            if (inlineHeaderControl is VisualElement ve)
            {
                inlineElement.InlineHeaderControl = inlineHeaderControl;
                inlineHeaderControl.AddInline(inlineElement);
            }
        }
        
        public static void AddInlineElement<T>(this T inlineableElement, VisualElement inlineParent) where T : VisualElement, IInlineElement
        {
            if (inlineableElement.DefaultParent == null)
                inlineableElement.DefaultParent = inlineableElement.parent;
            
            inlineableElement.InlineTarget = inlineParent;
            inlineParent.Add(inlineableElement as VisualElement);
        }
    }
}