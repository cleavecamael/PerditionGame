using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/KillCountSystem")]
public class KillCountSystem : ScriptableObject
{
    public int count;

    void OnEnable()
    {
        hideFlags = HideFlags.DontUnloadUnusedAsset;
    }

    public void AddKillCount()
    {
        count++;
    }
    public void ResetKillCount()
    {
        count = 0;
    }


}
