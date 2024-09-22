using System;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using Alchemy.Inspector;

namespace Alchemy.Editor.Elements
{
    /// <summary>
    /// Visual Element that draws properties based on Alchemy attributes
    /// </summary>
    [UxmlElement]
    public sealed partial class AlchemyPropertyField : BindableElement
    {
        private VisualElement element;
        public VisualElement FieldElement => element;

        public AlchemyPropertyField()
        {
            Label label = new Label("Alchemy Property Field (Unbound)");
            Add(label);
        }

        public AlchemyPropertyField(SerializedProperty property, Type type, bool isArrayElement = false) => BindField(property, type, isArrayElement);
        
        public void BindField(SerializedProperty property, Type type, bool isArrayElement = false)
        {
            Clear();
            
            var labelText = ObjectNames.NicifyVariableName(property.displayName);

            switch (property.propertyType)
            {
                // NOTE: RectOffset is a generic property type, but it doesn't have a SerializeField. Instead, use PropertyField.
                case SerializedPropertyType.Generic when property.type == "RectOffset":
                default:
                    element = new PropertyField(property);
                    break;
                case SerializedPropertyType.ObjectReference:
                    if (property.GetAttribute<InlineEditorAttribute>() != null)
                    {
                        element = new InlineEditorObjectField(property, type);
                    }
                    else
                    {
                        element = GUIHelper.CreateObjectPropertyField(property, type);
                    }
                    break;
                case SerializedPropertyType.Generic:
                    var targetType = property.GetPropertyType(isArrayElement);
                    var isManagedReferenceProperty = property.propertyType == SerializedPropertyType.ManagedReference;

                    if (InternalAPIHelper.GetDrawerTypeForType(targetType, isManagedReferenceProperty) != null)
                    {
                        element = new PropertyField(property);
                    }
                    else if (property.isArray)
                    {
                        element = new PropertyListView(property);
                    }
                    else if (targetType.TryGetCustomAttribute<PropertyGroupAttribute>(out var groupAttribute)) // custom group
                    {
                        var drawer = AlchemyEditorUtility.CreateGroupDrawer(groupAttribute, targetType);

                        var root = drawer.CreateRootElement(labelText);
                        InspectorHelper.BuildElements(property.serializedObject, root, property.GetValue<object>(), name => property.FindPropertyRelative(name));
                        if (root is BindableElement bindableElement) bindableElement.BindProperty(property);
                        element = root;
                    }
                    else
                    {
                        targetType.TryGetCustomAttribute<PropertyFieldStyleAttribute>(out PropertyFieldStyleAttribute classStyleAttribute);

                        element = InspectorHelper.CreateClassFoldout(labelText, property, classStyleAttribute);
                    }
                    break;
                case SerializedPropertyType.ManagedReference:
                    
                    SerializeReferenceDefaultStyles defaults = AlchemySettings.GetOrCreateSettings().DefaultSerializeReferenceStyle;
                    element = new SerializeReferenceField(property);
                    
                    // ((element) as SerializeReferenceField).Style = defaults.groupStyle;
                    // ((element) as SerializeReferenceField).text = labelText;
                    // ((element) as SerializeReferenceField).HeaderStyle = defaults.headerStyle;
                    // ((element) as SerializeReferenceField).BodyStyle = defaults.bodyStyle;
                    // ((element) as SerializeReferenceField).TintColor = defaults.tintColor;
                    
                    break;
            }
            
            Add(element);
        }

        public string Label
        {
            get
            {
                return element switch
                {
                    InlineEditorObjectField inlineEditorObjectField => inlineEditorObjectField.Label,
                    SerializeReferenceField serializeReferenceField => serializeReferenceField.text,
                    Foldout foldout => foldout.text,
                    PropertyField propertyField => propertyField.label,
                    PropertyListView propertyListView => propertyListView.Label,
                    _ => null,
                };
            }
            set
            {
                switch (element)
                {
                    case InlineEditorObjectField inlineEditorObjectField:
                        inlineEditorObjectField.Label = value;
                        break;
                    case SerializeReferenceField serializeReferenceField:
                        serializeReferenceField.text = value;
                        break;
                    case Foldout foldout:
                        foldout.text = value;
                        break;
                    case PropertyField propertyField:
                        propertyField.label = value;
                        break;
                    case PropertyListView propertyListView:
                        propertyListView.Label = value;
                        break;
                }
            }
        }

        public void AlignField(bool removeAlignmentClasses, bool modifyForHorizontalGroup)
        {
            FieldElement.RegisterCallback<GeometryChangedEvent>(_ => ModifyPropertyField());
            
            ModifyPropertyField();
            
            void ModifyPropertyField()
            {
                if (FieldElement is PropertyField field)
                {
                    if (modifyForHorizontalGroup)
                        field.AddToClassList("property-field--horizontal-grouped");
                    
                    if (field.childCount == 0 || !removeAlignmentClasses)
                        return;
                    
                    VisualElement innerField = field[0];
                    innerField.RemoveFromClassList("unity-base-field__inspector-field");
                    innerField.RemoveFromClassList("unity-base-field__aligned");
                }

                Label label = this.Q<Label>();

                if (removeAlignmentClasses)
                    label.RemoveFromClassList("unity-base-field__label");

                if (modifyForHorizontalGroup)
                {
                    label.style.width = new StyleLength(StyleKeyword.Auto);
                    label.style.minWidth = new StyleLength(new Length(30, LengthUnit.Percent));
                }
            }
        }
    }
}