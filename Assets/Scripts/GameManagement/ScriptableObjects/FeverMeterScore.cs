using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/FeverMeterScore")]
public class FeverMeterScore : ScriptableObject
{
    public float CurrentFever { get; set; }
    public float damageBuff;
    public bool activeFever;
}