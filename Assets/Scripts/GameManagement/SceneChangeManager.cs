using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneChangeManager : MonoBehaviour
{
    public void TransitionToTutorial()
    {
        SceneManager.LoadSceneAsync("Tutorial-World", LoadSceneMode.Single);
    }

    public void TransitionToEndCutscene()
    {
        SceneManager.LoadSceneAsync("StoryEnding", LoadSceneMode.Single);
    }

    public void TransitionToIntermediateScene()
    {
        SceneManager.LoadSceneAsync("Victory 2", LoadSceneMode.Single);
    }

    // For testing persistence
    public UnityEvent OnClearLevel;

    public void ClearLevel()
    {
        OnClearLevel.Invoke();
    }

    public void TransitionToWorld2()
    {
        SceneManager.LoadSceneAsync("World-2", LoadSceneMode.Single);
    }
    public void TransitionToVictoryScene()
    {
        SceneManager.LoadSceneAsync("Victory Scene", LoadSceneMode.Single);
    }
}
