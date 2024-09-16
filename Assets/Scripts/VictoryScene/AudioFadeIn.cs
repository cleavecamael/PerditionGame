using System.Collections;
using UnityEngine;

public class AudioFadeIn : MonoBehaviour
{
    public AudioSource audioSource;
    public float fadeInTime = 2.0f; // Time to fade in the audio (in seconds)
    public float startVolume = 0;


    void Start()
    {
        // Make sure an AudioSource is attached
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                Debug.LogError("AudioSource not found!");
                return;
            }
        }

        // Start the fade-in coroutine
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        float currentTime = 0;

        // Remember the initial volume
        if (audioSource.isPlaying)
        {
            audioSource.volume = startVolume;
        }

        while (currentTime < fadeInTime)
        {
            currentTime += Time.deltaTime;

            // Calculate the new volume using Lerp
            audioSource.volume = Mathf.Lerp(startVolume, 1f, currentTime / fadeInTime);

            yield return null; // Wait for the next frame
        }

        // Ensure the volume is set to 1 at the end
        audioSource.volume = 1f;
    }
}
