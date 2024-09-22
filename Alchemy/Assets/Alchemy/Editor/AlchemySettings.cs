using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Alchemy.Hierarchy;
using Alchemy.Inspector;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

namespace Alchemy.Editor
{
    /// <summary>
    /// Alchemy project-level settings
    /// </summary>
    public sealed class AlchemySettings : ScriptableObject
    {
        static readonly string SettingsPath = "ProjectSettings/AlchemySettings.json";

        static AlchemySettings instance;

        /// <summary>
        /// Get a cached instance. If the cache does not exist, returns a newly created one.
        /// </summary>
        public static AlchemySettings GetOrCreateSettings()
        {
            if (instance != null) return instance;

            if (File.Exists(SettingsPath))
            {
                instance = CreateInstance<AlchemySettings>();
                JsonUtility.FromJsonOverwrite(File.ReadAllText(SettingsPath), instance);
            }
            else
            {
                instance = CreateInstance<AlchemySettings>();
            }

            return instance;
        }

        /// <summary>
        /// Save the settings to a file.
        /// </summary>
        public static void SaveSettings()
        {
            File.WriteAllText(SettingsPath, JsonUtility.ToJson(instance, true));
        }

        static readonly string SettingsMenuName = "Project/Alchemy";

        [SettingsProvider]
        internal static SettingsProvider CreateSettingsProvider()
        {
            return new SettingsProvider(SettingsMenuName, SettingsScope.Project)
            {
                label = "Alchemy",
                keywords = new HashSet<string>(new[] { "Alchemy, Inspector, Hierarchy" }),
                guiHandler = searchContext =>
                {
                    var serializedObject = new SerializedObject(GetOrCreateSettings());

                    using (new EditorGUILayout.HorizontalScope())
                    {
                        GUILayout.Space(10f);
                        using (new EditorGUILayout.VerticalScope())
                        {
                            EditorGUILayout.LabelField("Hierarchy", EditorStyles.boldLabel);

                            using (var changeCheck = new EditorGUI.ChangeCheckScope())
                            {
                                EditorGUILayout.PropertyField(serializedObject.FindProperty("hierarchyObjectMode"));
                                EditorGUILayout.PropertyField(serializedObject.FindProperty("showHierarchyToggles"), new GUIContent("Show Toggles"));
                                EditorGUILayout.PropertyField(serializedObject.FindProperty("showComponentIcons"));
                                var showTreeMap = serializedObject.FindProperty("showTreeMap");
                                EditorGUILayout.PropertyField(showTreeMap);
                                if (showTreeMap.boolValue)
                                {
                                    EditorGUI.indentLevel++;
                                    EditorGUILayout.PropertyField(serializedObject.FindProperty("treeMapColor"), new GUIContent("Color"));
                                    EditorGUI.indentLevel--;
                                }

                                var showSeparator = serializedObject.FindProperty("showSeparator");
                                EditorGUILayout.PropertyField(showSeparator, new GUIContent("Show Row Separator"));
                                if (showSeparator.boolValue)
                                {
                                    EditorGUI.indentLevel++;
                                    EditorGUILayout.PropertyField(serializedObject.FindProperty("separatorColor"), new GUIContent("Color"));
                                    EditorGUI.indentLevel--;
                                    var showRowShading = serializedObject.FindProperty("showRowShading");
                                    EditorGUILayout.PropertyField(showRowShading);
                                    if (showRowShading.boolValue)
                                    {
                                        EditorGUI.indentLevel++;
                                        EditorGUILayout.PropertyField(serializedObject.FindProperty("evenRowColor"));
                                        EditorGUILayout.PropertyField(serializedObject.FindProperty("oddRowColor"));
                                        EditorGUI.indentLevel--;
                                    }
                                }

                                EditorGUILayout.Space(15);
                                EditorGUILayout.LabelField("Inspector Defaults", EditorStyles.boldLabel);
                                EditorGUILayout.PropertyField(serializedObject.FindProperty("defaultGroupStyle"));
                                EditorGUILayout.PropertyField(serializedObject.FindProperty("defaultBoxGroupStyle"));
                                EditorGUILayout.PropertyField(serializedObject.FindProperty("defaultTabGroupStyle"));
                                EditorGUILayout.PropertyField(serializedObject.FindProperty("defaultFoldoutGroupStyle"));
                                EditorGUILayout.PropertyField(serializedObject.FindProperty("defaultClassStyle"));
                                EditorGUILayout.PropertyField(serializedObject.FindProperty("defaultSerializeReferenceStyle"));
                                EditorGUILayout.PropertyField(serializedObject.FindProperty("defaultListViewStyle"));

                                if (changeCheck.changed)
                                {
                                    serializedObject.ApplyModifiedProperties();
                                    SaveSettings();
                                }
                            }
                        }
                    }
                },
            };
        }

        [SerializeField] HierarchyObjectMode hierarchyObjectMode = HierarchyObjectMode.RemoveInBuild;
        [SerializeField] bool showHierarchyToggles;
        [SerializeField] bool showComponentIcons;
        [SerializeField] bool showTreeMap;
        [SerializeField] Color treeMapColor = new(0.53f, 0.53f, 0.53f, 0.45f);
        [SerializeField] bool showSeparator;
        [SerializeField] bool showRowShading;
        [SerializeField] Color separatorColor = new(0.19f, 0.19f, 0.19f, 0f);
        [SerializeField] Color evenRowColor = new(0f, 0f, 0f, 0.07f);
        [SerializeField] Color oddRowColor = Color.clear;
        
