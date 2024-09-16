using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Cursors")]
public class Cursors : ScriptableObject
{
    public CursorData[] cursors;
    public SceneCursorMapping[] sceneToCursorMappings;

    [System.Serializable]
    public struct CursorData
    {
        public Texture2D cursor;
        public Vector2 hotspot;
        public CursorContext context;
    }

    [System.Serializable]
    public struct SceneCursorMapping
    {
        public CursorContext context;
        public string scene;
    }

    public CursorData GetCursor(CursorContext context)
    {
        foreach (var cursor in cursors)
        {
            if (cursor.context == context)
            {
                return cursor;
            }
        }
        return cursors[0];
    }

    public CursorContext GetContext(string sceneName)
    {
        foreach (var mapping in sceneToCursorMappings)
        {
            if (mapping.scene == sceneName)
            {
                return mapping.context;
            }
        }
        return CursorContext.Default;
    }

}