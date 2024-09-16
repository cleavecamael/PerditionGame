using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryButtonCallBacks : MonoBehaviour
{
    // Start is called before the first frame update
    public void TransitionToWorld1()
    {
        SceneManager.LoadSceneAsync("World-1", LoadSceneMode.Single);
    }

    public void TransitionToMainMenu()
    {
        SceneManager.LoadSceneAsync("Main Menu 2", LoadSceneMode.Single);
    }
}
