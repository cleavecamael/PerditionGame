using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHandlers : MonoBehaviour
{
    public void OnTogglePause(bool toggle)
    {
        // Debug.Log("toggle is " + toggle);
        if (toggle)
        {
            AudioManager.resumeAllChannels();
        }
        else
        {
            AudioManager.pauseAllChannels();
            AudioManager.resumeChannel("Default");
        }
    }

    public void OnWeaponSwap(int swap)
    {
        AudioManager.resetChannel("Gun");
    }
    public void onPlayerDeath() 
    {
        AudioManager.pauseChannel("Gun");
    }
    public void onStartGame()
    {
        AudioManager.resetAllChannels();
    }
}
