using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SoulManager))]
public class SoulManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        SoulManager soulManager = (SoulManager)target;

        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (GUILayout.Button("Spawn Soul"))
        {
            soulManager.SpawnSoul(player.transform.position, 500);
        }
    }
}