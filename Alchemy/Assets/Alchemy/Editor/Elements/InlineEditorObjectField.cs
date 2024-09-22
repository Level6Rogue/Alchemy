using System;
using Alchemy.Inspector;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

namespace Alchemy.Editor.Elements
{
    /// <summary>
    /// Visual Element that draws the ObjectField of the InlineEditor attribute
    /// </summary>
    [UxmlElement]
    public sealed partial class InlineEditorObjectField : AlchemyFoldout
    {
        private readonly StyleSheet _styleSheet = Resources.Load<StyleSheet>("Elements/InlineEditorObjectField-Styles");
        
        private readonly ObjectField field;
        
        private SerializedProperty _property;
        bool isNull;
        public bool IsObjectNull => isNull;

        public string Label
        {
            get => text;
            set => text = value;
        }
        
        public InlineEditorObjectField()
        {
            styleSheets.Add(_styleSheet);
            
            field = new ObjectField();
            field.AddToClassList("alchemy-inline-editor-object-field__object-field");
            field.RegisterValueChangedCallback(OnFieldValueChanged);
            field.RegisterCallback<GeometryChangedEvent>(_ => field.RemoveFromClassList("unity-base-field__inspector-field"));
            GUIHelper.ScheduleAdjustLabelWidth(field);
            
            GroupBase.Header.Add(field);
        }

        public InlineEditorObjectField(SerializedProperty property, Type type) : this() => BindField(property, type);

        public void BindField(SerializedProperty property, Type type)
        {
            _property = property;
            
            Assert.IsTrue(property.propertyType == SerializedPropertyType.ObjectReference);

            string fieldName = ObjectNames.NicifyVariableName(property.displayName);
            
            text = fieldName;
            this.BindProperty(property);
            
            field.label = fieldName;
            field.objectType = type;
            field.allowSceneObjects = !property.GetFieldInfo().HasCustomAttribute<AssetsOnlyAttribute>();
            field.value = property.objectReferenceValue;
            
            Label fieldLabel = field.Q<Label>();
            fieldLabel.AddToClassList("alchemy-inline-editor-object-field__object-field__label--hidden");
            
            OnPropertyChanged(property);
        }
        
        private void OnFieldValueChanged(ChangeEvent<Object> evt)
        {
            if (_property == null) return;
            
            _property.objectReferenceValue = evt.newValue;
            _property.serializedObject.ApplyModifiedProperties();
            OnPropertyChanged(_property);
        }

        void OnPropertyChanged(SerializedProperty property)
        {
            isNull = property.objectReferenceValue == null;

            Label fieldLabel = field.Q<Label>();
            fieldLabel.text = isNull ? ObjectNames.NicifyVariableName(property.displayName) : string.Empty;
            
            field.pickingMode = PickingMode.Ignore;
            
            var objectField = field.Q<ObjectField>();
            objectField.pickingMode = PickingMode.Ignore;

            var label = objectField.Q<Label>();
            label.pickingMode = PickingMode.Ignore;

            HideFoldout(isNull);
            
            Build(property);
        }

        void Build(SerializedProperty property)
        {
            Clear();

            isNull = property.objectReferenceValue == null;
            if (!isNull)
            {
                var so = new SerializedObject(property.objectReferenceValue);
                InspectorHelper.BuildElements(so, this, so.targetObject, name => so.FindProperty(name));
                this.Bind(so);
            }
            else
            {
                this.Unbind();
            }
        }
    }
}