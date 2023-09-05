using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }

    private AudioSource audioSource;

    [SerializeField]
    private List<AudioHolder> audioClipsList = new List<AudioHolder>();

    private Dictionary<SoundsFX, AudioClip> audioClipsDictionary = new Dictionary<SoundsFX, AudioClip>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        audioSource = Camera.main.GetComponent<AudioSource>();

        foreach (AudioHolder audio in audioClipsList)
        {
            if (!audioClipsDictionary.ContainsValue(audio.audioClip))
                audioClipsDictionary.Add(audio.audiokey, audio.audioClip);
        }
    }

    public void PlayClip(SoundsFX audioClipKey)
    {
        if (audioClipsDictionary.ContainsKey(audioClipKey))
        {
            audioSource.PlayOneShot(audioClipsDictionary[audioClipKey]);
        }
        else
        {
            Debug.Log("No audioclip key founded");
        }
    }
}

[System.Serializable]
public class AudioHolder
{
    public SoundsFX audiokey;
    public AudioClip audioClip;

    public AudioHolder(SoundsFX audiokey, AudioClip audioClip)
    {
        this.audiokey = audiokey;
        this.audioClip = audioClip;
    }
}

public enum SoundsFX
{
    SFX_Door,
    SFX_Coins,
    SFX_CashRegister,
    SFX_Harvest,
    SFX_TierUpgrade,
    SFX_Craft,
    SFX_MissionComplete,
    SFX_PoofOfSmoke,
    SFX_Water,
    Ambient_Birds,
    Ambient_ForestSound,
    Music_FarmTheme,
    Music_BearSpot,
    Music_FoxSpot,
    Music_Lake,
    Music_CursedLake,
    Music_HouseInterior,
    None
}