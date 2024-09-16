using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/OxygenTimeSystem")]
public class OxygenTimeSystem : ScriptableObject
{
    // [HideInInspector]
    public float currentTime;
    public GameConstants gameConstants;
    public SimpleGameEvent restart;

    private void OnEnable()
    {
        hideFlags = HideFlags.DontUnloadUnusedAsset;
    }

    public void SetTime(float time)
    {
        currentTime = time;
    }
    public void ResetTime()
    {
        currentTime = gameConstants.startingOxygenTime;
    }


}
