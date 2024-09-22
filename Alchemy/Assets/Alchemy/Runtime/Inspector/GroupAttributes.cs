namespace Alchemy.Inspector
{
    public sealed class GroupAttribute : PropertyGroupAttribute
    {
        public string Title { get; }
        public string Subtitle { get; }
        public Style Style { get; set; } = 0;
        public TintColor Color { get; set; } = 0;
        public ShowTitleSeparator ShowTitleSeparator { get; set; } = ShowTitleSeparator.Hide;
    

        public GroupAttribute() : base() { }
        public GroupAttribute(string groupPath) : base(groupPath) { }
        public GroupAttribute(string groupPath, 
            string title = null,
            string subtitle = null
        ) : base(groupPath)
        {
            Title = title;
            Subtitle = subtitle;
        }
    }

    public sealed class BoxGroupAttribute : PropertyGroupAttribute
    {
        public string Title { get; }
        public string Subtitle { get; }
        public ShowTitleSeparator ShowTitleSeparator { get; set; } = ShowTitleSeparator.Hide;
        
        public Style Style { get; set; } = 0;
        public TintColor Color { get; set; } = 0;

        public BoxGroupAttribute() : base() { }
        public BoxGroupAttribute(string groupPath) : base(groupPath) { }
        public BoxGroupAttribute(string groupPath, 
            string title = null,
            string subtitle = null
        ) : base(groupPath)
        {
            Title = title;
            Subtitle = subtitle;
        }
    }

    public sealed class TabGroupAttribute : PropertyGroupAttribute
    {
        public string TabName { get; }
        public Style Style { get; set; } = Style.Tint_None;
        public TintColor Color { get; set; } = 0;
        
        public TabGroupAttribute(string tabName)
        {
            TabName = tabName;
        }

        public TabGroupAttribute(string groupPath, string tabName) : base(groupPath)
        {
            TabName = tabName;
        }
    }

    public sealed class FoldoutGroupAttribute : PropertyGroupAttribute
    {
        public GroupStyle GroupStyle { get; set; } = GroupStyle.Unset;
        public HeaderStyle HeaderStyle { get; set; } = HeaderStyle.Unset;
        public BodyStyle BodyStyle { get; set; } = BodyStyle.Unset;
        public TintColor TintColor { get; set; } = TintColor.Active;

        public FoldoutGroupAttribute() : base() { }
        public FoldoutGroupAttribute(string groupPath) : base(groupPath) { }
    }

    public sealed class HorizontalGroupAttribute : PropertyGroupAttribute
    {
        public HorizontalGroupAttribute() : base() { }
        public HorizontalGroupAttribute(string groupPath) : base(groupPath) { }
    }

    public sealed class InlineGroupAttribute : PropertyGroupAttribute
    {
        public InlineGroupAttribute() : base() { }
        public InlineGroupAttribute(string groupPath) : base(groupPath) { }
    }
}