using System;
using UnityEngine;
[CreateAssetMenu(menuName = "AudioFiles")]
public class AudioFiles : ScriptableObject
{
    public AudioFile[] audioFiles;
    [Serializable]
    public struct AudioFile
    {
        public string id;
        public AudioClip clip;
        public float cooldown;
    }

}
