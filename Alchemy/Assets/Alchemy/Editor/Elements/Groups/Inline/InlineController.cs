using System.Collections.Generic;
using UnityEngine.UIElements;

namespace Alchemy.Editor.Elements
{
    public class InlineController
    {
        public VisualElement InlineParent { get; set; }
        
        private List<IInlineElement> _inlineElements = new();

        public void MoveAllInlines(bool moveInline)
        {
            if (InlineParent == null)
                moveInline = false;
            
            foreach (IInlineElement inlineElement in _inlineElements)
            {
                VisualElement target = moveInline ? InlineParent : inlineElement.DefaultParent;

                if (inlineElement is VisualElement visualElement)
                {
                    inlineElement.InlineTarget = InlineParent;
                    target?.Add(visualElement);
                }
            }
        }

        public void Add(IInlineElement inlineElement)
        {
            if (!_inlineElements.Contains(inlineElement))
                _inlineElements.Add(inlineElement);
        }
    }
}