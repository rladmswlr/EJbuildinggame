using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioControl : MonoBehaviour
{
    public AudioMixer masterMixer;

    public void BgmControl(float SliderValue)
	{
        masterMixer.SetFloat("BGM", Mathf.Log(SliderValue) * 20);
	}

    public void SFXControl(float SliderValue)
    {
        masterMixer.SetFloat("SFX", Mathf.Log(SliderValue) * 20);
    }
}
