using System;
using UnityEngine;
[CreateAssetMenu(menuName = "Troops")]
public class Troops : ScriptableObject
{
    public Troop[] troops;
    [Serializable]
    public struct Troop
    {
        public string id;
        public GameObject prefab;
    }

}
