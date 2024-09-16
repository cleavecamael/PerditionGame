using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.PostProcessing.HistogramMonitor;

public class AudioManager : Singleton<AudioManager>
{
    public AudioFiles audioFileSO;
    static AudioFiles.AudioFile[] audioFiles;
    public static Dictionary<string, AudioClip> audioFileMap = new Dictionary<string, AudioClip>();
    public static Dictionary<string, float> lastPlayedMap = new Dictionary<string, float>();
    public static Dictionary<string, float> audioCooldownMap = new Dictionary<string, float>();

   
    public void OnEnable()
    {
      
        audioFiles = audioFileSO.audioFiles;
        for (int i = 0; i < audioFiles.Length; i++)
        {
           
            audioFileMap.Add(audioFiles[i].id, audioFiles[i].clip);
            lastPlayedMap.Add(audioFiles[i].id, 0f);
            audioCooldownMap.Add(audioFiles[i].id, audioFiles[i].cooldown);
        }
    }

    //get a sfx clip
    public static AudioClip getClip(string id)
    {
        return audioFileMap[id];
    }

    //play a sfx clip
    public static void playClip(AudioSource source, string clipName)
    {
        float lastPlayed = lastPlayedMap[clipName];
        float currentTime = Time.time;
        float cooldown = audioCooldownMap[clipName];
        AudioClip clip = getClip(clipName);

        if (currentTime - lastPlayed > cooldown)
        {
           
            lastPlayedMap[clipName] = currentTime;
            source.PlayOneShot(clip);
        }
    }
    public static void playClip(string clipName)
    {
        playClip(GameObject.Find("Default Channel").GetComponent<AudioSource>(), clipName);
    }
    public static void playClip(string channel, string clipName)
    {
        playClip(GameObject.Find(channel + " Channel").GetComponent<AudioSource>(), clipName);
    }
    public static void playContinuous(AudioSource source, string clipName)
    {
        source.Stop();
        AudioClip clip = getClip(clipName);
        source.clip = clip;
        source.Play();
    }
    public static void stopSource(AudioSource source)
    {
        source.Stop();
    }
    //play a random clip from the array (eg. when using a random step sound for walking)
    public static void playRandomClip(AudioSource source, string[] ids)
    {
        int end = ids.Length;
        int picked = Random.Range(0, end);
        float currentTime = Time.time;
        float maxlastPlayed = 0;
        float maxcooldown = 0;
        foreach (string id in ids)
        {
           if( lastPlayedMap[id] > maxlastPlayed)
            {
                maxlastPlayed = lastPlayedMap[id];
            }
            if (audioCooldownMap[id] > maxcooldown)
            {
                maxcooldown = audioCooldownMap[id];
            }
        }

        if (currentTime - maxlastPlayed > maxcooldown)
        {
            foreach (string id in ids)
            {
                lastPlayedMap[id] = currentTime;
            }
            source.PlayOneShot(getClip(ids[picked]));
        }


    }
    public static AudioSource getChannel(string channel)
    {
        return GameObject.Find(channel + " Channel").GetComponent<AudioSource>();
    }

    public static void pauseAllChannels()
    {
        foreach (Transform child in GameObject.Find("Channels").transform)
        {
            child.GetComponent<AudioSource>().Pause();
            child.GetComponent<AudioSource>().enabled = false;
        }
    }
    public static void pauseChannel(string channel)
    { 
        var channelObject = GameObject.Find(channel + " Channel").GetComponent<AudioSource>();
        AudioSource channelSource = channelObject.GetComponent<AudioSource>();
        channelSource.Pause();
        channelSource.volume = 0;
        channelSource.enabled = false;
    }
    public static void resumeAllChannels()
    {
        foreach (Transform child in GameObject.Find("Channels").transform)
        {
            
            child.GetComponent<AudioSource>().UnPause();
            child.GetComponent<AudioSource>().volume = 1;
            child.GetComponent<AudioSource>().enabled = true;
        }
    }
    public static void resumeChannel(string channel)
    {
        var channelObject = GameObject.Find(channel + " Channel").GetComponent<AudioSource>();
        channelObject.UnPause();
        channelObject.volume = 1;
        channelObject.enabled = true;
    }
    public static void resetChannel(string channel)
    {
        var channelObject = GameObject.Find(channel + " Channel").GetComponent<AudioSource>();
        channelObject.Stop();
        channelObject.volume = 1;
        channelObject.enabled = true;
    }
    public static void resetAllChannels()
    {
        foreach (Transform child in GameObject.Find("Channels").transform)
        {

            child.GetComponent<AudioSource>().Stop();
            child.GetComponent<AudioSource>().volume = 1;
            child.GetComponent<AudioSource>().enabled = true;
        }
    }
}
