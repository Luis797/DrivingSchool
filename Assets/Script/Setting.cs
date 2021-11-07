using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{
    public GameSetting Settings;


    public Slider MusicSlider;
    public Slider SoundFxSlider;

    public AudioMixer audioMixer;

    private void Start()
    {
        SetOptionsUI();
        Time.timeScale = 1.0f;

       
    }
    void SetOptionsUI()
    {


        MusicSlider.value = Settings.GetMusicLevel();
        SoundFxSlider.value = Settings.GetSoundFxLevel();
    }


    public void Quit()
    {
        Application.Quit();
    }


    public void SetSfxLvl(float sfxLvl)
    {
       
        audioMixer.SetFloat("SoundFx", sfxLvl);
        SaveAudioSetting();
    }

    public void SetMusicLvl(float musicLvl)
    {
      
        audioMixer.SetFloat("Music", musicLvl);
        SaveAudioSetting();
    }

    public void SaveAudioSetting()
    {
        Settings.SetMusicLevel(MusicSlider.value);
        Settings.SetSoundFxLevel(SoundFxSlider.value);
    }


}
