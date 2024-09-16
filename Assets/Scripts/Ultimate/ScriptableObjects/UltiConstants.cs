using UnityEngine;

[CreateAssetMenu(fileName = "UltiConstants", menuName = "ScriptableObjects/UltiConstants", order = 1)]
public class UltiConstants : ScriptableObject
{
    public float velocityMissile;
    public float radius;
    public float damage = 25;
    public bool charge;
}
