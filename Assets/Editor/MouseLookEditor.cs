using UnityEngine;
using UnityEditor;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

[CustomEditor(typeof(MouseLook))]
public class MouseLookEditor : Editor
{
    public SerializedProperty lockCursor_Prop, mouseLookMode_Prop;

    void OnEnable()
    {
        // Setup the SerializedProperties
        mouseLookMode_Prop = serializedObject.FindProperty("mouseLookMode");
        lockCursor_Prop = serializedObject.FindProperty("lockCursor");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        MouseLook.MouseLookMode ml = (MouseLook.MouseLookMode)mouseLookMode_Prop.enumValueIndex;

        if (ml.Equals(MouseLook.MouseLookMode.MouseCenter))
        {
            EditorGUILayout.PropertyField(lockCursor_Prop, new GUIContent("lockCursor"));
        }
        
        serializedObject.ApplyModifiedProperties();
    }
}