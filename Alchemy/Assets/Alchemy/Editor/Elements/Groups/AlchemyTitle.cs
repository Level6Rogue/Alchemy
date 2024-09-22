using UnityEngine;
using UnityEngine.UIElements;

namespace Alchemy.Editor.Elements
{
    [UxmlElement]
    public partial class AlchemyTitle : VisualElement
    {
        private readonly StyleSheet _styleSheet = Resources.Load<StyleSheet>("Alchemy/Alchemy-Title-Styles");
        
        private Label _titleLabel;
        private Label _subtitleLabel;
        private VisualElement _titleSeparator;

        #region Uxml Attributes
        
        private string _title = "Title";
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

        private string _subtitle = "Subtitle";
        [UxmlAttribute]
        public string SubTitle
        {
            get => _subtitle;
            set
            {
                _subtitle = value;
                UpdateSubtitle();
            }
        }

        private bool _showTitleSeparator = true;
        [UxmlAttribute]
        public bool ShowTitleSeparator
        {
            get => _showTitleSeparator;
            set
            {
                _showTitleSeparator = value;
                UpdateTitleSeparator();
            }
        }
        
        #endregion Uxml Attributes

        public AlchemyTitle()
        {
            styleSheets.Add(_styleSheet);
            
            AddToClassList("alchemy-title");
        }

        private void UpdateTitle()
        {
            if (string.IsNullOrEmpty(_title))
            {
                _titleLabel?.RemoveFromHierarchy();
                _titleLabel = null;
            }
            else
            {
                if (_titleLabel == null)
                {
                    _titleLabel = new Label(_title) { name = "title" };
                    _titleLabel.AddToClassList("alchemy-title__label");
                    Insert(0, _titleLabel);
                }
                else
                {
                    _titleLabel.text = _title;
                }
            }
        }

        private void UpdateSubtitle()
        {
            if (string.IsNullOrEmpty(_subtitle))
            {
                _subtitleLabel?.RemoveFromHierarchy();
                _subtitleLabel = null;
            }
            else
            {
                if (_subtitleLabel == null)
                {
                    _subtitleLabel = new Label(_subtitle) { name = "subtitle" };
                    _subtitleLabel.AddToClassList("alchemy-title__subtitle__label");
                    Insert(1, _subtitleLabel);
                }
                else
                {
                    _subtitleLabel.text = _subtitle;
                }
            }
        }

        private void UpdateTitleSeparator()
        {
            if (_showTitleSeparator && !string.IsNullOrEmpty(_title))
            {
                if (_titleSeparator == null)
                {
                    _titleSeparator = new VisualElement { name = "title-separator" };
                    _titleSeparator.AddToClassList("alchemy-title__title-separator");
                    Insert(childCount, _titleSeparator);
                }
            }
            else
            {
                _titleSeparator?.RemoveFromHierarchy();
                _titleSeparator = null;
            }
        }   
    }
}