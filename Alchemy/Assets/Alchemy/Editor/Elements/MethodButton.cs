using System.Reflection;
using Alchemy.Inspector;
using UnityEditor;
using UnityEngine.UIElements;

namespace Alchemy.Editor.Elements
{
    public sealed class MethodButton : VisualElement
    {
        const string ButtonLabelText = "Invoke";

        private readonly AlchemyFoldout _foldout;
        private readonly Button button;
        
        public MethodButton(object target, MethodInfo methodInfo, bool inline = false)
        {
            var parameters = methodInfo.GetParameters();

            // Create parameterless button
            if (parameters.Length == 0)
            {
                if (inline)
                {
                    Button button = new AlchemyInlineButton(() => methodInfo.Invoke(target, null))
                    {
                        text = methodInfo.Name  
                    };
                    Add(button);
                }
                else
                {
                    button = new Button(() => methodInfo.Invoke(target, null))
                    {
                        text = methodInfo.Name
                    };
                    Add(button);
                }
                
            }
            else
            {
                _foldout = new()
                {
                    text = methodInfo.Name,
                    Style = GroupStyle.Boxed,
                };
                
                Add(_foldout);

                object[] parameterObjects = new object[parameters.Length];
                
                
                AlchemyInlineButton invokeButton = new(() => methodInfo.Invoke(target, parameterObjects))
                {
                    text = ButtonLabelText,
                };
                
                _foldout.GroupBase.Header.Add(invokeButton); 
                
                for (int i = 0; i < parameters.Length; i++)
                {
                    var index = i;
                    var parameter = parameters[index];
                    parameterObjects[index] = TypeHelper.CreateDefaultInstance(parameter.ParameterType);
                    var element = new GenericField(parameterObjects[index], parameter.ParameterType, ObjectNames.NicifyVariableName(parameter.Name));
                    element.OnValueChanged += x => parameterObjects[index] = x;
                    element.style.paddingRight = 4f;
                    _foldout.Add(element);
                }
            }
        }
    }
}