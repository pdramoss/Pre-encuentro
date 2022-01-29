using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioAssets : MonoBehaviour
{
    private static AudioAssets _i;

    public static AudioAssets i {
        get {
            if (_i == null) _i = (Instantiate(Resources.Load("AudioAssets")) as GameObject).GetComponent<AudioAssets>();
            return _i;
        }
    }

    public SoundAudioClip[] soundAudioClipArray;
    public AudioMixerGroup ambienceBus;
	public AudioMixerGroup sfxBus;
	public AudioMixerGroup musicBus;

    [System.Serializable]
    public class SoundAudioClip{
        public AudioManager.Sound sound;
        public AudioClip audioClip;
    }
}
