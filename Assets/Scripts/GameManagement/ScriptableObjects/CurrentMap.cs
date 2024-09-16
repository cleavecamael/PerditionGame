using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/CurrentMap")]
public class CurrentMap : ScriptableObject
{
    public string CurrentMapName { get; set; }

    public Dictionary<string, string> nextMap = new Dictionary<string, string>(){
        {"Main Menu 2","Tutorial-World"},
        {"World-1","World-2"},
        {"World-2","Victory Scene"}
    };
}