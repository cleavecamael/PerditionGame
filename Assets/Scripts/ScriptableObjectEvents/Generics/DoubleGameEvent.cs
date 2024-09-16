using System.Collections.Generic;
using UnityEngine;

public class DoubleGameEvent<T1, T2> : ScriptableObject
{
    private readonly List<DoubleGameEventListener<T1, T2>> eventListeners =
        new List<DoubleGameEventListener<T1, T2>>();

    public void Raise(T1 data1, T2 data2)
    {
        for (int i = eventListeners.Count - 1; i >= 0; i--)
            eventListeners[i].OnEventRaised(data1, data2);
    }

    public void RegisterListener(DoubleGameEventListener<T1, T2> listener)
    {
        if (!eventListeners.Contains(listener))
            eventListeners.Add(listener);
    }

    public void UnregisterListener(DoubleGameEventListener<T1, T2> listener)
    {
        if (eventListeners.Contains(listener))
            eventListeners.Remove(listener);
    }
}