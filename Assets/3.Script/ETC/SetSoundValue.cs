using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetSoundValue : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider slider;

    public void AudioControl()
    {
        float sound = slider.value;

        if(sound == 40f)
        {
            mixer.SetFloat("BGM", -80);
        }
        else
        {
            mixer.SetFloat("BGM", sound);
        }
    }
}
