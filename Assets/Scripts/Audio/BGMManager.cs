using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    // Start is called before the first frame update
    AudioSource BGMSource;
    public string sceneBGM;
    public AudioFiles audioFilesSO;
    AudioFiles.AudioFile[] audioFiles;
    public AudioClip ambience;
    public Dictionary<string, AudioClip> audioFileMap = new Dictionary<string, AudioClip>();
    void Start()
    {
        BGMSource = GetComponent<AudioSource>();
        audioFiles = audioFilesSO.audioFiles;

        for (int i = 0; i < audioFiles.Length; i++)
        {
            audioFileMap.Add(audioFiles[i].id, audioFiles[i].clip);
        }

        playBGM(sceneBGM);
        BGMSource.PlayOneShot(ambience);

    }

   public void playBGM(string stringid)
    {
        LoadClip(stringid);
        playClip();
    }
    void playClip()
    {
        BGMSource.Play();
    }
    void pauseClip()
    {
        BGMSource.Play();
    }
    void StopClip()
    {
        BGMSource.Stop();
    }

    void LoadClip(string clipname)
    {
        BGMSource.clip = audioFileMap[clipname];
    }
}
