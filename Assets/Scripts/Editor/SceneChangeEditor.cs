using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SceneChangeManager))]
public class SceneChangeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        SceneChangeManager sceneChangeManager = (SceneChangeManager)target;

        if (GUILayout.Button("Trigger level clear"))
        {
            sceneChangeManager.ClearLevel();
        }
    }
}