        public HierarchyObjectMode HierarchyObjectMode => hierarchyObjectMode;
        public bool ShowHierarchyToggles => showHierarchyToggles;
        public bool ShowComponentIcons => showComponentIcons;
        public bool ShowTreeMap => showTreeMap;
        public Color TreeMapColor => treeMapColor;
        public bool ShowSeparator => showSeparator;
        public bool ShowRowShading => showRowShading;
        public Color SeparatorColor => separatorColor;
        public Color EvenRowColor => evenRowColor;
        public Color OddRowColor => oddRowColor;

        [SerializeField] private GroupDefaultStyles defaultGroupStyle;
        [SerializeField] private BoxGroupDefaultStyles defaultBoxGroupStyle;
        [SerializeField] private TabGroupDefaultStyles defaultTabGroupStyle;
        [SerializeField] private FoldoutGroupDefaultStyles defaultFoldoutGroupStyle;
        [SerializeField] private ClassDefaultStyles defaultClassStyle;
        [SerializeField] private SerializeReferenceDefaultStyles defaultSerializeReferenceStyle;
        [SerializeField] private ListViewDefaults defaultListViewStyle;
        
        public GroupDefaultStyles DefaultGroupStyle => defaultGroupStyle;
        public BoxGroupDefaultStyles DefaultBoxGroupStyle => defaultBoxGroupStyle;
        public TabGroupDefaultStyles DefaultTabGroupStyle => defaultTabGroupStyle;
        public FoldoutGroupDefaultStyles DefaultFoldoutGroupStyle => defaultFoldoutGroupStyle;
        public ClassDefaultStyles DefaultClassStyle => defaultClassStyle;
        public SerializeReferenceDefaultStyles DefaultSerializeReferenceStyle => defaultSerializeReferenceStyle;
        public ListViewDefaults DefaultListViewStyle => defaultListViewStyle;
    }
    [System.Serializable]
    public class GroupDefaultStyles
    {
        [SerializeField] public GroupStyle groupStyle = GroupStyle.Default;
        [SerializeField] public HeaderStyle headerStyle = HeaderStyle.Default;
        [SerializeField] public BodyStyle bodyStyle = BodyStyle.Default;
        [SerializeField] public TintColor tintColor = TintColor.Active;
        [SerializeField] public bool seperateHeader = false;
    }
    
    [System.Serializable]
    public class BoxGroupDefaultStyles
    {
        [SerializeField] public GroupStyle groupStyle = GroupStyle.Boxed;
        [SerializeField] public HeaderStyle headerStyle = HeaderStyle.Default;
        [SerializeField] public BodyStyle bodyStyle = BodyStyle.Default;
        [SerializeField] public TintColor tintColor = TintColor.Active;
        [SerializeField] public bool seperateHeader = true;
    }
    
    [System.Serializable]
    public class TabGroupDefaultStyles
    {
        [SerializeField] public GroupStyle groupStyle = GroupStyle.Boxed;
        [SerializeField] public HeaderStyle headerStyle = HeaderStyle.Tint;
        [SerializeField] public BodyStyle bodyStyle = BodyStyle.Default;
        [SerializeField] public TintColor tintColor = TintColor.Active;
        [SerializeField] public bool expandTabs = true;
    }
    
    [System.Serializable]
    public class FoldoutGroupDefaultStyles
    {
        [SerializeField] public GroupStyle groupStyle = GroupStyle.Boxed;
        [SerializeField] public HeaderStyle headerStyle = HeaderStyle.Default;
        [SerializeField] public BodyStyle bodyStyle = BodyStyle.Default;
        [SerializeField] public TintColor tintColor = TintColor.Active;
        [SerializeField] public bool seperateHeader = true;
    }
    
    [System.Serializable]
    public class ClassDefaultStyles
    {
        [SerializeField] public GroupStyle groupStyle = GroupStyle.Default;
        [SerializeField] public HeaderStyle headerStyle = HeaderStyle.Default;
        [SerializeField] public BodyStyle bodyStyle = BodyStyle.None;
        [SerializeField] public TintColor tintColor = TintColor.Active;
    }
    
    [System.Serializable]
    public class SerializeReferenceDefaultStyles
    {
        [SerializeField] public GroupStyle groupStyle = GroupStyle.Default;
        [SerializeField] public HeaderStyle headerStyle = HeaderStyle.Default;
        [SerializeField] public BodyStyle bodyStyle = BodyStyle.None;
        [SerializeField] public TintColor tintColor = TintColor.Active;
    }
    
    [System.Serializable]
    public class ListViewDefaults
    {
        [SerializeField] public GroupStyle groupStyle = GroupStyle.Default;
        [SerializeField] public HeaderStyle headerStyle = HeaderStyle.Default;
        [SerializeField] public BodyStyle bodyStyle = BodyStyle.Default;
        [SerializeField] public TintColor tintColor = TintColor.Active;
        [SerializeField] public AlternatingRowBackground alternatingRowBackground = AlternatingRowBackground.None;
    }
}