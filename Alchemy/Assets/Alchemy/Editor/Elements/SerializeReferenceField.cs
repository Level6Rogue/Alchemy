using System;
using System.Linq;
using Alchemy.Inspector;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UIElements;

namespace Alchemy.Editor.Elements
{
    /// <summary>
    /// Draw properties marked with SerializeReference attribute
    /// </summary>
    [UxmlElement]
    public sealed partial class SerializeReferenceField : AlchemyFoldout
    {
        private readonly StyleSheet _styleSheet = Resources.Load<StyleSheet>("Elements/SerializeReferenceField-Styles");
        
        private readonly Button _pickReferenceButton;

        private SerializedProperty _property;

        public SerializeReferenceField()
        {
            styleSheets.Add(_styleSheet);
            
            AddToClassList("alchemy-serialize-reference__foldout");
            contentContainer.AddToClassList("alchemy-serialize-reference__content");

            _pickReferenceButton = new Button(OnClick)
            {
                text = "null()",
                iconImage = (Background)EditorIcons.CsScriptIcon.image
            };
            
            _pickReferenceButton.AddToClassList("alchemy-serialize-reference__pick-reference-button");

            GroupBase.Header.Add(_pickReferenceButton);
            
            schedule.Execute(() =>
            {
                RegisterCallback<GeometryChangedEvent>(_ => SetWidth());
                SetWidth();
                
                void SetWidth()
                {
                    _pickReferenceButton.style.width = GUIHelper.CalculateFieldWidth(_pickReferenceButton, panel.visualTree) -
                                                       (_pickReferenceButton.GetFirstAncestorOfType<Foldout>() != null ? 18f : 0f);
                }
            });
        }
        
        public SerializeReferenceField(SerializedProperty property) : this() => BindField(property);

        public void BindField(SerializedProperty property)
        {
            _property = property;
            
            Assert.IsTrue(property.propertyType == SerializedPropertyType.ManagedReference);

            text = ObjectNames.NicifyVariableName(property.displayName);
            
            this.BindProperty(property);
            
            Rebuild(property);
        }
        
        private void OnClick()
        {
            if (_property == null) 
                return;
            
            const int MaxTypePopupLineCount = 13;
            
            var baseType = _property.GetManagedReferenceFieldType();
            SerializeReferenceDropdown dropdown = new(
                TypeCache.GetTypesDerivedFrom(baseType).Append(baseType).Where(t =>
                    (t.IsPublic || t.IsNestedPublic) &&
                    !t.IsAbstract &&
                    !t.IsGenericType &&
                    !typeof(UnityEngine.Object).IsAssignableFrom(t) &&
                    t.IsSerializable
                ),
                MaxTypePopupLineCount,
                new AdvancedDropdownState()
            );
            
            dropdown.onItemSelected += item =>
            {
                _property.SetManagedReferenceType(item.type);
                _property.isExpanded = true;
                _property.serializedObject.ApplyModifiedProperties();
                _property.serializedObject.Update();
            
                Rebuild(_property);
            };

            dropdown.Show(_pickReferenceButton.worldBound);
        }

        /// <summary>
        /// Rebuild child elements
        /// </summary>
        void Rebuild(SerializedProperty property)
        {
            try
            {
                if (_property != null)
                {
                    string typeName = _property.managedReferenceValue == null
                        ? "Null"
                        : _property.managedReferenceValue.GetType().Name;

                    _pickReferenceButton.text = $"{typeName} ({_property.GetManagedReferenceFieldTypeName()})";
                }
            }
            catch (InvalidOperationException)
            {
                // Ignoring exceptions when disposed (bad solution)
                return;
            }
            
            Clear();

            if (property.managedReferenceValue == null)
            {
                HelpBox helpbox = new("No type assigned.", HelpBoxMessageType.Info);

                helpbox.AddToClassList("alchemy-serialize-reference__helpbox");
                helpbox.EnableInClassList("alchemy-serialize-reference__helpbox--boxed", Style == GroupStyle.Boxed);

                Add(helpbox);
            }
            else
            {
                InspectorHelper.BuildElements(property.serializedObject, this, property.managedReferenceValue,
                    x => property.FindPropertyRelative(x));
            }

            this.Bind(property.serializedObject);
        }
    }
}