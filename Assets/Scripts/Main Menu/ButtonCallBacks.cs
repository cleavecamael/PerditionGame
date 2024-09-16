using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonCallBacks : MonoBehaviour
{
    // Start is called before the first frame update
    public void TransitionToWorld1()
    {
        SceneManager.LoadSceneAsync("Tutorial-World", LoadSceneMode.Single);

    }

    public void TransitionToCutscene()
    {
        SceneManager.LoadSceneAsync("StoryIntro", LoadSceneMode.Single);
    }

    public void TransitionToShop()
    {
        SceneManager.LoadSceneAsync("Shope Manu", LoadSceneMode.Single);
    }
    public void TransitionToSettings()
    {
        SceneManager.LoadSceneAsync("Settings", LoadSceneMode.Additive);
    }
    public void TransitionToMainMenu()
    {
        SceneManager.LoadSceneAsync("Main Menu 2", LoadSceneMode.Single);

    }

    public void TransitionToWeaponsCollection()
    {
        SceneManager.LoadSceneAsync("WeaponCol", LoadSceneMode.Single);

    }

    public void playConfirmClip()
    {
        AudioManager.playClip("MenuSelect");
    }

    public void playHoverClip()
    {
        AudioManager.playClip("menuHover");
    }

    public void playCloseClip()
    {
        AudioManager.playClip("Close");
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

}
