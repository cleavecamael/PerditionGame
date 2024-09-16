using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsCallbacks : MonoBehaviour
{
    // Start is called before the first frame update

    public TextMeshProUGUI masterVolumeText;
    public TextMeshProUGUI BGMVolumeText;
    public TextMeshProUGUI SFXVolumeText;
    public AudioMixer perditionMixer;
    public Slider masterSlider;
    public Slider BGMSlider;
    public Slider SFXSlider;
    public TMPro.TMP_Dropdown qualityDropdown;
    public TMPro.TMP_Dropdown resolutionDropdown;

    private float masterVolume = 100;
    private float BGMVolume = 100;
    private float SFXVolume = 100;
    private int quality = 1;
    private int width =1920;
    private int height =1080;

    Resolution[] resolutions;
    private int resolution = 0;


    void Start()
    {

        loadFromSettings();
        updateAll();
       
    }

    public void loadFromSettings()
    {
        setResolutions();
        if (PlayerPrefs.HasKey("masterVolume"))
        {
            masterVolume = PlayerPrefs.GetFloat("masterVolume");

        }
        if (PlayerPrefs.HasKey("BGMVolume"))
        {
            BGMVolume = PlayerPrefs.GetFloat("BGMVolume");

        }
        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            SFXVolume = PlayerPrefs.GetFloat("SFXVolume");

        }
        if (PlayerPrefs.HasKey("Quality"))
        {
            quality = PlayerPrefs.GetInt("Quality");
           

        }
        if (PlayerPrefs.HasKey("Resolution"))
        {
            resolution = PlayerPrefs.GetInt("Resolution");
        }

    }

    public void updateAll()
    {
        updateVolume(masterVolume);
        updateMasterText(masterVolume);
        updateBGMText(BGMVolume);
        updateBGMVolume(BGMVolume);
        updateSFXText(SFXVolume);
        updateSFXVolume(SFXVolume);
        updateQuality(quality);
        updateSliders();
    }
    public void saveSettings()
    {
        PlayerPrefs.SetFloat("masterVolume", masterVolume);
        PlayerPrefs.SetFloat("BGMVolume", BGMVolume);
        PlayerPrefs.SetFloat("SFXVolume", SFXVolume);
        PlayerPrefs.SetInt("Quality", quality);
        PlayerPrefs.SetInt("Resolution", resolution);

    }
    public void setResolutions()
    {
        resolutions = Screen.resolutions;
        List<string> options = new List<string>();
        foreach (Resolution resolution in resolutions)
        {
            options.Add(resolution.width.ToString() + "x" + resolution.height.ToString());
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.RefreshShownValue();
 }
    public void updateResolution(int newResolution)
    {
        Resolution updatedResolution = resolutions[newResolution];
        Screen.SetResolution(updatedResolution.width, updatedResolution.height, false);
        resolution = newResolution;
    }
    public void updateQuality(int newQuality)
    {
        QualitySettings.SetQualityLevel(newQuality);
        quality = newQuality;
    }
    public void updateMasterText(float volume)
    {
        int nearestVolume =  Mathf.RoundToInt(volume * 100);
        masterVolumeText.text = (nearestVolume).ToString();
    }
    public void updateBGMText(float volume)
    {
        int nearestVolume = Mathf.RoundToInt(volume * 100);
        BGMVolumeText.text = (nearestVolume).ToString();
    }
    public void updateSFXText(float volume)
    {
        int nearestVolume = Mathf.RoundToInt(volume * 100);
        SFXVolumeText.text = (nearestVolume).ToString();
    }

    public void updateVolume(float volume)
    {
        perditionMixer.SetFloat("masterVolume", volume * 40 - 40);
        masterVolume = volume;
    }
    public void updateBGMVolume(float volume)
    {
        perditionMixer.SetFloat("BGMVolume", volume * 40 - 40);
        BGMVolume = volume;
    }
    public void updateSFXVolume(float volume)
    {
        perditionMixer.SetFloat("SFXVolume", volume * 40 - 40);
        SFXVolume = volume;
    }
    public void closeSettings()
    {
        loadFromSettings();
        updateAll();
        SceneManager.UnloadSceneAsync("Settings");
    }
    void updateSliders()
    {
        masterSlider.value = masterVolume;
        BGMSlider.value = BGMVolume;
        SFXSlider.value = SFXVolume;
        qualityDropdown.value = quality;
        qualityDropdown.RefreshShownValue();
        resolutionDropdown.value = resolution;
        resolutionDropdown.RefreshShownValue();

    }
}
