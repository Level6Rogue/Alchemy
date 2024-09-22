using Alchemy.Inspector;
using UnityEngine.UIElements;

namespace Alchemy.Editor.Elements
{
    [UxmlElement]
    public partial class AlchemyGroup : AlchemyGroupBase
    {
        private readonly AlchemyTitle _titleElement;
        
        private string _title = "Group Box";
        [UxmlAttribute]
        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                UpdateTitle();
            }
        }
        
        private string _subtitle = "";
        [UxmlAttribute]
        public string SubTitle
        {
            get => _subtitle;
            set
            {
                _subtitle = value;
                UpdateTitle();
            }
        }
        
        private bool _showTitleSeparator = false;
        [UxmlAttribute]
        public bool ShowTitleSeparator
        {
            get => _showTitleSeparator;
            set
            {
                _showTitleSeparator = value;
                UpdateTitle();
            }
        }

        public AlchemyGroup() : base()
        {
            _titleElement = new AlchemyTitle();
            _titleElement.AddToClassList("alchemy-group__title");
            Header.Add(_titleElement);
            
            UpdateTitle();
        }

        private void UpdateTitle()
        {
            if (string.IsNullOrEmpty(Title) && string.IsNullOrEmpty(SubTitle))
            {
                ShowHeader = false;
            }
            else
            {
                ShowHeader = true;
                _titleElement.Title = Title;
                _titleElement.SubTitle = SubTitle;
                _titleElement.ShowTitleSeparator = ShowTitleSeparator && Style != GroupStyle.Boxed;
            }
            
            UpdateStyles();
        }
        
        protected override void UpdateStyles()
        {
            base.UpdateStyles();
            
            _titleElement.EnableInClassList("alchemy-group__title--boxed", Style == GroupStyle.Boxed);
            
            Header.EnableInClassList("alchemy-group__header--boxed--hide-header", Style == GroupStyle.Boxed && !ShowTitleSeparator);
        }
    }
}