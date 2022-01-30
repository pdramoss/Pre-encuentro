using UnityEngine;
using System.Collections.Generic;

public static class AudioManager
{
    public enum Sound {
        mxIngame,
        sfx_baby_damage,
        sfx_baby_head,
        sfx_baby_loop,
        sfx_baby_tail,
        sfx_whale_damage,
        sfx_whale_head,
        sfx_whale_loop,
        sfx_whale_tail,
        sfx_whale_die,
        sfx_whale_sonar,
        sfx_baby_die,
        sfx_ui_click,
        sfx_pickup_food,
        sfx_danger_loop,
        amb_ocean,
        mx_menu
    }

    private static List<GameObject> activeSounds = new List<GameObject>();

    // This send the default parameters to the PlaySound method
    public static void PlaySound(Sound sound)
    {
        PlaySound(sound, false, 1f);
    }
    
    public static void PlaySound(Sound sound, bool loop, float volume){
        
        GameObject soundGameObject = new GameObject(sound.ToString());
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.clip = GetAudioClip(sound);
        audioSource.loop = loop;
        audioSource.volume = volume;
        if (soundGameObject.name.StartsWith("amb")){
            audioSource.time = Random.Range(0, audioSource.clip.length);
            audioSource.outputAudioMixerGroup = AudioAssets.i.ambienceBus;
        }
        else if (soundGameObject.name.StartsWith("sfx") || soundGameObject.name.StartsWith("ui")){
            audioSource.outputAudioMixerGroup = AudioAssets.i.sfxBus;
        }
        else if (soundGameObject.name.StartsWith("mx")){
            audioSource.outputAudioMixerGroup = AudioAssets.i.musicBus;
        }
        
        audioSource.Play();

        if (audioSource.loop == false){
            Object.Destroy(soundGameObject, audioSource.clip.length);
        }

    }

    public static void StopSound(Sound sound){

        // Solución provisional mientras se hace un audioPool
        GameObject soundObject = GameObject.Find(sound.ToString());
        AudioSource activeSound = soundObject.GetComponent<AudioSource>();
        //debería haber fade
        activeSound.Stop();
        Object.Destroy(soundObject);
      
    }

    private static AudioClip GetAudioClip (Sound sound){
        // Que agarre uno random si es array de mas de 1
        foreach (AudioAssets.SoundAudioClip soundAudioClip in AudioAssets.i.soundAudioClipArray)
        {
            if (soundAudioClip.sound == sound){
                return soundAudioClip.audioClip;
            }
        }
        Debug.LogError("Sound" + sound + "no existe my dog");
        return null;
    }
    
}
