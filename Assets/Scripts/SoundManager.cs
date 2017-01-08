using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class SoundManager : MonoBehaviour
{
    public static SoundManager instance = null;

    [System.Serializable]
     public struct SSoundSource
     {
        public string soundName;
        public AudioSource audioSource;
     }
 
    public SSoundSource[] audioSources;
    private Dictionary<string, AudioSource> m_pAudioSources;

    void Start()
    {
        if (instance != null)
        {
            Debug.LogError("Sound Manager already created !");
            return;
        }
           
        instance = this;
   
        m_pAudioSources = new Dictionary<string, AudioSource>();

        foreach (SSoundSource source in audioSources)
            m_pAudioSources.Add(source.soundName, source.audioSource);
    }

    public AudioSource Play(string soundName)
    {
        m_pAudioSources[soundName].Play();
        return m_pAudioSources[soundName];
    }
}