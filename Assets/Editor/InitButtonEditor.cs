using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor.AnimatedValues;

[CustomEditor(typeof(InitButton), true)]
public class InitButtonEditor : Editor {
    GUIContent m_VisualizeNavigation = new GUIContent("Init", "Cilck to Init");



    private static bool ShowNavigation = false;
    public bool s_ShowNavigation { get { return ShowNavigation; }
        set { if (ShowNavigation == false && value) {
                InitAndReturn();
            } } }
    SerializedProperty m_NavigationProperty;
    SerializedProperty m_resetTargetGraphic;
    SerializedProperty m_addParticleEffect;
    SerializedProperty m_TraceParticleSystemPrefab;
    SerializedProperty m_ClickParticleSystemPrefab;
    SerializedProperty m_addClickedEvent;
    SerializedProperty m_OnClick;

    private string[] m_PropertyPathToExcludeForChildClasses;
    protected virtual void OnEnable()
    {
        m_resetTargetGraphic = serializedObject.FindProperty("resetTargetGraphic");

        m_addParticleEffect = serializedObject.FindProperty("addParticleEffect");
        m_TraceParticleSystemPrefab = serializedObject.FindProperty("TraceParticleSystemPrefab");
        m_ClickParticleSystemPrefab = serializedObject.FindProperty("ClickParticleSystemPrefab");

        m_addClickedEvent = serializedObject.FindProperty("addClickedEvent");
        m_OnClick = serializedObject.FindProperty("m_OnClick");

        m_NavigationProperty = serializedObject.FindProperty("m_Navigation");

        m_PropertyPathToExcludeForChildClasses = new[]
                {
               //m_NavigationProperty.propertyPath,
               m_resetTargetGraphic.propertyPath,
               //m_addParticleEffect.propertyPath,m_TraceParticleSystemPrefab.propertyPath,m_ClickParticleSystemPrefab.propertyPath,
               //m_addClickedEvent.propertyPath,m_OnClick.propertyPath,

        };

        //RegisterStaticOnSceneGUI();
        SceneView.onSceneGUIDelegate += StaticOnSceneGUI;
       
    }
    private void OnDisable()
    {
        SceneView.onSceneGUIDelegate -= StaticOnSceneGUI;
    }
    public override void OnInspectorGUI()
    {
 
        EditorGUILayout.Space();

       EditorGUILayout.PropertyField(m_resetTargetGraphic);
        
            Rect toggleRect = EditorGUILayout.GetControlRect();
        toggleRect.xMin += EditorGUIUtility.labelWidth;
        s_ShowNavigation = GUI.Toggle(toggleRect, s_ShowNavigation, m_VisualizeNavigation, EditorStyles.miniButton);
       
        ChildClassPropertiesGUI();
        serializedObject.ApplyModifiedProperties();
    }

    private void ChildClassPropertiesGUI()
    {
        if (IsDerivedSelectableEditor())
            return;

        DrawPropertiesExcluding(serializedObject, m_PropertyPathToExcludeForChildClasses);
    }

    private bool IsDerivedSelectableEditor()
    {
        return GetType() != typeof(InitButtonEditor);
    }

    private void RegisterStaticOnSceneGUI()
    {
        SceneView.onSceneGUIDelegate -= StaticOnSceneGUI;
        
        SceneView.onSceneGUIDelegate += StaticOnSceneGUI;
    }
    //rremove static
    private  void StaticOnSceneGUI(SceneView view)
    {
        if (!s_ShowNavigation)
            return;

        Debug.Log("we reach in StaticOnSceneGUI end");
        
    }

    private void InitAndReturn()
    {
        InitButton debug = target as InitButton;
        //InitButton initButton = (target as GameObject).GetComponent<InitButton>();
        (target as InitButton).reset();
        
    }
}
