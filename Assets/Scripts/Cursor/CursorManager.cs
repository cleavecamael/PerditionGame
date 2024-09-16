using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CursorManager : MonoBehaviour
{
    [SerializeField] private Cursors cursors;
    [SerializeField] private CursorContext context;
    private Cursors.CursorData currentCursor;
    private Stack<string> scenes;

    void Awake()
    {
        scenes = new Stack<string>();
        SceneManager.sceneLoaded += OnSceneLoad;
        SceneManager.sceneUnloaded += OnSceneUnload;
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoad;
        SceneManager.sceneUnloaded -= OnSceneUnload;
    }

    void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        Cursors.CursorData cursor = cursors.GetCursor(cursors.GetContext(scene.name));
        Cursor.SetCursor(cursor.cursor, cursor.hotspot, CursorMode.Auto);
        scenes.Push(scene.name);
    }

    void OnSceneUnload(Scene scene)
    {
        scenes.Pop();
        if (scenes.Count != 0)
        {
            Cursors.CursorData cursor = cursors.GetCursor(cursors.GetContext(scenes.Peek()));
            Cursor.SetCursor(cursor.cursor, cursor.hotspot, CursorMode.Auto);
        }
    }
}