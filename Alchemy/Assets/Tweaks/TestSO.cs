using System.Collections.Generic;
using Alchemy.Inspector;
using Tweaks;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "TestSO", menuName = "TestSO")]
public class TestSO : ScriptableObject
{
    [ListViewSettings()]
    public List<TestInnerClass> testList = new List<TestInnerClass>();

    [SerializeReference] public ITestInterface testInterface;
    [SerializeReference] public ITestInterface testInterface2;
    
    [System.Serializable]
    public class TestInnerClass
    {
        public int testInt;
        
        public string testString;
        
        public List<int> testIntList = new List<int>();
    }
}
