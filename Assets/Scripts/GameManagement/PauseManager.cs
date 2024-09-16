using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public UnityEvent<bool> togglePlayerActions;
    private bool paused;

    void Awake()
    {
        paused = false;
    }

    public void TogglePauseMenu()
    {
        paused ^= true;
        if (paused)
        {
            Time.timeScale = 0;
            togglePlayerActions.Invoke(false);
            SceneManager.LoadScene("PauseMenu", LoadSceneMode.Additive);
        }
        else
        {
            Time.timeScale = 1;
            togglePlayerActions.Invoke(true);
            SceneManager.UnloadSceneAsync("PauseMenu");
        }
    }
}