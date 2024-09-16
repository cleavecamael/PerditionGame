using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Collider2DSO")]
public class Collider2DSO : ScriptableObject
{
    public Collider2D col { get; set; }
}