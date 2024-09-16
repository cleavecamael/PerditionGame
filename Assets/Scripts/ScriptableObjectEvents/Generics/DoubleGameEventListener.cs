using UnityEngine;
using UnityEngine.Events;

// if attached to an object that might be disabled, callback will not work
// attach it on a parent object that wont be disabled
public class DoubleGameEventListener<T1, T2> : MonoBehaviour
{
    public DoubleGameEvent<T1, T2> Event;

    public UnityEvent<T1, T2> Response;

    private void OnEnable()
    {
        Event.RegisterListener(this);
    }

    private void OnDisable()
    {
        Event.UnregisterListener(this);
    }

    public void OnEventRaised(T1 data1, T2 data2)
    {
        Response.Invoke(data1, data2);
    }
}