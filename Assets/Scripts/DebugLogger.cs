using UnityEngine;

public static class DebugLogger
{
    public static bool enabled = true;
    public static void Log(string message)
    {
        if (enabled)
        {
            Debug.Log(message);
        }
        
    }
}
