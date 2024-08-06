using System.Collections.Generic;
using Alchemy.Inspector;
using Alchemy.Serialization;
using Tweaks;
using UnityEngine;

[AlchemySerialize]
[CreateAssetMenu(fileName = "TestSO", menuName = "TestSO")]
public partial class TestSO : ScriptableObject
{
    [Title("Title Test", subtitle:"A SubTitle Test")]
    [Preview]
    public Texture2D testTexture;
    
    [AlchemySerializeField] Dictionary<int, TestInnerClass> testDictionary = new Dictionary<int, TestInnerClass>();
    
    [ListViewSettings()]
    public List<TestInnerClass> testList = new List<TestInnerClass>();

    [SerializeReference] public ITestInterface testInterface;
    [SerializeReference] public ITestInterface testInterface2;
    
    [Block]
    public List<TestInnerClass> testList2 = new List<TestInnerClass>();
    
    [Block]
    [SerializeReference] public ITestInterface testInterface3;

    [BoxGroup("A")] public string boxGroup1;
    [BoxGroup("A")] public string boxGroup2;
    
    [Group("B")] public string group1;
    [Group("B")] public string group2;
  
    
    [TabGroup("Tab1")] public string tabGroup1;
    [TabGroup("Tab1")] public string tabGroup2;
    
    [TabGroup("Tab2")] public string tabGroup3;
    [TabGroup("Tab2")] public string tabGroup4;
    
    [TabGroup("Tab3")] public string tabGroup5;
    [TabGroup("Tab3")] public string tabGroup6;
    
    [Block]
    [FoldoutGroup("Foldout1")] public string foldoutGroup1;
    [FoldoutGroup("Foldout1")] public string foldoutGroup2;
    
    [HorizontalGroup("Horizontal1")] public string horizontalGroup1;
    [HorizontalGroup("Horizontal1")] public string horizontalGroup2;
    
    [HorizontalGroup("Horizontal2")] public string horizontalGroup3;
    [HorizontalGroup("Horizontal2")] public string horizontalGroup4;
    [HorizontalGroup("Horizontal2")] public string horizontalGroup5;
    
    
    [HorizontalGroup("Group2")]
    [BoxGroup("Group2/Box1")]
    public float foo;

    [HorizontalGroup("Group2")]
    [BoxGroup("Group2/Box1")]
    public Vector3 bar;

    [HorizontalGroup("Group2")]
    [BoxGroup("Group2/Box1")]
    public GameObject baz;

    [HorizontalGroup("Group2")]
    [BoxGroup("Group2/Box2")]
    public float alpha;

    [HorizontalGroup("Group2")]
    [BoxGroup("Group2/Box2")]
    public Vector3 beta;

    [HorizontalGroup("Group2")]
    [BoxGroup("Group2/Box2")]
    public GameObject gamma;

    [Block]
    [InlineEditor]
    public TestSO a;
    
    [Block]
    public TestInnerClass testInnerClass;
    
    [System.Serializable]
    public class TestInnerClass
    {
        public int testInt;
        
        public string testString;
        
        public List<int> testIntList = new List<int>();
    }

    [Button]
    private void DoThing(int testint)
    {
    }
}
