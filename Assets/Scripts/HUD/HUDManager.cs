using UnityEngine;

public class HUDManager : MonoBehaviour
{
    private bool active;

    void Awake()
    {
        active = true;
    }
    public void ToggleVisibility()
    {
        active ^= true;
        GetComponent<CanvasGroup>().alpha = active ? 1.0f : 0.0f;
    }
}
