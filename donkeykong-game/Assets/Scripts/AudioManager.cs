using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    void Awake()
    {
        instance = this;
    }

    //sound effects
    public AudioClip sfx_shoot, sfx_hurt, sfx_jump, sfx_win, sfx_zombie;

    public GameObject soundObject;

    public void PlaySFX(string sfxName)
    {
        switch (sfxName)
        {
            case "shoot":
                SoundObjectCreation(sfx_shoot);
                break;
            case "hurt":
                SoundObjectCreation(sfx_hurt);
                break;
            case "win":
                SoundObjectCreation(sfx_win);
                break;
            case "jump":
                SoundObjectCreation(sfx_jump);
                break;
            case "zombie":
                SoundObjectCreation(sfx_zombie);
                break;
            default:
                break;
        }


    }

    void SoundObjectCreation(AudioClip clip)
    {
        //create soundobject gameobject
        GameObject newObject = Instantiate(soundObject, transform);
        //assign audioclip to its source
        newObject.GetComponent<AudioSource>().clip = clip;
        //play audio
        newObject.GetComponent<AudioSource>().Play();
    }

}
