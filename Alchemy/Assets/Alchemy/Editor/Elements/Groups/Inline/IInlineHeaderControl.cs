
namespace Alchemy.Editor.Elements
{
    public interface IInlineHeaderControl
    {
        void AddInline(IInlineElement inlineElement);
        void RemoveInline(IInlineElement inlineElement);
        void ClearInlines();
    }
}