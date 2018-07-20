using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using UnityEngine.UI;

[CustomEditor(typeof(InitImage), true)]
public class InitImageEditor : Editor
{

    private static bool ShowNavigation = false;
    public bool s_ShowNavigation
    {
        get { return ShowNavigation; }
        set
        {
            if (ShowNavigation == false && value)
            {
                InitAndReturn();
            }
        }
    }
    GUIContent m_VisualizeNavigation = new GUIContent("Init", "Cilck to Init");
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void OnInspectorGUI()
    {

        EditorGUILayout.Space();

        //EditorGUILayout.PropertyField(m_resetTargetGraphic);

        Rect toggleRect = EditorGUILayout.GetControlRect();
        toggleRect.xMin += EditorGUIUtility.labelWidth;
        s_ShowNavigation = GUI.Toggle(toggleRect, s_ShowNavigation, m_VisualizeNavigation, EditorStyles.miniButton);

        // ChildClassPropertiesGUI();
        DrawPropertiesExcluding(serializedObject, new string[0]);
        serializedObject.ApplyModifiedProperties();
    }

    private void InitAndReturn()
    {
       // InitImage debug = target as InitImage;
        //InitButton initButton = (target as GameObject).GetComponent<InitButton>();
        (target as InitImage).reset();

    }
}
