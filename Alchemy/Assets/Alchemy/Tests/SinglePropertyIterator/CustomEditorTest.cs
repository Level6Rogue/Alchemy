using System.Collections.Generic;
using Alchemy.Inspector;
using UnityEngine;

namespace Alchemy.Tests.Runtime
{
    public class CustomEditorTest : MonoBehaviour
    {
        [HorizontalLine]
        [SerializeReference] public ITestInterface testInterface;
        
        public List<TestClass1> testClass1List;
    }
    
    public interface ITestInterface
    {
        
    }

    [System.Serializable]
    public class TestClass1 : ITestInterface
    {
        [ReadOnly]
        public string testString1 = "Hammer Time";

        [Blockquote("Lorem Ipsum")] 
        public int testInt1 = -1;
        
        [HelpBox("This is helpful")]
        public float testFloat1 = 3.14f;
    }
    
    [System.Serializable]
    public class TestClass2 : ITestInterface
    {
        public string testString2;
        public int testInt2;
        public float testFloat2;
    }
}
