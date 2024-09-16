using UnityEngine;
using UnityEngine.Audio;

public class InitiateSettings : MonoBehaviour
{
    public AudioMixer perditionMixer;

    // Start is called before the first frame update
    //initiate settings at game start
    void Start()
    {
        if (PlayerPrefs.HasKey("masterVolume"))
        {
            float masterVolume = PlayerPrefs.GetFloat("masterVolume");
            perditionMixer.SetFloat("masterVolume", masterVolume * 40 - 40);
   
        }
        if (PlayerPrefs.HasKey("BGMVolume"))
        {
            float BGMVolume = PlayerPrefs.GetFloat("BGMVolume");
            perditionMixer.SetFloat("BGMVolume", BGMVolume * 40 - 40);
        }
        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            float SFXVolume = PlayerPrefs.GetFloat("SFXVolume");
            perditionMixer.SetFloat("SFXVolume", SFXVolume * 40 - 40);
        }
        if (PlayerPrefs.HasKey("Quality"))
        {
            int quality = PlayerPrefs.GetInt("Quality");
            QualitySettings.SetQualityLevel(quality);
        }
        if (PlayerPrefs.HasKey("Resolution"))
        {
            int resolution = PlayerPrefs.GetInt("Resolution");
            Resolution updatedResolution = Screen.resolutions[resolution];
            Screen.SetResolution(updatedResolution.width, updatedResolution.height, false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
