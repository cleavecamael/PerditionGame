using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/QuestObjectSystem")]
public class QuestObjectSystem : ScriptableObject
{
    void OnEnable()
    {
        hideFlags = HideFlags.DontUnloadUnusedAsset;
    }

    public float timeToRead = 30f;

    public List<String> quests;

    private int index = 0;

    public String GetNextQuest()
    {
        string res = quests[index];
        index++;
        return res;
    }

    public void ResetQuests()
    {
        index = 0;
    }
}