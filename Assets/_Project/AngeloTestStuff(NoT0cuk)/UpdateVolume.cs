using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class UpdateVolume : MonoBehaviour
{
    [SerializeField] private AudioMixer master;
    [SerializeField] private Slider audioSlider;
    [SerializeField] private FloatObject masterVolume;

    public void UpdateAudio()
    {
      master.SetFloat("MasterVolume", masterVolume.value);
    }

    public void UpdateAudioFromSlider()
    {
      if(audioSlider.value <= -19)
      {
        masterVolume.value = -80;
        master.SetFloat("MasterVolume", 0);
      } else {
        masterVolume.value = audioSlider.value;
        master.SetFloat("MasterVolume", audioSlider.value);
      }

    }
}
