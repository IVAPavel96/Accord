using UnityEditor;
using UnityEditor.UI;

[CustomEditor(typeof(HoldableButton), true)]
[CanEditMultipleObjects]
public class HoldableButtonEditor : SelectableEditor
{
    SerializedProperty onClickProperty;
    SerializedProperty onHoldProperty;
    SerializedProperty onReleaseProperty;

    protected override void OnEnable()
    {
        base.OnEnable();
        onClickProperty = serializedObject.FindProperty("m_OnClick");
        onHoldProperty = serializedObject.FindProperty("onHold");
        onReleaseProperty = serializedObject.FindProperty("onRelease");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        EditorGUILayout.Space();

        serializedObject.Update();
        EditorGUILayout.PropertyField(onClickProperty);
        EditorGUILayout.PropertyField(onHoldProperty);
        EditorGUILayout.PropertyField(onReleaseProperty);
        serializedObject.ApplyModifiedProperties();
    }
}