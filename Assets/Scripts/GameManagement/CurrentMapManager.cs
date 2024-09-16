using UnityEngine;
using UnityEngine.SceneManagement;
public class CurrentMapManager : MonoBehaviour
{

    public CurrentMap currentMap;

    void Awake()
    {
        currentMap.CurrentMapName = SceneManager.GetActiveScene().name;
    }

}
