using System;
using UnityEngine;

namespace Alchemy.Inspector
{
    [Serializable]
    public enum GroupStyle
    {
        Unset,
        Default, 
        Boxed
    }
        
    [Serializable]
    public enum HeaderStyle
    {
        Unset,
        Default, 
        Tint
    }
        
    [Serializable]
    public enum BodyStyle
    {
        Unset,
        None,
        Default, 
        Tint
    }
    
    [Serializable]
    public enum BackgroundStyle
    {
        Unset,
        None,
        Default,
        Tint
    }

    public enum ShowTitleSeparator
    {
        Unset,
        Show,
        Hide
    }
    
    public enum ExpandTabs
    {
        Unset,
        Expand,
        Shrink
    }
    
    public enum TintColor
    {
        Active = 1 << 1,
        Red = 1 << 2,
        Green = 1 << 3,
        Blue = 1 << 4,
        Cyan = 1 << 5,
        Yellow = 1 << 6,
        Orange = 1 << 7,
        Purple = 1 << 8,
        Pink = 1 << 9,
        Brown = 1 << 10,
        Gray = 1 << 11, 
    }

    [Flags]
    public enum Style
    {
        Default = 1 << 1,
        Boxed = 1 << 2,
        
        Tint_None = 1 << 3,
        Tint_Header = 1 << 4,
        Tint_Body = 1 << 5,
        Body_Default = 1 << 6,
        Body_None = 1 << 7,

        Tint_All = Tint_Header | Tint_Body
    }
    
    public static class StyleExtensions
    {
        public static bool GetGroupStyle(this Style value, out GroupStyle groupStyle)
        {
            if (value.HasFlag(Style.Boxed))
            {
                groupStyle = GroupStyle.Boxed;
                return true;
            }
            
            if (value.HasFlag(Style.Default))
            {
                groupStyle = GroupStyle.Default;
                return true;
            }
            
            groupStyle = GroupStyle.Default;
            return false;
        }
        
        public static bool GetHeaderStyle(this Style value, out HeaderStyle headerStyle)
        {
            if (value.HasFlag(Style.Tint_Header))
            {
                headerStyle = HeaderStyle.Tint;
                return true;
            }

            headerStyle = HeaderStyle.Default;
            return false;
        }
        
        public static bool GetBodyStyle(this Style value, out BodyStyle bodyStyle)
        {
            if (value.HasFlag(Style.Tint_Body))
            {
                bodyStyle = BodyStyle.Tint;
                return true;
            }
            
            if (value.HasFlag(Style.Body_None))
            {
                bodyStyle = BodyStyle.None;
                return true;
            }
            
            if (value.HasFlag(Style.Body_Default))
            {
                bodyStyle = BodyStyle.Default;
                return true;
            }

            bodyStyle = BodyStyle.Default;
            return false;
        }
    }
}